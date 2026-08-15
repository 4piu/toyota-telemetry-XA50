param(
    [ValidateSet('Release', 'Debug')]
    [string]$Configuration = 'Release'
)

$ErrorActionPreference = 'Stop'

$projectRoot = Join-Path $PSScriptRoot 'Xa50.J2534'
$sources = @(
    (Join-Path $projectRoot 'Program.cs'),
    (Join-Path $projectRoot 'Adapters\J2534Api.cs')
)
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$outputDirectory = Join-Path $projectRoot "bin\$Configuration"
$executable = Join-Path $outputDirectory 'xa50-j2534.exe'

if (-not (Test-Path -LiteralPath $compiler)) {
    throw "The .NET Framework C# compiler was not found at $compiler."
}

New-Item -ItemType Directory -Force -Path $outputDirectory | Out-Null

$compilerArguments = @(
    '/nologo',
    '/target:exe',
    '/platform:x86',
    '/warn:4',
    "/out:$executable"
)
if ($Configuration -eq 'Release') {
    $compilerArguments += '/optimize+'
    $compilerArguments += '/debug:pdbonly'
}
else {
    $compilerArguments += '/optimize-'
    $compilerArguments += '/debug:full'
    $compilerArguments += '/define:DEBUG;TRACE'
}
$compilerArguments += $sources

& $compiler @compilerArguments
if ($LASTEXITCODE -ne 0) {
    throw "C# compilation failed with exit code $LASTEXITCODE."
}

if (-not (Test-Path -LiteralPath $executable)) {
    throw "Build succeeded but the executable was not found at $executable."
}

Get-Item -LiteralPath $executable | Select-Object FullName, Length, LastWriteTime
