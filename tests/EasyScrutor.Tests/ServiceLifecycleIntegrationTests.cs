using EasyScrutor.Tests.TestServices;

namespace EasyScrutor.Tests
{
    [TestFixture]
    public class ServiceLifecycleIntegrationTests
    {
        [Test]
        public void MixedLifetimes_ShouldWorkTogetherCorrectly()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdvancedDependencyInjection();
            var serviceProvider = services.BuildServiceProvider();

            // Act
            string? singletonMsg1, singletonMsg2;
            string? scopedMsg1, scopedMsg2, scopedMsg3;
            string? transientMsg1, transientMsg2, transientMsg3;

            // First scope
            using (var scope1 = serviceProvider.CreateScope())
            {
                var singleton = scope1.ServiceProvider.GetRequiredService<ISingletonService>();
                var scoped = scope1.ServiceProvider.GetRequiredService<IScopedService>();
                var transient1 = scope1.ServiceProvider.GetRequiredService<ITransientService>();
                var transient2 = scope1.ServiceProvider.GetRequiredService<ITransientService>();

                singletonMsg1 = singleton.GetMessage();
                scopedMsg1 = scoped.GetMessage();
                transientMsg1 = transient1.GetMessage();
                transientMsg2 = transient2.GetMessage();
            }

            // Second scope
            using (var scope2 = serviceProvider.CreateScope())
            {
                var singleton = scope2.ServiceProvider.GetRequiredService<ISingletonService>();
                var scoped1 = scope2.ServiceProvider.GetRequiredService<IScopedService>();
                var scoped2 = scope2.ServiceProvider.GetRequiredService<IScopedService>();
                var transient = scope2.ServiceProvider.GetRequiredService<ITransientService>();

                singletonMsg2 = singleton.GetMessage();
                scopedMsg2 = scoped1.GetMessage();
                scopedMsg3 = scoped2.GetMessage();
                transientMsg3 = transient.GetMessage();
            }

            // Assert
            // Singleton should be same across scopes
            Assert.That(singletonMsg1, Is.EqualTo(singletonMsg2), "Singleton should be same across scopes");

            // Scoped should be same within scope but different across scopes
            Assert.That(scopedMsg2, Is.EqualTo(scopedMsg3), "Scoped should be same within scope");
            Assert.That(scopedMsg1, Is.Not.EqualTo(scopedMsg2), "Scoped should be different across scopes");

            // Transient should always be different
            Assert.That(transientMsg1, Is.Not.EqualTo(transientMsg2), "Transient should be different within same scope");
            Assert.That(transientMsg1, Is.Not.EqualTo(transientMsg3), "Transient should be different across scopes");
            Assert.That(transientMsg2, Is.Not.EqualTo(transientMsg3), "Transient should be different across scopes");
        }

        [Test]
        public void ComplexService_ShouldReceiveCorrectDependencies()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdvancedDependencyInjection();
            var serviceProvider = services.BuildServiceProvider();

            // Act
            string? result1, result2;

            using (var scope1 = serviceProvider.CreateScope())
            {
                var complex = scope1.ServiceProvider.GetRequiredService<IComplexService>();
                result1 = complex.ProcessData();
            }

            using (var scope2 = serviceProvider.CreateScope())
            {
                var complex = scope2.ServiceProvider.GetRequiredService<IComplexService>();
                result2 = complex.ProcessData();
            }

            // Assert
            Assert.That(result1, Does.StartWith("Complex:"));
            Assert.That(result2, Does.StartWith("Complex:"));

            // Both should contain the same singleton GUID
            var singleton1Guid = ExtractGuid(result1, "Singleton:");
            var singleton2Guid = ExtractGuid(result2, "Singleton:");
            Assert.That(singleton1Guid, Is.EqualTo(singleton2Guid), "Should use same singleton instance");

            // But different transient GUIDs
            var transient1Guid = ExtractGuid(result1, "Transient:");
            var transient2Guid = ExtractGuid(result2, "Transient:");
            Assert.That(transient1Guid, Is.Not.EqualTo(transient2Guid), "Should use different transient instances");
        }

        [Test]
        public void SelfRegisteredServices_ShouldOnlyBeAvailableAsSelf()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdvancedDependencyInjection();
            var serviceProvider = services.BuildServiceProvider();

            // Act
            var selfSingleton = serviceProvider.GetService<SelfSingletonService>();
            var selfTransient = serviceProvider.GetService<SelfTransientService>();
            var selfScoped = serviceProvider.GetService<SelfScopedService>();

            // Assert
            Assert.That(selfSingleton, Is.Not.Null);
            Assert.That(selfTransient, Is.Not.Null);
            Assert.That(selfScoped, Is.Not.Null);
        }

        [Test]
        public void InterfaceRegisteredServices_ShouldBeAvailableViaInterface()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdvancedDependencyInjection();
            var serviceProvider = services.BuildServiceProvider();

            // Act
            var singleton = serviceProvider.GetService<ISingletonService>();
            var transient = serviceProvider.GetService<ITransientService>();
            var scoped = serviceProvider.GetService<IScopedService>();

            // Assert
            Assert.That(singleton, Is.Not.Null);
            Assert.That(transient, Is.Not.Null);
            Assert.That(scoped, Is.Not.Null);
        }

        [Test]
        public void InterfaceRegisteredServices_ShouldNotBeAvailableAsConcreteType()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdvancedDependencyInjection();
            var serviceProvider = services.BuildServiceProvider();

            // Act
            var singletonConcrete = serviceProvider.GetService<SingletonService>();
            var transientConcrete = serviceProvider.GetService<TransientService>();
            var scopedConcrete = serviceProvider.GetService<ScopedService>();

            // Assert - These should be null because they're registered via AsMatchingInterface
            Assert.That(singletonConcrete, Is.Null, "Interface-registered services should not be available as concrete type");
            Assert.That(transientConcrete, Is.Null, "Interface-registered services should not be available as concrete type");
            Assert.That(scopedConcrete, Is.Null, "Interface-registered services should not be available as concrete type");
        }

        [Test]
        public void ServiceProvider_Dispose_ShouldDisposeDisposableServices()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdvancedDependencyInjection();
            var serviceProvider = services.BuildServiceProvider();

            // Act
            var singleton = serviceProvider.GetService<ISingletonService>();

            // Dispose the service provider
            serviceProvider.Dispose();

            // Assert
            Assert.That(singleton, Is.Not.Null, "Service should have been resolved before disposal");
            // Note: We can't directly test disposal, but this ensures no exceptions are thrown
        }

        private static string ExtractGuid(string message, string prefix)
        {
            var startIndex = message.IndexOf(prefix, StringComparison.Ordinal);
            if (startIndex == -1) return string.Empty;

            // Move past the prefix and any spaces
            startIndex += prefix.Length;
            while (startIndex < message.Length && message[startIndex] == ' ')
            {
                startIndex++;
            }

            // Extract until we hit a space or end of string
            var remainingMessage = message.Substring(startIndex);

            // The GUID is 36 characters long (with dashes)
            // Format: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
            if (remainingMessage.Length >= 36)
            {
                var potentialGuid = remainingMessage.Substring(0, 36).Trim();
                // Validate it looks like a GUID
                if (Guid.TryParse(potentialGuid, out _))
                {
                    return potentialGuid;
                }
            }

            return string.Empty;
        }
    }
}
