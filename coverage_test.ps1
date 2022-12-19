<#
.SYNOPSIS 
    Build and generate coverate raport for tests

.DESCRIPTION
    To build and test, this script is required put reportgenerator and coverrage collector module 

.EXAMPLE
    Build_&_pack.ps1

.NOTES
    Version    : 1.0.0
    Author     : JanoPL
    Created on : 2022-12-19
    License    : MIT License
    Copyright  : (c) 2022 JanoPL
#>

dotnet build .\RoutesList.sln --configuration Release

dotnet test --collect:"XPlat Code Coverage" --no-build 

if (Get-Command reportgenerator.exe -ErrorAction SilentlyContinue) {
    reportgenerator.exe -reports:tests\*\TestResults\*\coverage.cobertura.xml -targetdir:coveragereport    
} else {
    Write-Host "Report generator does'n exist, please install via 'dotnet tool install -g dotnet-reportgenerator-globaltool' "
}
