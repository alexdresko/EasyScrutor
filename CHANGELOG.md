# Changelog

## [0.2.3](https://github.com/alexdresko/EasyScrutor/compare/v0.2.2...v0.2.3) (2025-12-19)


### Bug Fixes

* improve Release Please workflow and update documentation ([#19](https://github.com/alexdresko/EasyScrutor/issues/19)) ([a27d1b7](https://github.com/alexdresko/EasyScrutor/commit/a27d1b7ed87c7d7f499c9e41990f2047e0101454))

## [0.2.2](https://github.com/alexdresko/EasyScrutor/compare/v0.2.1...v0.2.2) (2025-12-18)


### Bug Fixes

* combine Release Please and NuGet publish into single workflow ([#17](https://github.com/alexdresko/EasyScrutor/issues/17)) ([45e70b9](https://github.com/alexdresko/EasyScrutor/commit/45e70b9a49904f46c415f2c7e42ad7b26e49851b))

## [0.2.1](https://github.com/alexdresko/EasyScrutor/compare/v0.2.0...v0.2.1) (2025-12-18)


### Features

* add Quick Start section to README for easier onboarding ([#15](https://github.com/alexdresko/EasyScrutor/issues/15)) ([6d3e551](https://github.com/alexdresko/EasyScrutor/commit/6d3e5510c3f546730d227074bf1874f0ca3aaa57))

## [0.2.0](https://github.com/alexdresko/EasyScrutor/compare/v0.1.0...v0.2.0) (2025-12-18)


### ⚠ BREAKING CHANGES

* The main registration method has been renamed from AddAdvancedDependencyInjection() to AddEasyScrutor() for better clarity and discoverability. Users must update their code to use the new method name.

### Features

* add troubleshooting section to examples documentation ([#8](https://github.com/alexdresko/EasyScrutor/issues/8)) ([04ceb27](https://github.com/alexdresko/EasyScrutor/commit/04ceb27cfba7c59355c628a53fa653a6c27768ad))
* rename AddAdvancedDependencyInjection to AddEasyScrutor ([#9](https://github.com/alexdresko/EasyScrutor/issues/9)) ([6eb18fe](https://github.com/alexdresko/EasyScrutor/commit/6eb18fe1d28d3c022e24ef707f2c69ab163e508c))


### Bug Fixes

* add missing benefit point to documentation ([bcca87f](https://github.com/alexdresko/EasyScrutor/commit/bcca87f7ec6f013b7aae7140e5cc8054447fae35))
* add missing benefit point to documentation ([acf0ac6](https://github.com/alexdresko/EasyScrutor/commit/acf0ac62ed9a1f75dc33bb675d80553c835ae328))
* update Release Please to use v*.*.* tag format and make NuGet workflow support both formats ([#13](https://github.com/alexdresko/EasyScrutor/issues/13)) ([be2caca](https://github.com/alexdresko/EasyScrutor/commit/be2caca1b2f0d44226a6e84ce9e209bf26d4534e))

## [0.1.0](https://github.com/alexdresko/EasyScrutor/compare/EasyScrutor-v0.0.7...EasyScrutor-v0.1.0) (2025-12-18)


### ⚠ BREAKING CHANGES

* The main registration method has been renamed from AddAdvancedDependencyInjection() to AddEasyScrutor() for better clarity and discoverability. Users must update their code to use the new method name.

### Features

* rename AddAdvancedDependencyInjection to AddEasyScrutor ([#9](https://github.com/alexdresko/EasyScrutor/issues/9)) ([6eb18fe](https://github.com/alexdresko/EasyScrutor/commit/6eb18fe1d28d3c022e24ef707f2c69ab163e508c))

## [0.0.7](https://github.com/alexdresko/EasyScrutor/compare/EasyScrutor-v0.0.6...EasyScrutor-v0.0.7) (2025-12-18)


### Features

* add troubleshooting section to examples documentation ([#8](https://github.com/alexdresko/EasyScrutor/issues/8)) ([04ceb27](https://github.com/alexdresko/EasyScrutor/commit/04ceb27cfba7c59355c628a53fa653a6c27768ad))


### Bug Fixes

* add missing benefit point to documentation ([bcca87f](https://github.com/alexdresko/EasyScrutor/commit/bcca87f7ec6f013b7aae7140e5cc8054447fae35))
* add missing benefit point to documentation ([acf0ac6](https://github.com/alexdresko/EasyScrutor/commit/acf0ac62ed9a1f75dc33bb675d80553c835ae328))

---

## Major Changes Since Fork

The following are the major changes made since forking from [Scrutor.AspNetCore](https://github.com/sefacan/Scrutor.AspNetCore):

- **Project renamed** from Scrutor.AspNetCore to **EasyScrutor** to better reflect its purpose and remove misleading framework-specific naming
- **Removed service locator pattern** implementation (anti-pattern) and all ASP.NET Core-specific dependencies, making the library compatible with any .NET application using dependency injection
- **Multi-framework support**: Added support for .NET 8.0, 9.0, and 10.0 target frameworks
- **Complete test coverage**: Added comprehensive NUnit test suite with 43 tests across 6 test classes covering all lifetime marker interfaces
- **Example applications**: Created working examples for ASP.NET Core (Blazor Server, MVC, Web API) and console/worker services demonstrating the library works with any .NET application
- **Advanced filtering documentation**: Added comprehensive documentation on assembly filtering for performance optimization
- **Developer experience improvements**:
  - Added .editorconfig and dotnet format support
  - Added C# Copilot instructions
  - XML documentation comments for all public APIs
  - Created package.json for easy task automation
- **CI/CD improvements**: Updated GitHub Actions workflows to latest versions, added Release Please automation for semantic versioning
- **Code quality improvements**: Fixed code scanning alerts (proper LINQ usage, resource disposal, variable assignments)
- **Code formatting standardization**: Configured consistent line endings and added formatting verification to CI workflows
- **Community health files**: Added CODE_OF_CONDUCT.md and CONTRIBUTING.md
- **Enhanced NuGet package**: Added package icon, README inclusion, and improved metadata with automatic release notes
- **Build quality improvements**: Enabled Source Link (allows developers to step into the library source code during debugging), deterministic builds (ensures identical binaries across different machines for security and verification), embedded debugging symbols, and proper compiler flags for optimal builds and diagnostics
- Maintained backward compatibility with all six lifetime marker interfaces (IScopedLifetime, ITransientLifetime, ISingletonLifetime, and their Self* variants)
