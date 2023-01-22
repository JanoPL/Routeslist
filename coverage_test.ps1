<#
.SYNOPSIS 
    Build and generate coverate raport for tests

.DESCRIPTION
    To build and test, this script required reportgenerator and coverrage collector module to run

.EXAMPLE
    Build_&_pack.ps1

.NOTES
    Version    : 1.1.0
    Author     : JanoPL
    Created on : 2023-01-22
    License    : MIT License
    Copyright  : (c) 2023 JanoPL
#>

dotnet restore;

dotnet build .\RoutesList.sln --configuration Debug;

dotnet test --collect:"XPlat Code Coverage" --no-build;

if (Get-Command reportgenerator.exe -ErrorAction SilentlyContinue) {
    reportgenerator.exe -reports:tests\*\TestResults\*\coverage.cobertura.xml -targetdir:coveragereport;
} else {
    Write-Host "Report generator does'n exist, please install via 'dotnet tool install -g dotnet-reportgenerator-globaltool'";
}

Write-Host "Removing Directory TestResults";
Remove-Item tests\*\TestResults -Recurse -Verbose;