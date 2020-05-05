using System.Collections.Generic;

namespace Shuttle.Core.Container.Tests
{
    public interface ICollectionDependency
    {
        IEnumerable<IService> Services { get; }
    }
}