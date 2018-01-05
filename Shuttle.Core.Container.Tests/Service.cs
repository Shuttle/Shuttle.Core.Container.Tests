namespace Shuttle.Core.Container.Tests
{
    public class Service : IService
    {
        public Service(IServiceDependency serviceDependency)
        {
            ServiceDependency = serviceDependency;
        }

        public IServiceDependency ServiceDependency { get; }
    }
}