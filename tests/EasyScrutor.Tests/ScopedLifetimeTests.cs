using EasyScrutor.Tests.TestServices;

namespace EasyScrutor.Tests
{
    [TestFixture]
    public class ScopedLifetimeTests
    {
        private ServiceProvider? _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddAdvancedDependencyInjection();
            _serviceProvider = services.BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
            _serviceProvider?.Dispose();
        }

        [Test]
        public void IScopedLifetime_ShouldReturnSameInstanceWithinScope()
        {
            // Arrange & Act
            using var scope = _serviceProvider!.CreateScope();
            var instance1 = scope.ServiceProvider.GetService<IScopedService>();
            var instance2 = scope.ServiceProvider.GetService<IScopedService>();

            // Assert
            Assert.That(instance1, Is.Not.Null);
            Assert.That(instance2, Is.Not.Null);
            Assert.That(instance1, Is.SameAs(instance2), "Scoped services should return the same instance within a scope");
        }

        [Test]
        public void IScopedLifetime_ShouldReturnDifferentInstancesAcrossScopes()
        {
            // Arrange & Act
            IScopedService? instance1;
            IScopedService? instance2;

            using (var scope = _serviceProvider!.CreateScope())
            {
                instance1 = scope.ServiceProvider.GetRequiredService<IScopedService>();
            }

            using (var scope = _serviceProvider!.CreateScope())
            {
                instance2 = scope.ServiceProvider.GetRequiredService<IScopedService>();
            }

            // Assert
            Assert.That(instance1, Is.Not.SameAs(instance2), "Scoped services should return different instances across scopes");
        }

        [Test]
        public void IScopedLifetime_ShouldReturnSameGuidWithinScope()
        {
            // Arrange & Act
            using var scope = _serviceProvider!.CreateScope();
            var instance1 = scope.ServiceProvider.GetRequiredService<IScopedService>();
            var instance2 = scope.ServiceProvider.GetRequiredService<IScopedService>();

            var message1 = instance1.GetMessage();
            var message2 = instance2.GetMessage();

            // Assert
            Assert.That(message1, Is.EqualTo(message2), "Scoped instances should have the same GUID within a scope");
        }

        [Test]
        public void IScopedLifetime_ShouldReturnDifferentGuidsAcrossScopes()
        {
            // Arrange & Act
            string? message1;
            string? message2;

            using (var scope = _serviceProvider!.CreateScope())
            {
                var instance = scope.ServiceProvider.GetRequiredService<IScopedService>();
                message1 = instance.GetMessage();
            }

            using (var scope = _serviceProvider!.CreateScope())
            {
                var instance = scope.ServiceProvider.GetRequiredService<IScopedService>();
                message2 = instance.GetMessage();
            }

            // Assert
            Assert.That(message1, Is.Not.EqualTo(message2), "Scoped instances should have different GUIDs across scopes");
        }

        [Test]
        public void ISelfScopedLifetime_ShouldReturnSameInstanceWithinScope()
        {
            // Arrange & Act
            using var scope = _serviceProvider!.CreateScope();
            var instance1 = scope.ServiceProvider.GetService<SelfScopedService>();
            var instance2 = scope.ServiceProvider.GetService<SelfScopedService>();

            // Assert
            Assert.That(instance1, Is.Not.Null);
            Assert.That(instance2, Is.Not.Null);
            Assert.That(instance1, Is.SameAs(instance2), "Self-scoped services should return the same instance within a scope");
        }

        [Test]
        public void ISelfScopedLifetime_ShouldReturnDifferentInstancesAcrossScopes()
        {
            // Arrange & Act
            SelfScopedService? instance1;
            SelfScopedService? instance2;

            using (var scope = _serviceProvider!.CreateScope())
            {
                instance1 = scope.ServiceProvider.GetRequiredService<SelfScopedService>();
            }

            using (var scope = _serviceProvider!.CreateScope())
            {
                instance2 = scope.ServiceProvider.GetRequiredService<SelfScopedService>();
            }

            // Assert
            Assert.That(instance1, Is.Not.SameAs(instance2), "Self-scoped services should return different instances across scopes");
        }

        [Test]
        public void ScopedService_WithNestedScopes_ShouldBehaviorCorrectly()
        {
            // Arrange & Act
            IScopedService? outerInstance1;
            IScopedService? outerInstance2;
            IScopedService? innerInstance1;
            IScopedService? innerInstance2;

            using (var outerScope = _serviceProvider!.CreateScope())
            {
                outerInstance1 = outerScope.ServiceProvider.GetRequiredService<IScopedService>();
                outerInstance2 = outerScope.ServiceProvider.GetRequiredService<IScopedService>();

                using (var innerScope = outerScope.ServiceProvider.CreateScope())
                {
                    innerInstance1 = innerScope.ServiceProvider.GetRequiredService<IScopedService>();
                    innerInstance2 = innerScope.ServiceProvider.GetRequiredService<IScopedService>();
                }
            }

            // Assert
            Assert.That(outerInstance1, Is.SameAs(outerInstance2), "Same instance within outer scope");
            Assert.That(innerInstance1, Is.SameAs(innerInstance2), "Same instance within inner scope");
            Assert.That(outerInstance1, Is.Not.SameAs(innerInstance1), "Different instances between outer and inner scope");
        }
    }
}
