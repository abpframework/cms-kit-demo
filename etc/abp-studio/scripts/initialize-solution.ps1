$ErrorActionPreference = "Stop"

$scriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path
$solutionRoot = (Resolve-Path (Join-Path $scriptDirectory "..\..\..")).Path
$webProjectDirectory = Join-Path $solutionRoot "src\CmsKitDemo"

Write-Host "Installing client-side libraries..."
Push-Location $webProjectDirectory
try {
    abp install-libs

    Write-Host "Applying database migrations and seeding initial data..."
    dotnet run --migrate-database
}
finally {
    Pop-Location
}

Write-Host "Solution initialization completed."
