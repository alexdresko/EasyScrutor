namespace EasyScrutor.Tests.TestServices
{
    // Singleton Services
    public interface ISingletonService : ISingletonLifetime
    {
        string GetMessage();
    }

    public class SingletonService : ISingletonService
    {
        private readonly Guid _id = Guid.NewGuid();

        public string GetMessage() => $"Singleton: {_id}";
    }

    // Self Singleton Services
    public class SelfSingletonService : ISelfSingletonLifetime
    {
        private readonly Guid _id = Guid.NewGuid();

        public string GetMessage() => $"SelfSingleton: {_id}";
    }

    // Transient Services
    public interface ITransientService : ITransientLifetime
    {
        string GetMessage();
    }

    public class TransientService : ITransientService
    {
        private readonly Guid _id = Guid.NewGuid();

        public string GetMessage() => $"Transient: {_id}";
    }

    // Self Transient Services
    public class SelfTransientService : ISelfTransientLifetime
    {
        private readonly Guid _id = Guid.NewGuid();

        public string GetMessage() => $"SelfTransient: {_id}";
    }

    // Scoped Services
    public interface IScopedService : IScopedLifetime
    {
        string GetMessage();
    }

    public class ScopedService : IScopedService
    {
        private readonly Guid _id = Guid.NewGuid();

        public string GetMessage() => $"Scoped: {_id}";
    }

    // Self Scoped Services
    public class SelfScopedService : ISelfScopedLifetime
    {
        private readonly Guid _id = Guid.NewGuid();

        public string GetMessage() => $"SelfScoped: {_id}";
    }

    // Multiple Implementations
    public interface IMultipleImplementationService : ITransientLifetime
    {
        string GetName();
    }

    public class FirstImplementation : IMultipleImplementationService
    {
        public string GetName() => "First";
    }

    public class SecondImplementation : IMultipleImplementationService
    {
        public string GetName() => "Second";
    }

    // Service with Dependencies
    public interface IComplexService : IScopedLifetime
    {
        string ProcessData();
    }

    public class ComplexService : IComplexService
    {
        private readonly ISingletonService _singletonService;
        private readonly ITransientService _transientService;

        public ComplexService(ISingletonService singletonService, ITransientService transientService)
        {
            _singletonService = singletonService;
            _transientService = transientService;
        }

        public string ProcessData() => $"Complex: {_singletonService.GetMessage()} + {_transientService.GetMessage()}";
    }

    // Abstract class test
    public abstract class BaseService : ISingletonLifetime
    {
        public abstract string GetData();
    }

    public class ConcreteService : BaseService
    {
        public override string GetData() => "Concrete Data";
    }

    // Generic service test
    public interface IGenericService<T> : ITransientLifetime
    {
        T GetDefault();
    }

    public class GenericService<T> : IGenericService<T>
    {
        public T GetDefault() => default!;
    }
}
