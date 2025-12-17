using EasyScrutor.Tests.TestServices;

namespace EasyScrutor.Tests;

[TestFixture]
public class SingletonLifetimeTests {
    private ServiceProvider? _serviceProvider;

    [SetUp]
    public void Setup() {
        var services = new ServiceCollection();
        services.AddAdvancedDependencyInjection();
        _serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown() {
        _serviceProvider?.Dispose();
    }

    [Test]
    public void ISingletonLifetime_ShouldRegisterAsSingleton() {
        // Arrange & Act
        var instance1 = _serviceProvider!.GetService<ISingletonService>();
        var instance2 = _serviceProvider!.GetService<ISingletonService>();

        // Assert
        Assert.That(instance1, Is.Not.Null);
        Assert.That(instance2, Is.Not.Null);
        Assert.That(instance1, Is.SameAs(instance2), "Singleton services should return the same instance");
    }

    [Test]
    public void ISingletonLifetime_ShouldReturnSameGuid() {
        // Arrange & Act
        var instance1 = _serviceProvider!.GetRequiredService<ISingletonService>();
        var instance2 = _serviceProvider!.GetRequiredService<ISingletonService>();

        var message1 = instance1.GetMessage();
        var message2 = instance2.GetMessage();

        // Assert
        Assert.That(message1, Is.EqualTo(message2), "Singleton instances should have the same GUID");
    }

    [Test]
    public void ISelfSingletonLifetime_ShouldRegisterAsSingleton() {
        // Arrange & Act
        var instance1 = _serviceProvider!.GetService<SelfSingletonService>();
        var instance2 = _serviceProvider!.GetService<SelfSingletonService>();

        // Assert
        Assert.That(instance1, Is.Not.Null);
        Assert.That(instance2, Is.Not.Null);
        Assert.That(instance1, Is.SameAs(instance2), "Self-singleton services should return the same instance");
    }

    [Test]
    public void ISelfSingletonLifetime_ShouldReturnSameGuid() {
        // Arrange & Act
        var instance1 = _serviceProvider!.GetRequiredService<SelfSingletonService>();
        var instance2 = _serviceProvider!.GetRequiredService<SelfSingletonService>();

        var message1 = instance1.GetMessage();
        var message2 = instance2.GetMessage();

        // Assert
        Assert.That(message1, Is.EqualTo(message2), "Self-singleton instances should have the same GUID");
    }

    [Test]
    public void SingletonService_ShouldPersistAcrossScopes() {
        // Arrange & Act
        string? message1;
        string? message2;

        using (var scope = _serviceProvider!.CreateScope()) {
            var instance = scope.ServiceProvider.GetRequiredService<ISingletonService>();
            message1 = instance.GetMessage();
        }

        using (var scope = _serviceProvider!.CreateScope()) {
            var instance = scope.ServiceProvider.GetRequiredService<ISingletonService>();
            message2 = instance.GetMessage();
        }

        // Assert
        Assert.That(message1, Is.EqualTo(message2), "Singleton should persist across different scopes");
    }
}
