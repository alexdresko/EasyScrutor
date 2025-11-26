## Scrutor.AspNetCore

 [Scrutor](https://github.com/khellang/Scrutor) extension for automatic registration of classes inherited from IScopedLifetime, ISelfScopedLifetime, ITransientLifetime, ISelfTransientLifetime, ISingletonLifetime, ISelfSingletonLifetime. Works with any .NET application including web apps, console apps, and worker services.

### Build Status
| Build server    | Platform       | Status      |
|-----------------|----------------|-------------|
| Azure CI Pipelines  | All            |![](https://dev.azure.com/fsefacan/Scrutor.AspNetCore/_apis/build/status/sefacan.Scrutor.AspNetCore?branchName=master) |
| Github Actions  | All            |![](https://github.com/sefacan/Scrutor.AspNetCore/workflows/.NET%20Core%20CI/badge.svg) |

### Requirements

- .NET 8.0 or .NET 9.0

## Installation

Install the [Scrutor.AspNetCore NuGet Package](https://www.nuget.org/packages/Scrutor.AspNetCore).

### Package Manager Console

```
Install-Package Scrutor.AspNetCore
```

### .NET Core CLI

```
dotnet add package Scrutor.AspNetCore
```

## Usage

### Minimal API / Web Application (Recommended)

```csharp
var builder = WebApplication.CreateBuilder(args);

// For web apps with HTTP context support
builder.AddAdvancedDependencyInjectionForWeb();

var app = builder.Build();

// Initialize ServiceLocator (optional, for static access)
app.UseAdvancedDependencyInjection();

app.Run();
```

### Console Application / Worker Service

```csharp
var builder = Host.CreateApplicationBuilder(args);

// For non-web apps
builder.AddAdvancedDependencyInjection();

var host = builder.Build();

// Initialize ServiceLocator (optional, for static access)
host.UseAdvancedDependencyInjection();

host.Run();
```

### Traditional Startup Class Pattern

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // For web apps
    services.AddAdvancedDependencyInjectionForWeb();
    
    // Or for non-web apps
    // services.AddAdvancedDependencyInjection();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Initialize ServiceLocator (optional)
    app.UseAdvancedDependencyInjection();
}
```

### Using ServiceLocator (Optional)

```csharp
// Access services without constructor injection
var service = ServiceLocator.Context.GetService<MyClass>();
```

## API Reference

### Service Collection Extensions

| Method | Description |
|--------|-------------|
| `AddAdvancedDependencyInjection()` | Core registration for any .NET app |
| `AddAdvancedDependencyInjectionForWeb()` | Includes HttpContextAccessor and ActionContextAccessor |

### Host Builder Extensions

| Method | Description |
|--------|-------------|
| `builder.AddAdvancedDependencyInjection()` | For `IHostApplicationBuilder` (console/worker) |
| `builder.AddAdvancedDependencyInjectionForWeb()` | For `WebApplicationBuilder` (web apps) |

### Application/Host Extensions

| Method | Description |
|--------|-------------|
| `app.UseAdvancedDependencyInjection()` | Initialize ServiceLocator for web apps |
| `host.UseAdvancedDependencyInjection()` | Initialize ServiceLocator for non-web apps |
