# EasyScrutor.Tests

Comprehensive NUnit test suite for the EasyScrutor library.

## Overview

This test project provides full coverage of EasyScrutor's functionality, testing all six lifetime marker interfaces and their registration behaviors.

## Test Structure

### Test Files

1. **SingletonLifetimeTests.cs** - Tests for `ISingletonLifetime` and `ISelfSingletonLifetime`
   - Verifies singleton instances are reused across multiple resolutions
   - Validates singleton instances persist across scopes
   - Tests both interface-based and self-registration patterns

2. **TransientLifetimeTests.cs** - Tests for `ITransientLifetime` and `ISelfTransientLifetime`
   - Ensures new instances are created for each resolution
   - Validates transient behavior across scopes
   - Tests both interface-based and self-registration patterns

3. **ScopedLifetimeTests.cs** - Tests for `IScopedLifetime` and `ISelfScopedLifetime`
   - Verifies same instance within a scope
   - Ensures different instances across different scopes
   - Tests nested scope behavior
   - Tests both interface-based and self-registration patterns

4. **AdvancedDependencyInjectionTests.cs** - Tests for `AddAdvancedDependencyInjection` methods
   - Tests registration without predicates
   - Tests registration with assembly predicates
   - Validates registration strategy (Skip)
   - Tests duplicate registration handling
   - Tests complex service dependencies
   - Tests abstract class handling

5. **ServiceLifecycleIntegrationTests.cs** - Integration tests combining multiple lifetimes
   - Tests mixed lifetime scenarios
   - Validates dependency injection between different lifetime services
   - Tests service disposal behavior
   - Validates self-registration vs interface registration

6. **LifetimeInterfaceTests.cs** - Tests for the marker interfaces themselves
   - Validates all six lifetime interfaces exist
   - Ensures interfaces have no members (marker interfaces)
   - Validates interfaces don't inherit from each other

### Test Services

**TestServices.cs** - Contains test service implementations:
- `ISingletonService` / `SingletonService`
- `SelfSingletonService`
- `ITransientService` / `TransientService`
- `SelfTransientService`
- `IScopedService` / `ScopedService`
- `SelfScopedService`
- `IComplexService` / `ComplexService` (with dependencies)
- `BaseService` / `ConcreteService` (abstract/concrete test)
- Multiple implementation scenarios

## Running Tests

### Run all tests
```bash
dotnet test tests/EasyScrutor.Tests/EasyScrutor.Tests.csproj
```

### Run with detailed output
```bash
dotnet test tests/EasyScrutor.Tests/EasyScrutor.Tests.csproj --verbosity normal
```

### Run specific test class
```bash
dotnet test --filter "FullyQualifiedName~SingletonLifetimeTests"
```

### Run specific test
```bash
dotnet test --filter "FullyQualifiedName~ISingletonLifetime_ShouldRegisterAsSingleton"
```

## Test Coverage

The test suite covers:

✅ **All 6 Lifetime Interfaces**
- ISingletonLifetime
- ISelfSingletonLifetime
- ITransientLifetime
- ISelfTransientLifetime
- IScopedLifetime
- ISelfScopedLifetime

✅ **Registration Patterns**
- Interface-based registration (AsMatchingInterface)
- Self-registration (AsSelf)
- Registration with assembly predicates
- Registration strategy (Skip)

✅ **Service Resolution**
- Single instance resolution
- Multiple instance resolution
- Scoped resolution
- Cross-scope resolution
- Nested scope resolution

✅ **Dependency Injection**
- Services with dependencies
- Mixed lifetime dependencies
- Complex service graphs

✅ **Edge Cases**
- Abstract class handling
- Multiple implementations
- Duplicate registrations
- Service disposal

## Test Statistics

- **Total Tests**: 43
- **Test Classes**: 6
- **Test Services**: 12+
- **Coverage**: All public APIs and lifetime patterns

## CI/CD Integration

These tests are designed to run in continuous integration pipelines. The test project targets .NET 10.0 and uses:
- NUnit 4.3.0
- NUnit3TestAdapter 4.6.0
- Microsoft.NET.Test.Sdk 17.12.0

## Contributing

When adding new features to EasyScrutor:
1. Add corresponding test services to `TestServices.cs`
2. Create tests for the new functionality
3. Ensure all existing tests still pass
4. Update this README if new test categories are added
