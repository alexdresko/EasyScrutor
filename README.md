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
| Azure CI Pipelines  | All            |![](https://dev.azure.com/alexdresko/EasyScrutor/_apis/build/status/alexdresko.EasyScrutor?branchName=master) |
| Github Actions  | All            |![](https://github.com/alexdresko/EasyScrutor/workflows/.NET%20Core%20CI/badge.svg) |
| Travis CI       | Linux  |![](https://travis-ci.com/alexdresko/EasyScrutor.svg?branch=master) |

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

```csharp
public void ConfigureServices(IServiceCollection services)
{
    //add to the end of the method
    services.AddAdvancedDependencyInjection();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //add to the end of the method
    app.UseAdvancedDependencyInjection();
}

//usage without constructor classes
var service = ServiceLocator.Context.GetService<MyClass>();
```
