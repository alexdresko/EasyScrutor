## EasyScrutor

ASP.NET Core [Scrutor](https://github.com/khellang/Scrutor) extension for automatic registration of classes inherited from IScopedLifetime, ISelfScopedLifetime, ITransientLifetime, ISelfTransientLifetime, ISingletonLifetime, ISelfSingletonLifetime

### About This Project

EasyScrutor is a modernized fork of the original [Scrutor.AspNetCore](https://github.com/sefacan/Scrutor.AspNetCore) project. The main goals of this fork were to:

- **Modernize the library** with updated dependencies and best practices
- **Remove the misleading "AspNetCore" from the name** - while this library works great with ASP.NET Core, it also works with any .NET application using dependency injection (console apps, worker services, etc.)
- **Continue development** with a clear path forward, as discussions with the original maintainer indicated the original project might not receive active updates

The original project was created by [sefacan](https://github.com/sefacan) and provided a simple, convention-based approach to dependency injection. This fork maintains that simplicity while ensuring compatibility with modern .NET versions.

### Build Status
| Build server    | Platform       | Status      |
|-----------------|----------------|-------------|
| Github Actions  | All            |![](https://github.com/alexdresko/EasyScrutor/workflows/.NET%20Core%20CI/badge.svg) |

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

**ASP.NET Core:**

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add EasyScrutor - automatically scans and registers services
builder.Services.AddAdvancedDependencyInjection();

var app = builder.Build();
app.Run();
```

**Console/Worker Service:**

```csharp
var builder = Host.CreateApplicationBuilder(args);

// Add EasyScrutor - automatically scans and registers services
builder.Services.AddAdvancedDependencyInjection();

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

By default, `AddAdvancedDependencyInjection()` scans all assemblies in your application's dependency context. For better performance, especially in large applications, you can filter which assemblies to scan:

```csharp
builder.Services.AddAdvancedDependencyInjection(assembly =>
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
builder.Services.AddAdvancedDependencyInjection(assembly =>
    assembly.FullName?.StartsWith("MyCompany.MyApp", StringComparison.Ordinal) == true);
```

**Or exclude test/development assemblies in production:**

```csharp
builder.Services.AddAdvancedDependencyInjection(assembly =>
{
    var name = assembly.FullName ?? string.Empty;
    return !name.Contains("Tests") &&
           !name.Contains("Mock") &&
           !name.StartsWith("Microsoft.") &&
           !name.StartsWith("System.");
});
```

This can significantly improve application startup time by reducing the number of assemblies scanned for service registration.

Hello
