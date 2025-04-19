HOME:=$(shell pwd)
NAME:=RoutesList

BASE_VERSION = 1.2

BUILD_NUMBER ?= 0
VERSION = $(BASE_VERSION).$(BUILD_NUMBER)

all: get-dependencies build-release package

get-dependencies:
	dotnet restore .\\..\\RoutesList.sln

clean: 
	dotnet clean .\\..\\RoutesList.sln

build-debug: get-dependencies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln

build-debug-net9.0: get-dependencies
    dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f net9.0

build-debug-net8.0: get-dependencies
    dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f net8.0

build-debug-net7.0: get-dependencies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f net7.0

build-debug-net6.0: get-dependencies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f net6.0

build-debug-net5.0: get-dependencies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f net5.0

build-debug-netcoreapp3.1: get-dependencies
	dotnet build /p:Configuration=Debug .\\..\\RoutesList.sln -f netcoreapp3.1

build-release: get-dependencies
	dotnet build /p:Configuration=Release .\\..\\RoutesList.sln

package: build-release
	nuget.exe pack .\\..\\RoutesList.nuspec -OutputDirectory .\\nugets -Symbols -SymbolPackageFormat snupkg -Version $(VERSION)

test: get-dependencies
	dotnet test .\\..\\RoutesList.sln

# Coverage 
coverage: build-debug
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build

coverage-net9.0: build-debug-net9.0
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build -f net9.0

coverage-net8.0: build-debug-net8.0
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build -f net8.0

coverage-net7.0: build-debug-net7.0
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build -f net7.0

coverage-net6.0: build-debug-net6.0
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build -f net6.0

coverage-net5.0: build-debug-net5.0
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build -f net5.0

coverage-netcoreapp3.1: build-debug-netcoreapp3.1
	dotnet test .\\..\\RoutesList.sln --collect:"XPlat Code Coverage" --no-build

coverage-generate-report: coverage
	reportgenerator.exe -reports:.\\..\\tests\\*\\TestResults\\*\\coverage.cobertura.xml -targetdir:coveragereport;