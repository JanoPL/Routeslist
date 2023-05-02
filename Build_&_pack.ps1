<#
.SYNOPSIS 
    Build and generate package from nuspec file

.DESCRIPTION
    To build and pack, this script is required put in system environment path for MSBuild and nuget.exe

.EXAMPLE
    Build_&_pack.ps1

.NOTES
    Version    : 1.0.0
    Author     : JanoPL
    Created on : 2021-12-25
    License    : MIT License
    Copyright  : (c) 2022 JanoPL
#>

MSBuild.exe .\RoutesList.sln /t:Restore /p:Configuration=Release /t:build

nuget.exe pack .\RoutesList.nuspec