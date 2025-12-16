using EasyScrutor.Tests.TestServices;

namespace EasyScrutor.Tests;

[TestFixture]
public class TransientLifetimeTests {
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
    public void ITransientLifetime_ShouldRegisterAsTransient() {
        // Arrange & Act
        var instance1 = _serviceProvider!.GetService<ITransientService>();
        var instance2 = _serviceProvider!.GetService<ITransientService>();

        // Assert
        Assert.That(instance1, Is.Not.Null);
        Assert.That(instance2, Is.Not.Null);
        Assert.That(instance1, Is.Not.SameAs(instance2), "Transient services should return different instances");
    }

    [Test]
    public void ITransientLifetime_ShouldReturnDifferentGuids() {
        // Arrange & Act
        var instance1 = _serviceProvider!.GetRequiredService<ITransientService>();
        var instance2 = _serviceProvider!.GetRequiredService<ITransientService>();

        var message1 = instance1.GetMessage();
        var message2 = instance2.GetMessage();

        // Assert
        Assert.That(message1, Is.Not.EqualTo(message2), "Transient instances should have different GUIDs");
    }

    [Test]
    public void ISelfTransientLifetime_ShouldRegisterAsTransient() {
        // Arrange & Act
        var instance1 = _serviceProvider!.GetService<SelfTransientService>();
        var instance2 = _serviceProvider!.GetService<SelfTransientService>();

        // Assert
        Assert.That(instance1, Is.Not.Null);
        Assert.That(instance2, Is.Not.Null);
        Assert.That(instance1, Is.Not.SameAs(instance2), "Self-transient services should return different instances");
    }

    [Test]
    public void ISelfTransientLifetime_ShouldReturnDifferentGuids() {
        // Arrange & Act
        var instance1 = _serviceProvider!.GetRequiredService<SelfTransientService>();
        var instance2 = _serviceProvider!.GetRequiredService<SelfTransientService>();

        var message1 = instance1.GetMessage();
        var message2 = instance2.GetMessage();

        // Assert
        Assert.That(message1, Is.Not.EqualTo(message2), "Self-transient instances should have different GUIDs");
    }

    [Test]
    public void TransientService_ShouldCreateNewInstancesAcrossScopes() {
        // Arrange & Act
        string? message1;
        string? message2;
        string? message3;

        using (var scope = _serviceProvider!.CreateScope()) {
            var instance = scope.ServiceProvider.GetRequiredService<ITransientService>();
            message1 = instance.GetMessage();
        }

        using (var scope = _serviceProvider!.CreateScope()) {
            var instance1 = scope.ServiceProvider.GetRequiredService<ITransientService>();
            var instance2 = scope.ServiceProvider.GetRequiredService<ITransientService>();
            message2 = instance1.GetMessage();
            message3 = instance2.GetMessage();
        }

        // Assert
        Assert.That(message1, Is.Not.EqualTo(message2), "Transient should create new instances across scopes");
        Assert.That(message2, Is.Not.EqualTo(message3), "Transient should create new instances within the same scope");
    }

    [Test]
    public void TransientService_MultipleResolutions_ShouldAllBeDifferent() {
        // Arrange
        var instances = new List<ITransientService>();

        // Act
        for (int i = 0; i < 5; i++) {
            instances.Add(_serviceProvider!.GetRequiredService<ITransientService>());
        }

        // Assert
        var messages = instances.Select(i => i.GetMessage()).ToList();
        var uniqueMessages = messages.Distinct().ToList();

        Assert.That(uniqueMessages.Count, Is.EqualTo(5), "All transient instances should be unique");
    }
}