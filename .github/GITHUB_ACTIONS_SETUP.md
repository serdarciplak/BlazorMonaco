# GitHub Actions Configuration

## Required Secrets

The GitHub Actions workflow requires the following secrets to be configured in your repository:

### AZURE_DEVOPS_PAT
- **Description**: Personal Access Token for Azure DevOps NuGet feed authentication
- **Usage**: Used by `dotnet nuget push` to publish packages to the InSpark feed
- **Permissions Required**: 
  - Packaging (Read & Write)
- **Setup**: 
  1. Go to Azure DevOps → User Settings → Personal Access Tokens
  2. Create a new token with Packaging permissions
  3. Add to GitHub repository secrets as `AZURE_DEVOPS_PAT`

### AZURE_DEVOPS_PAT_SYMBOLS
- **Description**: Personal Access Token for publishing debug symbols to Azure Artifacts
- **Usage**: Used by the `microsoft/action-publish-symbols` action
- **Permissions Required**:
  - Symbols (Read & Write)
- **Setup**:
  1. Go to Azure DevOps → User Settings → Personal Access Tokens
  2. Create a new token with Symbols permissions
  3. Add to GitHub repository secrets as `AZURE_DEVOPS_PAT_SYMBOLS`

## Required GitHub Actions Runner

The workflow uses a custom self-hosted runner:
- **Runner Label**: `inspark-ubuntu-latest-8-core`
- This must be configured in your GitHub Actions environment

## Workflow Triggers

The workflow runs on:
- **Manual trigger**: `workflow_dispatch` (can be triggered manually from GitHub UI)
- **Pull requests**: On `synchronize`, `opened`, or `reopened` events
- **Push to main**: Automatically publishes packages on merge to main branch

## Version Suffixes

- **Main branch**: Packages are published with the version from `.csproj` (e.g., `1.0.0`)
- **Non-main branches**: Packages get a timestamp suffix (e.g., `1.0.0-20260602174500`)

This ensures:
- Official releases only come from the main branch
- Pull request builds create prerelease packages for testing
- Each build has a unique version number
