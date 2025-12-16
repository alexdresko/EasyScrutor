#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Automates the version bump and release tagging process for EasyScrutor.

.DESCRIPTION
    This script:
    - Reads the current version from EasyScrutor.csproj
    - Suggests the next patch version (increments the third part)
    - Prompts for version confirmation or custom version
    - Updates the version in the .csproj file
    - Commits the version change
    - Creates and pushes a git tag to trigger the NuGet release workflow

.PARAMETER Version
    Optional. Specify the version directly without prompting.

.EXAMPLE
    .\Release-Version.ps1
    Interactively prompts for version with automatic suggestion.

.EXAMPLE
    .\Release-Version.ps1 -Version 4.3.0
    Sets version to 4.3.0 directly.
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [string]$Version
)

# Set strict mode for better error handling
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Define paths
$scriptRoot = Split-Path -Parent $PSScriptRoot
$csprojPath = Join-Path $scriptRoot 'src\EasyScrutor\EasyScrutor.csproj'

# Verify we're in a git repository
if (-not (Test-Path (Join-Path $scriptRoot '.git'))) {
    Write-Error "This script must be run from the EasyScrutor repository root."
    exit 1
}

# Verify csproj exists
if (-not (Test-Path $csprojPath)) {
    Write-Error "Could not find EasyScrutor.csproj at: $csprojPath"
    exit 1
}

# Function to extract version from csproj
function Get-CurrentVersion {
    param([string]$CsprojPath)
    
    $content = Get-Content $CsprojPath -Raw
    if ($content -match '<Version>(\d+\.\d+\.\d+)</Version>') {
        return $matches[1]
    }
    
    Write-Error "Could not find version in $CsprojPath"
    exit 1
}

# Function to increment patch version
function Get-NextVersion {
    param([string]$CurrentVersion)
    
    $parts = $CurrentVersion.Split('.')
    if ($parts.Length -ne 3) {
        Write-Error "Version must be in format: Major.Minor.Patch"
        exit 1
    }
    
    $major = [int]$parts[0]
    $minor = [int]$parts[1]
    $patch = [int]$parts[2]
    
    return "$major.$minor.$($patch + 1)"
}

# Function to update version in csproj
function Set-CsprojVersion {
    param(
        [string]$CsprojPath,
        [string]$NewVersion
    )
    
    $content = Get-Content $CsprojPath -Raw
    $newContent = $content -replace '<Version>\d+\.\d+\.\d+</Version>', "<Version>$NewVersion</Version>"
    Set-Content $CsprojPath -Value $newContent -NoNewline
}

# Main script logic
Write-Host "`n=== EasyScrutor Release Manager ===" -ForegroundColor Cyan

# Get current version
$currentVersion = Get-CurrentVersion -CsprojPath $csprojPath
Write-Host "`nCurrent version: " -NoNewline
Write-Host $currentVersion -ForegroundColor Yellow

# Get suggested next version
$suggestedVersion = Get-NextVersion -CurrentVersion $currentVersion
Write-Host "Suggested version: " -NoNewline
Write-Host $suggestedVersion -ForegroundColor Green

# Prompt for version if not provided
if (-not $Version) {
    Write-Host "`nEnter new version (press Enter for suggested version): " -NoNewline -ForegroundColor Cyan
    $userInput = Read-Host
    
    if ([string]::IsNullOrWhiteSpace($userInput)) {
        $Version = $suggestedVersion
    } else {
        $Version = $userInput.Trim()
    }
}

# Validate version format
if ($Version -notmatch '^\d+\.\d+\.\d+$') {
    Write-Error "Invalid version format. Must be: Major.Minor.Patch (e.g., 4.2.0)"
    exit 1
}

# Check if version is the same as current
if ($Version -eq $currentVersion) {
    Write-Warning "New version ($Version) is the same as current version. No changes needed."
    exit 0
}

Write-Host "`nVersion to release: " -NoNewline
Write-Host $Version -ForegroundColor Green

# Confirm with user
Write-Host "`nThis will:" -ForegroundColor Cyan
Write-Host "  1. Update version in EasyScrutor.csproj"
Write-Host "  2. Commit the change"
Write-Host "  3. Create tag v$Version"
Write-Host "  4. Push the tag (triggers NuGet release)"
Write-Host "`nDo you want to continue? [Y/n]: " -NoNewline -ForegroundColor Yellow
$confirmation = Read-Host

if ($confirmation -and $confirmation -ne 'Y' -and $confirmation -ne 'y') {
    Write-Host "Release cancelled." -ForegroundColor Red
    exit 0
}

# Update version in csproj
Write-Host "`nUpdating version in csproj..." -ForegroundColor Cyan
Set-CsprojVersion -CsprojPath $csprojPath -NewVersion $Version
Write-Host "✓ Version updated to $Version" -ForegroundColor Green

# Check for uncommitted changes (other than the version file)
$status = git status --porcelain
$versionFileChange = $status | Where-Object { $_ -match 'EasyScrutor\.csproj' }

if (-not $versionFileChange) {
    Write-Error "Expected version file change not found. Something went wrong."
    exit 1
}

# Commit the version change
Write-Host "`nCommitting version change..." -ForegroundColor Cyan
git add $csprojPath
git commit -m "Bump version to $Version"

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Committed version change" -ForegroundColor Green
} else {
    Write-Error "Failed to commit version change"
    exit 1
}

# Create tag
Write-Host "`nCreating tag v$Version..." -ForegroundColor Cyan
git tag "v$Version"

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Created tag v$Version" -ForegroundColor Green
} else {
    Write-Error "Failed to create tag"
    exit 1
}

# Push commit
Write-Host "`nPushing commit to origin..." -ForegroundColor Cyan
$currentBranch = git branch --show-current
git push origin $currentBranch

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Pushed commit" -ForegroundColor Green
} else {
    Write-Error "Failed to push commit"
    exit 1
}

# Push tag
Write-Host "`nPushing tag v$Version to origin..." -ForegroundColor Cyan
git push origin "v$Version"

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Pushed tag v$Version" -ForegroundColor Green
} else {
    Write-Error "Failed to push tag"
    exit 1
}

# Success message
Write-Host "`n=== Release Process Initiated ===" -ForegroundColor Green
Write-Host "`nVersion $Version has been tagged and pushed!" -ForegroundColor Green
Write-Host "`nThe GitHub Actions workflow will now:" -ForegroundColor Cyan
Write-Host "  1. Build and test the package"
Write-Host "  2. Publish to NuGet.org"
Write-Host "  3. Create a GitHub release"
Write-Host "`nMonitor progress at:" -ForegroundColor Cyan
Write-Host "  https://github.com/alexdresko/EasyScrutor/actions" -ForegroundColor Blue
Write-Host "`n"