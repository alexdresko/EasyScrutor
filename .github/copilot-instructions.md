# GitHub Copilot Instructions for EasyScrutor

## Commit Message Format

### CRITICAL: Always Use Conventional Commits

**All commits and PR titles MUST follow [Conventional Commits](https://www.conventionalcommits.org/) format** to ensure Release-Please automation works correctly.

#### Format
```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

#### Required Types
- `feat:` - New features (triggers minor version bump)
- `fix:` - Bug fixes (triggers patch version bump)
- `chore:` - Maintenance tasks (no version bump unless breaking change)
- `docs:` - Documentation changes
- `test:` - Test additions or modifications
- `refactor:` - Code refactoring
- `perf:` - Performance improvements
- `ci:` - CI/CD configuration changes
- `build:` - Build system changes
- `style:` - Code style/formatting changes

#### Breaking Changes
- Add `!` after type/scope: `feat!: breaking change` OR
- Add `BREAKING CHANGE:` in footer (triggers major version bump)

#### Examples
- `feat: add support for keyed services`
- `fix: resolve null reference in service registration`
- `chore: update dependencies`
- `docs: improve README quick start section`
- `feat!: change method signature for AddEasyScrutor`

#### PR Titles
When creating PRs with `gh pr create`, the `--title` MUST use Conventional Commits format because it becomes the merge commit message:

```bash
gh pr create --base master --head feature-branch \
  --title "feat: add new registration options" \
  --body "Description of changes..."
```

**❌ Bad:** `gh pr create --title "Miscellaneous improvements"`
**✅ Good:** `gh pr create --title "chore: miscellaneous improvements to build and docs"`

## GitHub Operations

### Always Use GitHub CLI (`gh`)
For all GitHub operations (creating PRs, issues, managing repositories), **always use the `gh` CLI** instead of GitHub API tools or MCP GitHub tools.

#### Examples
- **Create PR**: `gh pr create --base <base> --head <head> --title "type: description" --body "details"`
- **List PRs**: `gh pr list`
- **View PR**: `gh pr view <number>`
- **Create Issue**: `gh issue create --title "title" --body "description"`
- **List Issues**: `gh issue list`

#### Rationale
- Direct CLI integration with the repository
- Simpler authentication and configuration
- Consistent with developer workflow
- Better terminal-based experience

## Code Style

See [instructions/csharp.instructions.md](.github/instructions/csharp.instructions.md) for C# coding standards.
