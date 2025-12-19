using EasyScrutor.Tests.TestServices;
using System.Reflection;

namespace EasyScrutor.Tests;

/// <summary>
/// Tests for assembly filtering methods like AddEasyScrutorForAssembliesStartingWith and AddEasyScrutorForAssembliesContaining.
/// </summary>
[TestFixture]
public class AssemblyFilterTests {
    [Test]
    public void AddEasyScrutorForAssembliesStartingWith_MatchingPrefix_ShouldRegisterServices() {
        // Arrange
        var services = new ServiceCollection();
        var testAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        var prefix = testAssemblyName!.Substring(0, 10); // Get first 10 characters as prefix

        // Act
        services.AddEasyScrutorForAssembliesStartingWith(prefix);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Not.Null);
        Assert.That(serviceProvider.GetService<ITransientService>(), Is.Not.Null);
        Assert.That(serviceProvider.GetService<IScopedService>(), Is.Not.Null);
    }

    [Test]
    public void AddEasyScrutorForAssembliesStartingWith_NonMatchingPrefix_ShouldNotRegisterServices() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddEasyScrutorForAssembliesStartingWith("NonExistentPrefix");
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Null);
        Assert.That(serviceProvider.GetService<ITransientService>(), Is.Null);
        Assert.That(serviceProvider.GetService<IScopedService>(), Is.Null);
    }

    [Test]
    public void AddEasyScrutorForAssembliesStartingWith_CaseInsensitive_ShouldRegisterServices() {
        // Arrange
        var services = new ServiceCollection();
        var testAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        var prefix = testAssemblyName!.Substring(0, 10).ToLower(); // Lowercase prefix

        // Act
        services.AddEasyScrutorForAssembliesStartingWith(prefix);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Not.Null, "Should be case-insensitive");
    }

    [Test]
    public void AddEasyScrutorForAssembliesStartingWith_ShouldReturnServiceCollection_ForChaining() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddEasyScrutorForAssembliesStartingWith("Test");

        // Assert
        Assert.That(result, Is.SameAs(services), "Should return the same ServiceCollection for method chaining");
    }

    [Test]
    public void AddEasyScrutorForAssembliesContaining_MatchingText_ShouldRegisterServices() {
        // Arrange
        var services = new ServiceCollection();
        var containingText = "Scrutor"; // Should be part of the assembly name

        // Act
        services.AddEasyScrutorForAssembliesContaining(containingText);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Not.Null);
        Assert.That(serviceProvider.GetService<ITransientService>(), Is.Not.Null);
        Assert.That(serviceProvider.GetService<IScopedService>(), Is.Not.Null);
    }

    [Test]
    public void AddEasyScrutorForAssembliesContaining_NonMatchingText_ShouldNotRegisterServices() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddEasyScrutorForAssembliesContaining("NonExistentText");
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Null);
        Assert.That(serviceProvider.GetService<ITransientService>(), Is.Null);
        Assert.That(serviceProvider.GetService<IScopedService>(), Is.Null);
    }

    [Test]
    public void AddEasyScrutorForAssembliesContaining_CaseInsensitive_ShouldRegisterServices() {
        // Arrange
        var services = new ServiceCollection();
        var containingText = "SCRUTOR"; // Uppercase

        // Act
        services.AddEasyScrutorForAssembliesContaining(containingText);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Not.Null, "Should be case-insensitive");
    }

    [Test]
    public void AddEasyScrutorForAssembliesContaining_ShouldReturnServiceCollection_ForChaining() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddEasyScrutorForAssembliesContaining("Test");

        // Assert
        Assert.That(result, Is.SameAs(services), "Should return the same ServiceCollection for method chaining");
    }

    [Test]
    public void AddEasyScrutorForAssembliesContaining_PartialMatch_ShouldRegisterServices() {
        // Arrange
        var services = new ServiceCollection();
        var testAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        // Take middle part of assembly name
        var partialText = testAssemblyName!.Substring(5, 5);

        // Act
        services.AddEasyScrutorForAssembliesContaining(partialText);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Not.Null, "Should match partial text");
    }

    [Test]
    public void AddEasyScrutorForAssembliesStartingWith_CalledMultipleTimes_ShouldSkipDuplicates() {
        // Arrange
        var services = new ServiceCollection();
        var testAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        var prefix = testAssemblyName!.Substring(0, 10);

        // Act
        services.AddEasyScrutorForAssembliesStartingWith(prefix);
        services.AddEasyScrutorForAssembliesStartingWith(prefix);

        // Assert - Should only have one registration due to Skip strategy
        var singletonServices = services.Where(s => s.ServiceType == typeof(ISingletonService)).ToList();
        Assert.That(singletonServices.Count, Is.EqualTo(1), "Should not register duplicate services");
    }

    [Test]
    public void AddEasyScrutorForAssembliesContaining_CalledMultipleTimes_ShouldSkipDuplicates() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddEasyScrutorForAssembliesContaining("Scrutor");
        services.AddEasyScrutorForAssembliesContaining("Scrutor");

        // Assert - Should only have one registration due to Skip strategy
        var singletonServices = services.Where(s => s.ServiceType == typeof(ISingletonService)).ToList();
        Assert.That(singletonServices.Count, Is.EqualTo(1), "Should not register duplicate services");
    }

    [Test]
    public void AddEasyScrutorForAssembliesStartingWith_AllLifetimeInterfaces_ShouldBeRegistered() {
        // Arrange
        var services = new ServiceCollection();
        var testAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        var prefix = testAssemblyName!.Substring(0, 10);

        // Act
        services.AddEasyScrutorForAssembliesStartingWith(prefix);
        var serviceProvider = services.BuildServiceProvider();

        // Assert - Verify all 6 lifetime interfaces are working
        using var scope = serviceProvider.CreateScope();

        Assert.That(scope.ServiceProvider.GetService<ISingletonService>(), Is.Not.Null, "ISingletonLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<SelfSingletonService>(), Is.Not.Null, "ISelfSingletonLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<ITransientService>(), Is.Not.Null, "ITransientLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<SelfTransientService>(), Is.Not.Null, "ISelfTransientLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<IScopedService>(), Is.Not.Null, "IScopedLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<SelfScopedService>(), Is.Not.Null, "ISelfScopedLifetime should work");
    }

    [Test]
    public void AddEasyScrutorForAssembliesContaining_AllLifetimeInterfaces_ShouldBeRegistered() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddEasyScrutorForAssembliesContaining("Scrutor");
        var serviceProvider = services.BuildServiceProvider();

        // Assert - Verify all 6 lifetime interfaces are working
        using var scope = serviceProvider.CreateScope();

        Assert.That(scope.ServiceProvider.GetService<ISingletonService>(), Is.Not.Null, "ISingletonLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<SelfSingletonService>(), Is.Not.Null, "ISelfSingletonLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<ITransientService>(), Is.Not.Null, "ITransientLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<SelfTransientService>(), Is.Not.Null, "ISelfTransientLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<IScopedService>(), Is.Not.Null, "IScopedLifetime should work");
        Assert.That(scope.ServiceProvider.GetService<SelfScopedService>(), Is.Not.Null, "ISelfScopedLifetime should work");
    }

    [Test]
    public void AddEasyScrutorForAssembliesStartingWith_EmptyPrefix_ShouldMatchAllAssemblies() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddEasyScrutorForAssembliesStartingWith("");
        var serviceProvider = services.BuildServiceProvider();

        // Assert - Empty prefix matches everything
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Not.Null);
    }

    [Test]
    public void AddEasyScrutorForAssembliesContaining_EmptyText_ShouldMatchAllAssemblies() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddEasyScrutorForAssembliesContaining("");
        var serviceProvider = services.BuildServiceProvider();

        // Assert - Empty text matches everything
        Assert.That(serviceProvider.GetService<ISingletonService>(), Is.Not.Null);
    }
}
