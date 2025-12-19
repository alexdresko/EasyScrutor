# GitHub Copilot Instructions for EasyScrutor

## GitHub Operations

### Always Use GitHub CLI (`gh`)
For all GitHub operations (creating PRs, issues, managing repositories), **always use the `gh` CLI** instead of GitHub API tools or MCP GitHub tools.

#### Examples:
- **Create PR**: `gh pr create --base <base> --head <head> --title "title" --body "description"`
- **List PRs**: `gh pr list`
- **View PR**: `gh pr view <number>`
- **Create Issue**: `gh issue create --title "title" --body "description"`
- **List Issues**: `gh issue list`

### Rationale
- Direct CLI integration with the repository
- Simpler authentication and configuration
- Consistent with developer workflow
- Better terminal-based experience

## Code Style

See [instructions/csharp.instructions.md](.github/instructions/csharp.instructions.md) for C# coding standards.
