namespace Shuttle.Core.Container.Tests
{
    public class Service : IService
    {
        public IServiceDependency ServiceDependency { get; private set; }

        public Service(IServiceDependency serviceDependency)
        {
            ServiceDependency = serviceDependency;
        }
    }
}