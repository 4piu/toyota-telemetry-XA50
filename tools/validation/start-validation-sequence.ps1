param(
    [switch]$Live,
    [string]$Manifest = (Join-Path $PSScriptRoot '..\..\data\validation-batches\hybrid-control-road-v1.json'),
    [string]$OutputDirectory,
    [string]$QualificationDirectory,
    [ValidateRange(10, 600)][int]$BaselineDurationSeconds = 45,
    [ValidateRange(1, 1024)][int]$MaxLogMiB = 32,
    [ValidateRange(64, 4096)][int]$MaxDirectoryMiB = 256,
    [ValidateRange(1, 20)][int]$MaxBatches = 20,
    [ValidateRange(0, 300)][int]$StartupDelaySeconds = 10,
    [switch]$SkipBaseline
)

$ErrorActionPreference = 'Stop'
if ($args.Count -ne 0) {
    throw "Unknown argument(s): $($args -join ' ')."
}
$repositoryRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..\..'))
$Manifest = [IO.Path]::GetFullPath($Manifest)
if ([string]::IsNullOrWhiteSpace($OutputDirectory)) {
    $OutputDirectory = Join-Path $repositoryRoot 'scratch\captures\vehicle-a\road-batches'
}
if ([string]::IsNullOrWhiteSpace($QualificationDirectory)) {
    $QualificationDirectory = Join-Path $repositoryRoot 'scratch\captures\vehicle-a\qualification'
}
$OutputDirectory = [IO.Path]::GetFullPath($OutputDirectory)
$QualificationDirectory = [IO.Path]::GetFullPath($QualificationDirectory)
$definition = Get-Content -LiteralPath $Manifest -Raw | ConvertFrom-Json
$manifestHash = (Get-FileHash -LiteralPath $Manifest -Algorithm SHA256).Hash.ToLowerInvariant().Substring(0, 12)
$executable = Join-Path $PSScriptRoot 'Xa50.Pcan\bin\Release\xa50-pcan.exe'
$roadAnalyzer = Join-Path $repositoryRoot 'tools\analysis\analyze-road-capture.py'
$candidateAnalyzer = Join-Path $repositoryRoot 'tools\analysis\analyze-candidate-capture.py'
$catalog = Join-Path $repositoryRoot 'data\catalog\generated\telemetry.csv'
if (-not (Test-Path -LiteralPath $executable -PathType Leaf)) { throw 'Build the PCAN client before capture.' }

function Move-Incomplete([string]$Path) {
    if (Test-Path -LiteralPath $Path) {
        $target = $Path -replace '\.partial\.jsonl$', '.incomplete.jsonl'
        Move-Item -LiteralPath $Path -Destination $target
        $partialSummary = $Path -replace '\.jsonl$', '.summary.json'
        if (Test-Path -LiteralPath $partialSummary -PathType Leaf) {
            $incompleteSummary = $target -replace '\.jsonl$', '.summary.json'
            Move-Item -LiteralPath $partialSummary -Destination $incompleteSummary
        }
        Write-Warning "Run was quarantined as incomplete: $target"
    }
}

function Assert-CaptureBudget {
    $used = 0L
    if (Test-Path -LiteralPath $OutputDirectory -PathType Container) {
        $used = (Get-ChildItem -LiteralPath $OutputDirectory -File -Recurse | Measure-Object -Property Length -Sum).Sum
    }
    $projected = $used + ([long]$MaxLogMiB * 1MB)
    if ($projected -gt ([long]$MaxDirectoryMiB * 1MB)) {
        throw "Road-batch directory budget would be exceeded. Review/remove private incomplete captures or raise -MaxDirectoryMiB deliberately."
    }
}

$startupDelayApplied = $false
function Invoke-StartupDelayOnce {
    if ($startupDelayApplied) { return }
    $script:startupDelayApplied = $true
    if ($StartupDelaySeconds -gt 0) {
        Write-Output "Live validation sequence will start in $StartupDelaySeconds seconds."
        Start-Sleep -Seconds $StartupDelaySeconds
        Write-Output 'Startup delay finished; opening PCAN.'
    }
}

function Invoke-BoundedRun([string]$Id, [string[]]$Arguments, [string]$Partial, [string]$Complete, [string]$Analyzer, [string[]]$AnalyzerArguments) {
    $partialSummary = $Partial -replace '\.jsonl$', '.summary.json'
    $completeSummary = $Complete -replace '\.jsonl$', '.summary.json'
    & $executable @Arguments
    if ($LASTEXITCODE -ne 0) { Move-Incomplete $Partial; throw "$Id capture did not complete." }
    & uv run python $Analyzer $Partial @AnalyzerArguments
    if ($LASTEXITCODE -ne 0) { Move-Incomplete $Partial; throw "$Id failed offline structural validation." }
    Move-Item -LiteralPath $Partial -Destination $Complete
    if (Test-Path -LiteralPath $partialSummary -PathType Leaf) {
        Move-Item -LiteralPath $partialSummary -Destination $completeSummary
    }
    Write-Output "COMPLETE: $Complete"
}

$planned = 0
Write-Output "Startup delay: $StartupDelaySeconds seconds (once before the first live batch)."
if (-not $SkipBaseline) {
    $baselineId = 'baseline-13'
    $baselineComplete = Join-Path $OutputDirectory ("${manifestHash}_${baselineId}.complete.jsonl")
    if (-not (Test-Path -LiteralPath $baselineComplete -PathType Leaf)) {
        Write-Output ([pscustomobject]@{ Mode = if ($Live) {'LIVE'} else {'DRY RUN'}; Batch = $baselineId; DurationSeconds = $BaselineDurationSeconds; Signals = 13 })
        if ($Live) {
            New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null
            Assert-CaptureBudget
            $stamp = [DateTime]::UtcNow.ToString('yyyyMMddTHHmmssfffZ')
            $partial = Join-Path $OutputDirectory ("${manifestHash}_${baselineId}.${stamp}.partial.jsonl")
            $captureArguments = @('--road-capture','--live','--duration-seconds',[string]$BaselineDurationSeconds,
                '--period-ms',[string]$definition.limits.period_ms,'--max-log-mib',[string]$MaxLogMiB,'--output',$partial,'--quiet')
            Invoke-StartupDelayOnce
            & $executable @captureArguments
            if ($LASTEXITCODE -ne 0) { Move-Incomplete $partial; throw 'Baseline capture did not complete.' }
            $end = Get-Content -LiteralPath $partial -Tail 1 | ConvertFrom-Json
            if ($end.record -ne 'session_end' -or $end.reason -ne 'duration_limit') {
                Move-Incomplete $partial
                throw 'Baseline was interrupted or truncated; the next launch will restart it.'
            }
            $partialSummary = $partial -replace '\.jsonl$', '.summary.json'
            $completeSummary = $baselineComplete -replace '\.jsonl$', '.summary.json'
            & uv run python $roadAnalyzer $partial --summary-json $partialSummary
            if ($LASTEXITCODE -ne 0) { Move-Incomplete $partial; throw 'Baseline failed offline validation.' }
            Move-Item -LiteralPath $partial -Destination $baselineComplete
            Move-Item -LiteralPath $partialSummary -Destination $completeSummary
            Write-Output "COMPLETE: $baselineComplete"
        }
        $planned++
    } else { Write-Output "SKIP completed batch: $baselineId" }
}

foreach ($batch in $definition.batches) {
    if ($planned -ge $MaxBatches) { break }
    $complete = Join-Path $OutputDirectory ("${manifestHash}_$($batch.id).complete.jsonl")
    if (Test-Path -LiteralPath $complete -PathType Leaf) { Write-Output "SKIP completed batch: $($batch.id)"; continue }
    $qualification = Join-Path $QualificationDirectory ("${manifestHash}_$($batch.id).qualification.complete.jsonl")
    if (-not (Test-Path -LiteralPath $qualification -PathType Leaf)) {
        if ($Live) { throw "Missing completed stationary qualification for $($batch.id): $qualification" }
        $qualified = @($batch.dids | ForEach-Object { [string]$_.data_id })
        $qualificationState = 'required before live use'
    } else {
        & uv run python $candidateAnalyzer $qualification --catalog $catalog | Out-Null
        if ($LASTEXITCODE -ne 0) { throw "Qualification record for $($batch.id) is invalid." }
        $qualified = @(
            Get-Content -LiteralPath $qualification |
            ForEach-Object { $_ | ConvertFrom-Json } |
            Where-Object { $_.record -eq 'candidate_sample' -and $_.supported -eq $true } |
            ForEach-Object { [string]$_.did }
        )
        $qualificationState = 'complete'
    }
    if ($qualified.Count -eq 0) { Write-Warning "SKIP $($batch.id): none of its DIDs qualified."; continue }
    $duration = [int]$definition.limits.duration_seconds
    $minimum = [int]$definition.limits.minimum_samples_per_did
    Write-Output ([pscustomobject]@{ Mode = if ($Live) {'LIVE'} else {'DRY RUN'}; Batch = $batch.id; DurationSeconds = $duration; DIDs = ($qualified -join ', '); Qualification = $qualificationState })
    if ($Live) {
        New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null
        Assert-CaptureBudget
        $stamp = [DateTime]::UtcNow.ToString('yyyyMMddTHHmmssfffZ')
        $partial = Join-Path $OutputDirectory ("${manifestHash}_$($batch.id).${stamp}.partial.jsonl")
        $captureArguments = @('--candidate-batch','--live','--batch-id',[string]$batch.id,
            '--duration-seconds',[string]$duration,'--period-ms',[string]$definition.limits.period_ms,
            '--max-log-mib',[string]$MaxLogMiB,'--output',$partial,'--quiet')
        foreach ($did in $qualified) { $captureArguments += @('--did',$did) }
        $analysisArgs = @('--catalog', $catalog, '--minimum-samples', [string]$minimum,
            '--summary-json', ($partial -replace '\.jsonl$', '.summary.json'))
        Invoke-StartupDelayOnce
        Invoke-BoundedRun -Id $batch.id -Arguments $captureArguments -Partial $partial -Complete $complete `
            -Analyzer $candidateAnalyzer -AnalyzerArguments $analysisArgs
    }
    $planned++
}

Write-Output 'Sequence finished. Only manifest-hash-matched .complete.jsonl files count as progress.'
