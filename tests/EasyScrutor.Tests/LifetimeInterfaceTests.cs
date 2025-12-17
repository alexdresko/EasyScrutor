namespace EasyScrutor.Tests;

[TestFixture]
public class LifetimeInterfaceTests {
    [Test]
    public void ISingletonLifetime_InterfaceShouldExist() {
        // Arrange & Act
        var type = typeof(ISingletonLifetime);

        // Assert
        Assert.That(type, Is.Not.Null);
        Assert.That(type.IsInterface, Is.True);
        Assert.That(type.Namespace, Is.EqualTo("EasyScrutor"));
    }

    [Test]
    public void ISelfSingletonLifetime_InterfaceShouldExist() {
        // Arrange & Act
        var type = typeof(ISelfSingletonLifetime);

        // Assert
        Assert.That(type, Is.Not.Null);
        Assert.That(type.IsInterface, Is.True);
        Assert.That(type.Namespace, Is.EqualTo("EasyScrutor"));
    }

    [Test]
    public void ITransientLifetime_InterfaceShouldExist() {
        // Arrange & Act
        var type = typeof(ITransientLifetime);

        // Assert
        Assert.That(type, Is.Not.Null);
        Assert.That(type.IsInterface, Is.True);
        Assert.That(type.Namespace, Is.EqualTo("EasyScrutor"));
    }

    [Test]
    public void ISelfTransientLifetime_InterfaceShouldExist() {
        // Arrange & Act
        var type = typeof(ISelfTransientLifetime);

        // Assert
        Assert.That(type, Is.Not.Null);
        Assert.That(type.IsInterface, Is.True);
        Assert.That(type.Namespace, Is.EqualTo("EasyScrutor"));
    }

    [Test]
    public void IScopedLifetime_InterfaceShouldExist() {
        // Arrange & Act
        var type = typeof(IScopedLifetime);

        // Assert
        Assert.That(type, Is.Not.Null);
        Assert.That(type.IsInterface, Is.True);
        Assert.That(type.Namespace, Is.EqualTo("EasyScrutor"));
    }

    [Test]
    public void ISelfScopedLifetime_InterfaceShouldExist() {
        // Arrange & Act
        var type = typeof(ISelfScopedLifetime);

        // Assert
        Assert.That(type, Is.Not.Null);
        Assert.That(type.IsInterface, Is.True);
        Assert.That(type.Namespace, Is.EqualTo("EasyScrutor"));
    }

    [Test]
    public void AllLifetimeInterfaces_ShouldHaveNoMembers() {
        // Arrange
        var lifetimeTypes = new[]
        {
            typeof(ISingletonLifetime),
            typeof(ISelfSingletonLifetime),
            typeof(ITransientLifetime),
            typeof(ISelfTransientLifetime),
            typeof(IScopedLifetime),
            typeof(ISelfScopedLifetime)
        };

        // Act & Assert
        foreach (var type in lifetimeTypes) {
            var members = type.GetMembers();
            // Only inherited members from object should be present
            Assert.That(members.Length, Is.EqualTo(0),
                $"{type.Name} should be a marker interface with no members");
        }
    }

    [Test]
    public void LifetimeInterfaces_ShouldNotInheritFromEachOther() {
        // Arrange
        var lifetimeTypes = new[]
        {
            typeof(ISingletonLifetime),
            typeof(ISelfSingletonLifetime),
            typeof(ITransientLifetime),
            typeof(ISelfTransientLifetime),
            typeof(IScopedLifetime),
            typeof(ISelfScopedLifetime)
        };

        // Act & Assert
        foreach (var type1 in lifetimeTypes) {
            foreach (var type2 in lifetimeTypes.Where(type2 => type2 != type1)) {
                Assert.That(type1.IsAssignableFrom(type2), Is.False,
                    $"{type2.Name} should not inherit from {type1.Name}");
            }
        }
    }
}
