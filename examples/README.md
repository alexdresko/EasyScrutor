# EasyScrutor Examples

This directory contains example projects demonstrating how to use **EasyScrutor** for automatic dependency injection registration in different ASP.NET Core application types.

## What is EasyScrutor?

EasyScrutor is a dependency injection helper package that automatically scans and registers services based on marker interfaces, eliminating the need for manual service registration in `Program.cs`.

## How It Works

Instead of manually registering services like this:
```csharp
builder.Services.AddScoped<IMyService, MyService>();
builder.Services.AddSingleton<IAnotherService, AnotherService>();
```

You simply:
1. Implement marker interfaces on your services (`IScopedLifetime`, `ISingletonLifetime`, `ITransientLifetime`)
2. Call `AddAdvancedDependencyInjection()` once
3. All services are automatically discovered and registered!

## Available Examples

### 1. WebApiExample
A minimal Web API demonstrating:
- Auto-registration of scoped and singleton services
- Service injection in minimal API endpoints
- Custom endpoint using injected services

**Run:** `dotnet run --project WebApiExample`
**Test:** Navigate to `/greeting/YourName` to see auto-registered services in action

### 2. MvcExample
An MVC application showing:
- Auto-registration of transient services
- Service injection in MVC controllers
- Using services in views

**Run:** `dotnet run --project MvcExample`
**View:** Home page displays message from auto-registered service

### 3. BlazorServerExample
A Blazor Server app demonstrating:
- Auto-registration of singleton services
- Service injection in Blazor components
- Shared state across users

**Run:** `dotnet run --project BlazorServerExample`
**Try:** Counter page uses a singleton service shared across all sessions

### 4. ConsoleHostExample
A generic host worker service (console application) demonstrating:
- Auto-registration in non-web applications
- Background worker using auto-registered services
- Singleton metrics collector shared across the application
- Scoped data processor

**Run:** `dotnet run --project ConsoleHostExample`
**See:** Console output showing auto-registered services in action

## Usage Pattern

### Step 1: Create Your Service Interface and Implementation

```csharp
// Interface
public interface IMyService
{
    string DoSomething();
}

// Implementation - Add the appropriate lifetime marker interface
public class MyService : IMyService, IScopedLifetime
{
    public string DoSomething() => "Hello from auto-registered service!";
}
```

### Step 2: Register Services in Program.cs

**For Web Applications (API, MVC, Blazor):**
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add all your other services...
builder.Services.AddControllers();

// Add EasyScrutor - This scans and registers all services
builder.Services.AddAdvancedDependencyInjection();

var app = builder.Build();

// Use advanced dependency injection
app.UseAdvancedDependencyInjection();

app.Run();
```

**For Generic Host / Console Applications:**
```csharp
var builder = Host.CreateApplicationBuilder(args);

// Add EasyScrutor - This scans and registers all services
builder.Services.AddAdvancedDependencyInjection();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

### Step 3: Use Your Services Anywhere

```csharp
public class MyController : Controller
{
    private readonly IMyService _myService;

    public MyController(IMyService myService)
    {
        _myService = myService; // Automatically injected!
    }
}
```

## Lifetime Marker Interfaces

| Interface | Lifetime | Use Case |
|-----------|----------|----------|
| `IScopedLifetime` | Scoped | Services that should live for the duration of a request |
| `ISingletonLifetime` | Singleton | Services that should be created once and shared across the app |
| `ITransientLifetime` | Transient | Services that should be created new each time they're requested |
| `ISelfScopedLifetime` | Scoped | Register the service as itself (not interface) with scoped lifetime |
| `ISelfSingletonLifetime` | Singleton | Register the service as itself (not interface) with singleton lifetime |
| `ISelfTransientLifetime` | Transient | Register the service as itself (not interface) with transient lifetime |

## Benefits

✅ **Cleaner Code** - No more cluttered Program.cs with dozens of service registrations  
✅ **Convention-Based** - Simply implement an interface to define the lifetime  
✅ **Type-Safe** - Compile-time checked, no magic strings  
✅ **Maintainable** - Services declare their own lifetime alongside their implementation  
✅ **Flexible** - Can still manually register services when needed  
✅ **Discoverable** - Easy to find all services by searching for lifetime interfaces

## Building and Running

Build all examples:
```bash
dotnet build
```

Run a specific example:
```bash
dotnet run --project WebApiExample
dotnet run --project MvcExample
dotnet run --project BlazorServerExample
dotnet run --project ConsoleHostExample
```

## Learn More

For more information, visit the [EasyScrutor GitHub repository](https://github.com/alexdresko/EasyScrutor).
