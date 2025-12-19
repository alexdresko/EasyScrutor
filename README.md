## EasyScrutor

ASP.NET Core [Scrutor](https://github.com/khellang/Scrutor) extension for automatic registration of classes inherited from IScopedLifetime, ISelfScopedLifetime, ITransientLifetime, ISelfTransientLifetime, ISingletonLifetime, ISelfSingletonLifetime

### About This Project

EasyScrutor is a modernized fork of the original [Scrutor.AspNetCore](https://github.com/sefacan/Scrutor.AspNetCore) project. The main goals of this fork were to:

- **Modernize the library** with updated dependencies and best practices
- **Remove the misleading "AspNetCore" from the name** - while this library works great with ASP.NET Core, it also works with any .NET application using dependency injection (console apps, worker services, etc.)
- **Continue development** with a clear path forward, as discussions with the original maintainer indicated the original project might not receive active updates

The original project was created by [sefacan](https://github.com/sefacan) and provided a simple, convention-based approach to dependency injection. This fork maintains that simplicity while ensuring compatibility with modern .NET versions.

**Major improvements in Scrutor dependency (v4.2.0 -> v7.0.0):**
- **Keyed service registration support** - Leverage .NET 8's keyed DI for more advanced scenarios
- **Interface filtering for `AsSelfWithInterfaces`** - Better control over which interfaces to register
- **Security fix** - Resolved System.Text.Json vulnerability through DependencyModel update
- **Performance improvements** - Optimized decoration performance for applications with many services
- **Generic type support** - Better handling of generic ServiceDescriptor from C# 11+
- **.NET 6 and .NET 8 support** - Full compatibility with modern .NET versions
- **Bug fixes**:
  - Fixed multiple decoration layers for generic types
  - Made DecoratedType and IsDecorated method public for extensibility
  - Improved error handling when scanning assemblies
  - Fixed generic type creation exceptions

### Build Status
| Build server    | Platform       | Status      |
|-----------------|----------------|-------------|
| Github Actions  | All            | ![Build Status](https://github.com/alexdresko/EasyScrutor/workflows/.NET%20Core%20CI/badge.svg) |
| Code Coverage   | All            | [![codecov](https://codecov.io/gh/alexdresko/EasyScrutor/branch/master/graph/badge.svg)](https://codecov.io/gh/alexdresko/EasyScrutor) |
| NuGet           | Package        | [![NuGet](https://img.shields.io/nuget/v/EasyScrutor.svg)](https://www.nuget.org/packages/EasyScrutor/) |
| NuGet           | Downloads      | [![NuGet Downloads](https://img.shields.io/nuget/dt/EasyScrutor.svg)](https://www.nuget.org/packages/EasyScrutor/) |
| GitHub          | Release        | [![GitHub Release](https://img.shields.io/github/release/alexdresko/EasyScrutor.svg)](https://github.com/alexdresko/EasyScrutor/releases) |
| License         | MIT            | [![License](https://img.shields.io/github/license/alexdresko/EasyScrutor.svg)](LICENSE) |

## Quick Start

Get started with EasyScrutor in just 3 steps:

**1. Install the package:**
```bash
dotnet add package EasyScrutor
```

**2. Mark your services with a lifetime interface:**
```csharp
public interface IMyService { string GetMessage(); }
public class MyService : IMyService, IScopedLifetime
{
    public string GetMessage() => "Hello from EasyScrutor!";
}
```

**3. Register in Program.cs:**
```csharp
builder.Services.AddEasyScrutor();
```

That's it! Your services are now automatically registered and ready to inject anywhere in your application.

## Installation

Install the [EasyScrutor NuGet Package](https://www.nuget.org/packages/EasyScrutor).

### Package Manager Console

```
Install-Package EasyScrutor
```

### .NET Core CLI

```
dotnet add package EasyScrutor
```

## Usage

EasyScrutor automatically discovers and registers your services by scanning for classes that implement lifetime marker interfaces.

### Step 1: Mark your service classes

Implement one of the lifetime marker interfaces on your service classes:
- `IScopedLifetime` - Registers as Scoped
- `ITransientLifetime` - Registers as Transient
- `ISingletonLifetime` - Registers as Singleton
- `ISelfScopedLifetime` - Registers as Scoped (self-registration, no interface)
- `ISelfTransientLifetime` - Registers as Transient (self-registration, no interface)
- `ISelfSingletonLifetime` - Registers as Singleton (self-registration, no interface)

```csharp
using EasyScrutor;

public interface IDataService
{
    Task<string> GetDataAsync();
}

// This class will be automatically registered as Scoped
public class DataService : IDataService, IScopedLifetime
{
    public async Task<string> GetDataAsync()
    {
        return await Task.FromResult("Hello from DataService!");
    }
}
```

### Step 2: Register EasyScrutor in your application

> **⚠️ BREAKING CHANGE**: The method signature has been updated. Please use `AddEasyScrutor()` instead of `AddAdvancedDependencyInjection()` for clearer, more intuitive naming.

**ASP.NET Core:**

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add EasyScrutor - automatically scans and registers services
builder.Services.AddEasyScrutor();

var app = builder.Build();
app.Run();
```

**Console/Worker Service:**

```csharp
var builder = Host.CreateApplicationBuilder(args);

// Add EasyScrutor - automatically scans and registers services
builder.Services.AddEasyScrutor();

var host = builder.Build();
host.Run();
```

### Step 3: Use your services

Services are injected automatically through constructor injection:

```csharp
public class MyController : ControllerBase
{
    private readonly IDataService _dataService;

    public MyController(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<IActionResult> Get()
    {
        var data = await _dataService.GetDataAsync();
        return Ok(data);
    }
}
```

That's it! No manual service registration needed - EasyScrutor handles it all for you.

## Advanced Usage

### Filtering Assemblies for Performance

By default, `AddEasyScrutor()` scans all assemblies in your application's dependency context. For better performance, especially in large applications, you can filter which assemblies to scan:

```csharp
builder.Services.AddEasyScrutor(assembly =>
{
    // Only scan assemblies from your application, exclude framework assemblies
    return !assembly.FullName?.StartsWith("Microsoft.", StringComparison.Ordinal) == true &&
           !assembly.FullName?.StartsWith("System.", StringComparison.Ordinal) == true &&
           !assembly.FullName?.StartsWith("netstandard", StringComparison.Ordinal) == true;
});
```

**Or target specific assemblies:**

```csharp
// Only scan assemblies matching your application name
builder.Services.AddEasyScrutor(assembly =>
    assembly.FullName?.StartsWith("MyCompany.MyApp", StringComparison.Ordinal) == true);
```

**Or exclude test/development assemblies in production:**

```csharp
builder.Services.AddEasyScrutor(assembly =>
{
    var name = assembly.FullName ?? string.Empty;
    return !name.Contains("Tests") &&
           !name.Contains("Mock") &&
           !name.StartsWith("Microsoft.") &&
           !name.StartsWith("System.");
});
```

This can significantly improve application startup time by reducing the number of assemblies scanned for service registration.

## Examples

See [examples/README.md](examples/README.md) for runnable sample apps (Web API, MVC, Blazor Server, and a console/worker host).

## Contributing

We welcome contributions! Please read our [CONTRIBUTING.md](CONTRIBUTING.md) guide to get started.

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for the complete version history and detailed list of changes since forking from [Scrutor.AspNetCore](https://github.com/sefacan/Scrutor.AspNetCore).
