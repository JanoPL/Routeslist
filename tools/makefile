# RoutesList Makefile - Cross-Platform Enhancement
# -----------------------------------------------

# Project configuration
NAME := RoutesList

# Detect OS and set path handling accordingly
ifeq ($(OS),Windows_NT)
    # Windows-specific settings
    ROOT_DIR := $(shell cd)
    PATH_SEP := \\
    NULL_DEV := nul
    POWERSHELL := powershell.exe
    NUGET := nuget.exe
    REPORTGEN := reportgenerator.exe
    PS_CMD = $(POWERSHELL) -Command
else
    # Unix-specific settings
    ROOT_DIR := $(shell pwd)
    PATH_SEP := /
    NULL_DEV := /dev/null
    NUGET := nuget
    REPORTGEN := reportgenerator
    PS_CMD = 
endif

# Define path normalization function
normalize_path = $(subst \,/,$(1))

# Define path conversion function
# Convert paths to OS-specific format when needed
ifeq ($(OS),Windows_NT)
    convert_path = $(subst /,$(PATH_SEP),$(1))
else
    convert_path = $(1)
endif

# Define directory operations
ifeq ($(OS),Windows_NT)
    # Windows directory operations
    mkdir_cmd = if not exist "$(call convert_path,$(1))" mkdir "$(call convert_path,$(1))"
    rmdir_cmd = if exist "$(call convert_path,$(1))" rmdir /S /Q "$(call convert_path,$(1))"
else
    # Unix directory operations
    mkdir_cmd = mkdir -p $(1)
    rmdir_cmd = rm -rf $(1)
endif

# Project paths
SOLUTION_PATH := $(call normalize_path,$(ROOT_DIR)/../$(NAME).sln)
NUSPEC_PATH := $(call normalize_path,$(ROOT_DIR)/../$(NAME).nuspec)

# Version configuration
MAJOR_VERSION := 1
MINOR_VERSION := 2
PATCH_VERSION ?= 1
BUILD_NUMBER ?= 0

# Base version (SemVer format)
VERSION := $(MAJOR_VERSION).$(MINOR_VERSION).$(PATCH_VERSION)

# Full version with build number
VERSION_FULL := $(VERSION).$(BUILD_NUMBER)

# For displaying version information during help
VERSION_DISPLAY := $(VERSION) (build $(BUILD_NUMBER))

# Date handling for copyright
ifeq ($(OS),Windows_NT)
    COPYRIGHT_DATE := $(shell $(PS_CMD) "Get-Date -Format 'yyyy'")
else
    COPYRIGHT_DATE := $(shell date +"%Y")
endif

# Build configuration
CONFIG_DEBUG := Debug
CONFIG_RELEASE := Release
FRAMEWORKS := net9.0 net8.0 net7.0 net6.0 net5.0 netcoreapp3.1

# Common build arguments
BUILD_COMMON_ARGS := /p:WarningLevel=4 /p:TreatWarningsAsErrors=true

# Build arguments for release
BUILD_RELEASE_ARGS := /p:Configuration=$(CONFIG_RELEASE) \
	/p:Deterministic=true \
	/p:ContinuousIntegrationBuild=true \
	-p:Version=$(VERSION_FULL) \
	-p:AssemblyVersion=$(VERSION_FULL) \
	-p:FileVersion=$(VERSION_FULL) \
	-p:PackageVersion=$(VERSION_FULL) \
	-p:NuGetVersion=$(VERSION_FULL) \
	-p:IncludeSymbols=true \
	-p:IncludeSource=true

# Output directories (normalized paths)
COVERAGE_DIR := $(call normalize_path,$(ROOT_DIR)/coveragereport)
NUGET_OUTPUT_DIR := $(call normalize_path,$(ROOT_DIR)/nugets)

# Test output paths
COVERAGE_REPORTS := $(call normalize_path,$(ROOT_DIR)/../tests/*/TestResults/*/coverage.cobertura.xml)

# Default target
.PHONY: all
all: get-dependencies build-release package

# Display help
.PHONY: help
help:
	@echo "RoutesList Makefile Commands:"
	@echo "-------------------------------------------------------------------------"
	@echo "  all                 : Restore dependencies, build in release mode, and package"
	@echo "  get-dependencies    : Restore .NET dependencies"
	@echo "  clean               : Clean solution"
	@echo "  build-debug         : Build solution in debug mode"
	@echo "  build-debug-[FRAMEWORK] : Build specific framework in debug mode"
	@echo "  build-release       : Build solution in release mode"
	@echo "  package             : Create NuGet package"
	@echo "  test                : Run tests"
	@echo "  coverage            : Run tests with code coverage"
	@echo "  coverage-[FRAMEWORK]: Run tests with code coverage for specific framework"
	@echo "  coverage-report     : Generate coverage report"
	@echo ""
	@echo "Version Control:"
	@echo "  Current version: $(VERSION_FULL)"
	@echo "  Set version with: make package PATCH_VERSION=3 BUILD_NUMBER=42"
	@echo ""
	@echo "Supported Frameworks: $(FRAMEWORKS)"
	@echo "-------------------------------------------------------------------------"

# Dependency management
.PHONY: get-dependencies
get-dependencies:
	@echo "Restoring dependencies..."
	dotnet restore "$(call normalize_path,$(SOLUTION_PATH))"

# Clean build outputs
.PHONY: clean
clean:
	@echo "Cleaning solution..."
	dotnet clean "$(call normalize_path,$(SOLUTION_PATH))"
	$(call rmdir_cmd,$(NUGET_OUTPUT_DIR))
	$(call rmdir_cmd,$(COVERAGE_DIR))

# Debug builds
.PHONY: build-debug
build-debug: get-dependencies
	@echo "Building solution in debug mode..."
	dotnet build /p:Configuration=$(CONFIG_DEBUG) $(BUILD_COMMON_ARGS) "$(call normalize_path,$(SOLUTION_PATH))"

# Generate debug build targets for each framework
define make-debug-target
.PHONY: build-debug-$(1)
build-debug-$(1): get-dependencies
	@echo "Building solution for $(1) in debug mode..."
	dotnet build /p:Configuration=$(CONFIG_DEBUG) "$(call normalize_path,$(SOLUTION_PATH))" -f $(1)
endef

$(foreach framework,$(FRAMEWORKS),$(eval $(call make-debug-target,$(framework))))

# Release build
.PHONY: build-release
build-release: get-dependencies
	@echo "Building solution in release mode (version $(VERSION_FULL))..."
	dotnet build $(BUILD_RELEASE_ARGS) "$(call normalize_path,$(SOLUTION_PATH))"

# Package creation
.PHONY: package
package: build-release
	@echo "Creating NuGet package (version $(VERSION_FULL))..."
	$(call mkdir_cmd,$(NUGET_OUTPUT_DIR))
	$(NUGET) pack "$(call normalize_path,$(NUSPEC_PATH))" \
		-OutputDirectory "$(call normalize_path,$(NUGET_OUTPUT_DIR))" \
		-Properties copyright="Copyright © $(COPYRIGHT_DATE)" \
		-Symbols \
		-SymbolPackageFormat snupkg \
		-Version $(VERSION_FULL) \
		-Verbosity detailed

# Testing
.PHONY: test
test: get-dependencies
	@echo "Running tests..."
	dotnet test "$(call normalize_path,$(SOLUTION_PATH))"

# Code coverage
.PHONY: coverage
coverage: build-debug
	@echo "Running tests with code coverage..."
	dotnet test "$(call normalize_path,$(SOLUTION_PATH))" --collect:"XPlat Code Coverage" --no-build

# Generate coverage targets for each framework
define make-coverage-target
.PHONY: coverage-$(1)
coverage-$(1): build-debug-$(1)
	@echo "Running tests with code coverage for $(1)..."
	dotnet test "$(call normalize_path,$(SOLUTION_PATH))" --collect:"XPlat Code Coverage" --no-build -f $(1)
endef

$(foreach framework,$(FRAMEWORKS),$(eval $(call make-coverage-target,$(framework))))

# Coverage report generation
.PHONY: coverage-report
coverage-report: coverage
	@echo "Generating code coverage report..."
	$(call mkdir_cmd,$(COVERAGE_DIR))
	$(REPORTGEN) -reports:"$(COVERAGE_REPORTS)" -targetdir:"$(call convert_path,$(COVERAGE_DIR))"

# Alias for specific framework coverage reports
define make-coverage-report-target
.PHONY: coverage-report-$(1)
coverage-report-$(1): coverage-$(1)
	@echo "Generating code coverage report for $(1)..."
	$(call mkdir_cmd,$(COVERAGE_DIR))
	$(REPORTGEN) -reports:"$(COVERAGE_REPORTS)" -targetdir:"$(call convert_path,$(COVERAGE_DIR))"
endef

$(foreach framework,$(FRAMEWORKS),$(eval $(call make-coverage-report-target,$(framework))))

# Version information display
.PHONY: version
version:
	@echo "$(VERSION_FULL)"