<#
.SYNOPSIS 
    Build and generate coverate raport for tests

.DESCRIPTION
    To build and test, this script required reportgenerator and coverrage collector module to run

.EXAMPLE
    Build_&_pack.ps1

.PARAMETER framework
     The target framework to run tests and coverage for.

.NOTES
    Version    : 1.1.1
    Author     : JanoPL
    Created on : 2023-02-05
    License    : MIT License
    Copyright  : (c) 2023 JanoPL
#>

param (
    [string]
    $framework
)

function CheckFramework 
{
    switch ($framework) {
         netcoreapp3.1 {
            Restore;
            Build($framework);
            Test($framework);
            GenerateReport;
            RemoveDirectories;
            break;
         }
         net5.0 {
            Restore;
            Build;
            Test($framework);
            GenerateReport;
            RemoveDirectories;
            break;
         }
         net6.0 {
            Restore;
            Build;
            Test($framework);
            GenerateReport;
            RemoveDirectories;
            break;
         }
         net7.0 {
            Restore;
            Build;
            Test($framework);
            GenerateReport;
            RemoveDirectories;
            break;
         }
         Default {
            Restore;
            Build;
            Test($framework);
            GenerateReport;
            RemoveDirectories;
            break;
         }
    }
}

function Restore() {
    dotnet restore;
}

function Build() 
{
    dotnet build .\..\RoutesList.sln --configuration Debug;
}

function Test($framework)
{
    if ([string]::IsNullOrEmpty($framework)) {
        dotnet test --collect:"XPlat Code Coverage" --no-build
    } else {
        dotnet test --collect:"XPlat Code Coverage" --no-build -f $framework;
    }
}

function GenerateReport() 
{
    if (Get-Command reportgenerator.exe -ErrorAction SilentlyContinue) {
        reportgenerator.exe -reports:.\..\tests\*\TestResults\*\coverage.cobertura.xml -targetdir:coveragereport;
    } else {
        Write-Output "Report generator does'n exist, please install via 'dotnet tool install -g dotnet-reportgenerator-globaltool'";
    }    
}

function RemoveDirectories() 
{
    Write-Output "Removing Directory TestResults";
    Remove-Item .\..\tests\*\TestResults -Recurse -Verbose;
}

CheckFramework;
