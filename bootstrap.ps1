<#
.SYNOPSIS
    Turn a freshly created project into a working one: project id, pinned
    toolkit version, and the installed toolkit itself.

.DESCRIPTION
    "Use this template" on GitHub gives you files, not a working project: the
    marker still carries placeholders, and the toolkit that reads it is not
    installed anywhere yet. This script fills in what is still a placeholder,
    then hands off to the toolkit's own setup script to do the rest.

    Every value it fills in is a value it found still set to the template's
    placeholder - it will not silently move a pin you already set. Once a
    project is configured, running this again just re-runs the install (safe:
    setup-platform-project.ps1 fetches and rebuilds without moving anything
    unless the marker itself changed).

    A project id you did not supply is derived from the folder name. A vehicle
    name you did not supply is left blank - the README keeps its <VEHICLE>
    placeholder rather than guessing. A toolkit revision you did not supply
    resolves to the newest release tag, or the tip of the default branch with
    a clear warning if no release exists yet; that choice is made once, at
    this first configuration, because afterwards the pin belongs to whoever
    owns the project - see the toolkit's "Updating the pin" for how they move
    it later.

.EXAMPLE
    .\bootstrap.ps1

.EXAMPLE
    .\bootstrap.ps1 -ProjectId rav4-hv-xa50 -Vehicle "2023 RAV4 Hybrid XA50" -NonInteractive

.EXAMPLE
    .\bootstrap.ps1 -SkipInstall
#>
param(
    # The project. Defaults to the current directory.
    [string]$ProjectRoot = (Get-Location).Path,

    # Skip the project id prompt and use this value (sanitized to a slug).
    [string]$ProjectId,

    # Skip the vehicle prompt and use this value to fill the README title.
    # Pass an empty string explicitly to skip filling it non-interactively.
    [string]$Vehicle,

    # Skip automatic pin resolution and use this revision. Only takes effect
    # while the marker still has the placeholder revision.
    [string]$Revision,

    # Passed through to setup-platform-project.ps1.
    [string]$Toolkit,
    [switch]$SkipBackends,
    [switch]$SkipToolkit,
    [switch]$Offline,

    # Write the marker and stop; do not clone the toolkit or build anything.
    [switch]$SkipInstall,

    # Accept every default without prompting. For scripted or CI use.
    [switch]$NonInteractive
)

$ErrorActionPreference = 'Stop'
$suppliedVehicle = $PSBoundParameters.ContainsKey('Vehicle')

function Write-Step {
    param([string]$Message)
    Write-Host "==> $Message"
}

function Read-Answer {
    <#
    Prompt interactively; under -NonInteractive, return the default without
    asking. $Default is shown in the prompt and returned for a blank reply.
    #>
    param([string]$Prompt, [string]$Default = '')

    if ($NonInteractive) { return $Default }
    $suffix = if ($Default) { " [$Default]" } else { '' }
    $answer = Read-Host "$Prompt$suffix"
    if ([string]::IsNullOrWhiteSpace($answer)) { return $Default }
    return $answer.Trim()
}

function ConvertTo-ProjectSlug {
    param([string]$Text)
    $slug = $Text.ToLowerInvariant() -replace '[^a-z0-9]+', '-'
    $slug = $slug.Trim('-')
    if (-not $slug) { $slug = 'project' }
    return $slug
}

function Get-TomlValue {
    <#
    .SYNOPSIS
    Read one scalar value from telemetry-project.toml.

    .DESCRIPTION
    Duplicated from setup-platform-project.ps1 rather than shared: this script
    runs before the toolkit checkout that file lives in exists, so it cannot
    depend on it. Understands `[section]` headers and `key = "value"` lines,
    which is all the marker contains.
    #>
    param([string]$Path, [string]$Section, [string]$Key)

    $inSection = ($Section -eq '')
    foreach ($line in Get-Content -LiteralPath $Path) {
        $trimmed = $line.Trim()
        if ($trimmed -match '^\[([A-Za-z0-9_.]+)\]$') {
            $inSection = ($Matches[1] -eq $Section)
            continue
        }
        if (-not $inSection) { continue }
        if ($trimmed -match "^$Key\s*=\s*`"([^`"]*)`"") {
            return $Matches[1]
        }
    }
    return $null
}

function Set-TomlValue {
    <#
    .SYNOPSIS
    Replace one scalar value in telemetry-project.toml, in place.

    .DESCRIPTION
    Rewrites only the matched `key = "..."` line inside the named section;
    every other line, including comments, is copied through unchanged. Fails
    loudly if the key is not found rather than appending a duplicate - a
    marker this script cannot recognise is a template problem, not something
    to patch around silently.
    #>
    param([string]$Path, [string]$Section, [string]$Key, [string]$Value)

    $inSection = ($Section -eq '')
    $found = $false
    $lines = Get-Content -LiteralPath $Path
    $output = for ($i = 0; $i -lt $lines.Length; $i++) {
        $line = $lines[$i]
        $trimmed = $line.Trim()
        if ($trimmed -match '^\[([A-Za-z0-9_.]+)\]$') {
            $inSection = ($Matches[1] -eq $Section)
            $line
            continue
        }
        if ($inSection -and -not $found -and $trimmed -match "^$Key\s*=\s*`"([^`"]*)`"") {
            $found = $true
            $Key + ' = "' + $Value + '"'
            continue
        }
        $line
    }
    if (-not $found) {
        throw "$Path has no $Key under [$Section]. Is this a current template checkout?"
    }
    ($output -join "`n") + "`n" | Set-Content -LiteralPath $Path -Encoding utf8 -NoNewline
}

function Resolve-LatestRevision {
    <#
    .SYNOPSIS
    The newest release tag on a remote, or the tip of its default branch.

    .DESCRIPTION
    Runs against the bare URL with `git ls-remote`, so it needs no local
    checkout - there is not one yet at this point in the flow. Tags are
    compared as dotted version numbers when they parse as one (v0.3.0 -> 0.3.0
    -> [3]0,3,0); anything else falls back to plain text order, newest last
    either way, so the last entry after sorting is always what is returned.
    #>
    param([string]$RepositoryUrl)

    $tagLines = & git ls-remote --tags --refs $RepositoryUrl 2>$null
    if ($LASTEXITCODE -ne 0) {
        throw "Could not reach $RepositoryUrl to resolve a version. Check your connection, or pass -Revision to pin one manually."
    }
    $tags = @($tagLines | ForEach-Object {
        if ($_ -match 'refs/tags/(.+)$') { $Matches[1] }
    })
    if ($tags.Count -gt 0) {
        $ranked = $tags | ForEach-Object {
            $bare = $_ -replace '^v', ''
            $parts = $bare -split '\.'
            $numeric = $true
            $sortKey = foreach ($part in $parts) {
                if ($part -notmatch '^\d+$') { $numeric = $false }
                [int]($part -replace '\D', '0')
            }
            [pscustomobject]@{ Tag = $_; Numeric = $numeric; Key = $sortKey }
        }
        $best = $ranked | Where-Object Numeric |
            Sort-Object { $_.Key[0] }, { $_.Key[1] }, { $_.Key[2] } |
            Select-Object -Last 1
        if (-not $best) { $best = $ranked | Sort-Object Tag | Select-Object -Last 1 }
        return @{ Revision = $best.Tag; IsRelease = $true }
    }

    Write-Warning "No release tag found at $RepositoryUrl. Pinning the default branch tip instead - update the pin once a release exists."
    $headLine = & git ls-remote $RepositoryUrl HEAD 2>$null
    if ($LASTEXITCODE -ne 0 -or -not $headLine) {
        throw "Could not resolve HEAD at $RepositoryUrl."
    }
    $hash = ($headLine -split '\s+')[0]
    return @{ Revision = $hash; IsRelease = $false }
}

# ---------------------------------------------------------------- the project

$ProjectRoot = (Resolve-Path -LiteralPath $ProjectRoot).Path
$projectFile = Join-Path $ProjectRoot 'telemetry-project.toml'
if (-not (Test-Path -LiteralPath $projectFile -PathType Leaf)) {
    throw "No telemetry-project.toml in $ProjectRoot. Run this from a project created from the template, or pass -ProjectRoot."
}

$repository = Get-TomlValue -Path $projectFile -Section 'toolkit' -Key 'repository'
$currentId = Get-TomlValue -Path $projectFile -Section '' -Key 'project_id'
$currentRevision = Get-TomlValue -Path $projectFile -Section 'toolkit' -Key 'revision'

# --------------------------------------------------------------- project id

if ($currentId -like 'replace-with-*') {
    $folderName = Split-Path -Leaf $ProjectRoot
    $seed = if ($ProjectId) { $ProjectId } else { $folderName }
    $suggested = ConvertTo-ProjectSlug $seed
    $chosen = ConvertTo-ProjectSlug (Read-Answer -Prompt 'Project id' -Default $suggested)
    Set-TomlValue -Path $projectFile -Section '' -Key 'project_id' -Value $chosen
    Write-Step "project id  $chosen"
}
else {
    Write-Step "project id  $currentId (already set)"
}

# -------------------------------------------------------------- README title

$readmeFile = Join-Path $ProjectRoot 'README.md'
if ((Test-Path -LiteralPath $readmeFile) -and
    (Select-String -LiteralPath $readmeFile -Pattern '<VEHICLE>' -Quiet)) {
    $vehicle = if ($suppliedVehicle) { $Vehicle } else { Read-Answer -Prompt 'Vehicle (optional, fills the README title)' }
    if ($vehicle) {
        (Get-Content -LiteralPath $readmeFile -Raw) -replace '<VEHICLE>', $vehicle |
            Set-Content -LiteralPath $readmeFile -Encoding utf8 -NoNewline
        Write-Step "README      titled for $vehicle"
    }
    else {
        Write-Step 'README      left as <VEHICLE> - edit it whenever you like'
    }
}

# -------------------------------------------------------------------- pin

if ($currentRevision -like 'replace-with-*') {
    if ($Revision) {
        $resolved = $Revision
    }
    else {
        Write-Step "resolving the newest release of $repository"
        $result = Resolve-LatestRevision -RepositoryUrl $repository
        $resolved = $result.Revision
    }
    Set-TomlValue -Path $projectFile -Section 'toolkit' -Key 'revision' -Value $resolved
    Write-Step "pinned      $resolved"
}
else {
    Write-Step "pinned      $currentRevision (already set - see the toolkit's Updating the pin to move it)"
}

# ----------------------------------------------------------------- install

if ($SkipInstall) {
    Write-Host ''
    Write-Step 'configuration written; skipping install (-SkipInstall)'
    exit 0
}

$proceed = $NonInteractive -or ((Read-Answer -Prompt 'Install the toolkit and build the backends now? [Y/n]' -Default 'y') -notmatch '^n')
if (-not $proceed) {
    Write-Host ''
    Write-Step 'configuration written; run this again when you are ready to install'
    exit 0
}

$toolkitPath = if ($Toolkit) {
    $Toolkit
}
else {
    $configured = Get-TomlValue -Path $projectFile -Section 'paths' -Key 'toolkit'
    if ($configured) { Join-Path $ProjectRoot $configured } else { Join-Path (Split-Path -Parent $ProjectRoot) 'telemetry-tools' }
}
$toolkitPath = [System.IO.Path]::GetFullPath($toolkitPath)

if (-not (Test-Path -LiteralPath $toolkitPath -PathType Container)) {
    if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
        throw 'git was not found on PATH.'
    }
    Write-Step "cloning $repository into $toolkitPath"
    & git clone $repository $toolkitPath
    if ($LASTEXITCODE -ne 0) { throw 'git clone failed.' }
}

$setupScript = Join-Path $toolkitPath 'tools\setup-platform-project.ps1'
if (-not (Test-Path -LiteralPath $setupScript -PathType Leaf)) {
    throw "$setupScript is missing. $toolkitPath may not be a toolkit checkout."
}

$setupArgs = @{ ProjectRoot = $ProjectRoot; Toolkit = $toolkitPath }
if ($SkipBackends) { $setupArgs.SkipBackends = $true }
if ($SkipToolkit) { $setupArgs.SkipToolkit = $true }
if ($Offline) { $setupArgs.Offline = $true }
Write-Host ''
& $setupScript @setupArgs
