param(
    [switch]$Live,

    [ValidateRange(10, 7200)]
    [int]$DurationSeconds = 900,

    [ValidateRange(70, 5000)]
    [int]$PeriodMs = 100,

    [ValidateRange(1, 1024)]
    [int]$MaxLogMiB = 64,

    [ValidateRange(0, 300)]
    [int]$StartupDelaySeconds = 10,

    [string]$OutputDirectory,

    [switch]$ShowSamples
)

$ErrorActionPreference = 'Stop'
if ($args.Count -ne 0) {
    $unknown = $args -join ' '
    throw "Unknown argument(s): $unknown. For -SkipBaseline and candidate batches, use start-validation-sequence.ps1."
}

$repositoryRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..\..'))
$workspaceRoot = Split-Path -Parent $repositoryRoot
if ([string]::IsNullOrWhiteSpace($OutputDirectory)) {
    $OutputDirectory = Join-Path $workspaceRoot 'XA50-workbench\captures\vehicle-a'
}
$OutputDirectory = [IO.Path]::GetFullPath($OutputDirectory)
$executable = Join-Path $PSScriptRoot 'Xa50.Pcan\bin\Release\xa50-pcan.exe'
if (-not (Test-Path -LiteralPath $executable -PathType Leaf)) {
    throw "PCAN client not found at $executable. Run tools\validation\build-pcan-client.ps1 first."
}

$timestamp = [DateTime]::UtcNow.ToString('yyyyMMddTHHmmssfffZ')
$outputPath = Join-Path $OutputDirectory ("vehicle-a_road_{0}.jsonl" -f $timestamp)
if (Test-Path -LiteralPath $outputPath) {
    throw "Refusing to overwrite existing capture: $outputPath"
}

$arguments = @(
    '--road-capture',
    '--duration-seconds', [string]$DurationSeconds,
    '--period-ms', [string]$PeriodMs,
    '--max-log-mib', [string]$MaxLogMiB,
    '--output', $outputPath
)
if ($Live) {
    $arguments += '--live'
} else {
    $arguments += '--dry-run'
}
if (-not $ShowSamples) {
    $arguments += '--quiet'
}

Write-Output ([pscustomobject]@{
    Workflow = 'Standalone 13-signal baseline only'
    Mode = if ($Live) { 'LIVE' } else { 'DRY RUN' }
    Output = $outputPath
    DurationSeconds = $DurationSeconds
    PeriodMs = $PeriodMs
    MaxLogMiB = $MaxLogMiB
    StartupDelaySeconds = $StartupDelaySeconds
})

if ($Live -and $StartupDelaySeconds -gt 0) {
    Write-Warning 'This command records only the 13-signal baseline; it does not run qualified candidate batches.'
    Write-Output "Live capture will start in $StartupDelaySeconds seconds."
    Start-Sleep -Seconds $StartupDelaySeconds
    Write-Output 'Startup delay finished; opening PCAN.'
}

& $executable @arguments
if ($LASTEXITCODE -ne 0) {
    throw "Road capture failed with exit code $LASTEXITCODE."
}

if ($Live) {
    if (-not (Test-Path -LiteralPath $outputPath -PathType Leaf)) {
        throw "Capture reported success but the JSONL file is missing: $outputPath"
    }
    Get-Item -LiteralPath $outputPath | Select-Object FullName, Length, LastWriteTime
}
