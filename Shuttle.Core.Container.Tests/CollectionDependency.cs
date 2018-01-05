using System.Collections.Generic;

namespace Shuttle.Core.Container.Tests
{
    public class CollectionDependency : ICollectionDependency
    {
        public IEnumerable<IService> Services { get; }

        public CollectionDependency(IEnumerable<IService> services)
        {
            Services = services;
        }
    }
}