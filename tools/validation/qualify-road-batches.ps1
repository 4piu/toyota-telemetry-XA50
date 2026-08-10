param(
    [switch]$Live,
    [string]$Manifest = (Join-Path $PSScriptRoot '..\..\data\validation-batches\hybrid-control-road-v1.json'),
    [string]$OutputDirectory,
    [ValidateRange(70, 5000)][int]$PeriodMs = 100,
    [ValidateRange(1, 1024)][int]$MaxLogMiB = 16,
    [ValidateRange(32, 4096)][int]$MaxDirectoryMiB = 128
)

$ErrorActionPreference = 'Stop'
$repositoryRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..\..'))
$workspaceRoot = Split-Path -Parent $repositoryRoot
$Manifest = [IO.Path]::GetFullPath($Manifest)
if (-not (Test-Path -LiteralPath $Manifest -PathType Leaf)) { throw "Manifest not found: $Manifest" }
if ([string]::IsNullOrWhiteSpace($OutputDirectory)) {
    $OutputDirectory = Join-Path $workspaceRoot 'XA50-workbench\captures\vehicle-a\qualification'
}
$OutputDirectory = [IO.Path]::GetFullPath($OutputDirectory)
$executable = Join-Path $PSScriptRoot 'Xa50.Pcan\bin\Release\xa50-pcan.exe'
$analyzer = Join-Path $repositoryRoot 'tools\analysis\analyze-candidate-capture.py'
if (-not (Test-Path -LiteralPath $executable -PathType Leaf)) {
    throw "PCAN client not found. Run tools\validation\build-pcan-client.ps1 first."
}
$definition = Get-Content -LiteralPath $Manifest -Raw | ConvertFrom-Json
if ($definition.schema_version -ne 1) { throw 'Only validation-batch manifest schema 1 is supported.' }
if ($definition.endpoint.tester_can_id -ne '0x7D2' -or $definition.endpoint.response_can_id -ne '0x7DA') {
    throw 'This tool is restricted to the live-confirmed Hybrid Control 0x7D2/0x7DA endpoint.'
}
$manifestHash = (Get-FileHash -LiteralPath $Manifest -Algorithm SHA256).Hash.ToLowerInvariant().Substring(0, 12)

function Assert-CaptureBudget {
    $used = 0L
    if (Test-Path -LiteralPath $OutputDirectory -PathType Container) {
        $used = (Get-ChildItem -LiteralPath $OutputDirectory -File -Recurse | Measure-Object -Property Length -Sum).Sum
    }
    $projected = $used + ([long]$MaxLogMiB * 1MB)
    if ($projected -gt ([long]$MaxDirectoryMiB * 1MB)) {
        throw "Qualification directory budget would be exceeded. Review/remove private incomplete captures or raise -MaxDirectoryMiB deliberately."
    }
}

foreach ($batch in $definition.batches) {
    $dids = @($batch.dids | ForEach-Object { [string]$_.data_id })
    if ($dids.Count -lt 1 -or $dids.Count -gt 6) { throw "Batch $($batch.id) must contain 1-6 DIDs." }
    $stem = '{0}_{1}' -f $manifestHash, $batch.id
    $complete = Join-Path $OutputDirectory ($stem + '.qualification.complete.jsonl')
    if (Test-Path -LiteralPath $complete -PathType Leaf) {
        Write-Output "SKIP completed qualification: $($batch.id)"
        continue
    }
    Write-Output ([pscustomobject]@{ Mode = if ($Live) {'LIVE'} else {'DRY RUN'}; Batch = $batch.id; DIDs = ($dids -join ', '); Completion = $complete })
    if (-not $Live) { continue }

    New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null
    Assert-CaptureBudget
    $timestamp = [DateTime]::UtcNow.ToString('yyyyMMddTHHmmssfffZ')
    $partial = Join-Path $OutputDirectory ($stem + '.' + $timestamp + '.qualification.partial.jsonl')
    $summary = Join-Path $OutputDirectory ($stem + '.qualification.summary.json')
    $arguments = @('--candidate-qualify', '--live', '--batch-id', [string]$batch.id,
        '--period-ms', [string]$PeriodMs, '--max-log-mib', [string]$MaxLogMiB, '--output', $partial)
    foreach ($did in $dids) { $arguments += @('--did', $did) }
    & $executable @arguments
    $captureExit = $LASTEXITCODE
    if ($captureExit -eq 0) {
        & uv run python $analyzer $partial --catalog (Join-Path $repositoryRoot 'data\catalog\generated\telemetry.csv') --summary-json $summary
    }
    if ($captureExit -eq 0 -and $LASTEXITCODE -eq 0) {
        Move-Item -LiteralPath $partial -Destination $complete
        Write-Output "COMPLETE qualification: $complete"
        continue
    }
    if (Test-Path -LiteralPath $partial) {
        $incomplete = $partial -replace '\.partial\.jsonl$', '.incomplete.jsonl'
        Move-Item -LiteralPath $partial -Destination $incomplete
        Write-Warning "Qualification was quarantined as incomplete: $incomplete"
    }
    throw "Qualification batch $($batch.id) did not complete; the next launch will restart this batch."
}
