# Release Process

This document describes how to release a new version of EasyScrutor to NuGet.

## Prerequisites

1. **NuGet API Key**: Ensure the `NUGET_TOKEN` secret is configured in the repository settings
   - Go to Settings → Secrets and variables → Actions
   - Add a secret named `NUGET_TOKEN` with your NuGet.org API key

2. **Permissions**: You need write access to the repository

## Quick Release (Recommended)

Use the automated PowerShell script to release a new version:

```powershell
# From repository root
.\scripts\Release-Version.ps1
```

This script will:
1. Show the current version (e.g., `4.2.0`)
2. Suggest the next patch version (e.g., `4.2.1`)
3. Prompt you to confirm or enter a different version
4. Update the version in `EasyScrutor.csproj`
5. Commit the change with message "Bump version to X.X.X"
6. Create and push tag `vX.X.X`
7. Trigger the automated NuGet release workflow

### Options

```powershell
# Specify version directly (no prompts)
.\scripts\Release-Version.ps1 -Version 4.3.0

# Preview what would happen without making changes
.\scripts\Release-Version.ps1 -WhatIf
```

## Manual Release Process

If you prefer to release manually, follow these steps:

### Step 1: Update Version

Update the version in `src/EasyScrutor/EasyScrutor.csproj`:

```xml
<Version>4.3.0</Version>
```

### Step 2: Commit and Push Changes

```bash
git add .
git commit -m "Bump version to 4.3.0"
git push origin alex
```

### Step 3: Create and Push a Tag

```bash
# Create a tag matching the version
git tag v4.3.0

# Push the tag to trigger the release workflow
git push origin v4.3.0
```

### Step 4: Monitor the Release

1. Go to the [Actions tab](https://github.com/alexdresko/EasyScrutor/actions)
2. Watch the "Publish to NuGet" workflow
3. Once complete, the package will be available on [NuGet.org](https://www.nuget.org/packages/EasyScrutor)
4. A GitHub release will be created automatically with the NuGet package attached

## GitHub UI Release (Alternative)

You can also trigger a release manually from the GitHub UI without creating a tag:

1. Go to the [Actions tab](https://github.com/alexdresko/EasyScrutor/actions)
2. Select "Publish to NuGet" workflow
3. Click "Run workflow"
4. Optionally specify a version number
5. Click "Run workflow"

**Note:** This method uses the version in the `.csproj` file and does not create a git tag.

## Using GitHub CLI

If you have the [GitHub CLI](https://cli.github.com/) installed:

```bash
# Create and push a release tag
gh release create v4.3.0 --generate-notes --title "Release 4.3.0"
```

This will:
- Create the tag
- Create a GitHub release
- Trigger the NuGet publish workflow

## Troubleshooting

### Release Failed

1. Check the [Actions tab](https://github.com/alexdresko/EasyScrutor/actions) for error logs
2. Common issues:
   - `NUGET_TOKEN` secret not configured or expired
   - Version already exists on NuGet (use `--skip-duplicate` flag)
   - Build or test failures

### Version Already Published

If you need to republish:
1. Increment the version number in the `.csproj` file
2. Create a new tag with the new version

## Version Numbering

Follow [Semantic Versioning](https://semver.org/):
- **Major** (X.0.0): Breaking changes
- **Minor** (x.X.0): New features, backward compatible
- **Patch** (x.x.X): Bug fixes, backward compatible

Example: `4.2.0` → `4.3.0` (new feature) or `4.2.1` (bug fix)
