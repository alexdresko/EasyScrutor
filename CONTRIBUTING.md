# Contributing to EasyScrutor

Thank you for your interest in contributing to EasyScrutor! We welcome contributions from the community.

## Getting Started

1. Fork the repository
2. Clone your fork locally
3. Create a new branch for your feature or bug fix
4. Make your changes
5. Test your changes
6. Submit a pull request

## Development Setup

### Prerequisites

- .NET SDK (see [global.json](global.json) for the required version)
- A code editor (Visual Studio, VS Code, or Rider recommended)

### Building the Project

```bash
dotnet restore
dotnet build
```

### Running Tests

```bash
dotnet test
```

All tests should pass before submitting a pull request.

## Making Changes

### Code Style

- Follow the existing code style in the project
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and concise

### Commit Messages

- Use clear and descriptive commit messages
- Start with a verb in the present tense (e.g., "Add", "Fix", "Update")
- Reference issue numbers when applicable

### Pull Requests

1. **Title**: Use a clear, descriptive title
2. **Description**: Explain what changes you made and why
3. **Tests**: Include tests for new features or bug fixes
4. **Documentation**: Update documentation if needed

## Reporting Issues

When reporting issues, please include:

- A clear description of the problem
- Steps to reproduce the issue
- Expected vs actual behavior
- Your environment (OS, .NET version, etc.)

## Questions?

If you have questions about contributing, feel free to open an issue for discussion.

## License

By contributing to EasyScrutor, you agree that your contributions will be licensed under the same license as the project (see [LICENSE](LICENSE)).
