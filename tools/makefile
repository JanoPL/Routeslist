HOME:=$(shell pwd)
NAME:=RoutesList

BASE_VERSION = 1.1

BUILD_NUMBER ?= 1
VERSION = $(BASE_VERSION).$(BUILD.NUMBER)

all: get-dependecies package

get-dependecies:
	dotnet restore .\\..\\RoutesList.sln

clean: 
	dotnet clean .\\..\\RoutesList.sln

build-debug: get-dependecies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln

build-debug-net7.0: get-dependecies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f net7.0

build-debug-net6.0: get-dependecies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f net6.0

build-debug-net5.0: get-dependecies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f net5.0

build-debug-netcoreapp3.1: get-dependecies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f netcoreapp3.1

build-release: get-dependecies
	dotnet build /p:Configuration=Release .\\..\\RoutesList.sln

package: build-release
	nuget.exe pack .\\..\\RoutesList.nuspec -OutputDirectory .\\nugets

test: get-dependecies
	dotnet test .\\..\\RoutesList.sln

# Coverage 
coverage: build-debug
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build

coverage-net7.0: build-debug-net7.0
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build

coverage-net6.0: build-debug-net6.0
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build

coverage-net5.0: build-debug-net5.0
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build

coverage-netcoreapp3.1: build-debug-netcoreapp3.1
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build

coverage-generate-report: coverage
	reportgenerator.exe -reports:.\\..\\tests\\*\\TestResults\\*\\coverage.cobertura.xml -targetdir:coveragereport;