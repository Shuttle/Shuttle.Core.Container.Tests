﻿using System.Linq;
using NUnit.Framework;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Container.Tests
{
    public class ContainerFixture
    {
        protected void RegisterSingleton(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            Assert.IsFalse(registry.IsRegistered<IServiceDependency>());
            Assert.IsFalse(registry.IsRegistered<IService>());

            Assert.IsTrue(registry.Register<IServiceDependency, ServiceDependency>(Lifestyle.Singleton)
                .IsRegistered<IServiceDependency>());
            Assert.IsTrue(registry.Register<IService, Service>(Lifestyle.Singleton).IsRegistered<IService>());
        }

        protected void RegisterTransient(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            Assert.IsFalse(registry.IsRegistered<IServiceDependency>());
            Assert.IsFalse(registry.IsRegistered<IService>());

            Assert.IsTrue(registry.Register<IServiceDependency, ServiceDependency>(Lifestyle.Transient)
                .IsRegistered<IServiceDependency>());
            Assert.IsTrue(registry.Register<IService, Service>(Lifestyle.Transient).IsRegistered<IService>());
        }

        protected void RegisterSingletonGeneric(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            Assert.IsFalse(registry.IsRegistered(typeof(IOpenGeneric<>)));

            Assert.IsTrue(registry.RegisterGeneric(typeof(IOpenGeneric<>), typeof(OpenGeneric<>), Lifestyle.Singleton)
                .IsRegistered(typeof(IOpenGeneric<>)));
        }

        protected void RegisterTransientGeneric(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            Assert.IsFalse(registry.IsRegistered(typeof(IOpenGeneric<>)));

            Assert.IsTrue(registry.RegisterGeneric(typeof(IOpenGeneric<>), typeof(OpenGeneric<>), Lifestyle.Transient)
                .IsRegistered(typeof(IOpenGeneric<>)));
        }

        protected void RegisterMultipleSingleton(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            Assert.IsFalse(registry.IsRegistered<IMultipleImplementation<string>>());
            Assert.IsFalse(registry.IsRegistered<IMultipleImplementation<int>>());

            Assert.IsTrue(registry
                .Register<IMultipleImplementation<string>, MultipleImplementation>(Lifestyle.Singleton)
                .IsRegistered<IMultipleImplementation<string>>());
            Assert.IsTrue(registry.Register<IMultipleImplementation<int>, MultipleImplementation>(Lifestyle.Singleton)
                .IsRegistered<IMultipleImplementation<int>>());
        }

        protected void RegisterMultipleTransient(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            Assert.IsFalse(registry.IsRegistered<IMultipleImplementation<string>>());
            Assert.IsFalse(registry.IsRegistered<IMultipleImplementation<int>>());

            Assert.IsTrue(registry
                .Register<IMultipleImplementation<string>, MultipleImplementation>(Lifestyle.Transient)
                .IsRegistered<IMultipleImplementation<string>>());
            Assert.IsTrue(registry.Register<IMultipleImplementation<int>, MultipleImplementation>(Lifestyle.Transient)
                .IsRegistered<IMultipleImplementation<int>>());
        }

        protected void ResolveSingleton(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            var singleton = resolver.Resolve<IService>();

            Assert.IsNotNull(singleton, "The requested IService implementation may not be null.");
            Assert.AreSame(singleton, resolver.Resolve<IService>(),
                "Multiple calls to resolve IService should return the same instance.");
        }

        protected void ResolveTransient(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            var transient = resolver.Resolve<IService>();

            Assert.IsNotNull(transient, "The requested IService implementation may not be null.");
            Assert.AreNotSame(transient, resolver.Resolve<IService>(),
                "Multiple calls to resolve IService should return unique instances.");
        }

        protected void ResolveSingletonGeneric(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            var singletonString = resolver.Resolve<IOpenGeneric<string>>();
            var singletonInt = resolver.Resolve<IOpenGeneric<int>>();

            Assert.IsNotNull(singletonString, "The requested IOpenGeneric<string> implementation may not be null.");
            Assert.IsNotNull(singletonInt, "The requested IOpenGeneric<int> implementation may not be null.");

            Assert.AreSame(singletonString, resolver.Resolve<IOpenGeneric<string>>(),
                "Multiple calls to resolve IOpenGeneric<string> should return the same instance.");
            Assert.AreSame(singletonInt, resolver.Resolve<IOpenGeneric<int>>(),
                "Multiple calls to resolve IOpenGeneric<int> should return the same instance.");
        }

        protected void ResolveTransientGeneric(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            var transientString = resolver.Resolve<IOpenGeneric<string>>();
            var transientInt = resolver.Resolve<IOpenGeneric<int>>();

            Assert.IsNotNull(transientString, "The requested IOpenGeneric<string> implementation may not be null.");
            Assert.IsNotNull(transientInt, "The requested IOpenGeneric<int> implementation may not be null.");

            Assert.AreNotSame(transientString, resolver.Resolve<IOpenGeneric<string>>(),
                "Multiple calls to resolve IOpenGeneric<string> should return unique instances.");
            Assert.AreNotSame(transientInt, resolver.Resolve<IOpenGeneric<int>>(),
                "Multiple calls to resolve IOpenGeneric<int> should return unique instances.");
        }

        protected void ResolveMultipleSingleton(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            Assert.IsNotNull(resolver.Resolve<IMultipleImplementation<string>>(),
                "The requested IMultipleImplementation<string> implementation may not be null.");
            Assert.AreSame(resolver.Resolve<IMultipleImplementation<string>>(),
                resolver.Resolve<IMultipleImplementation<string>>(),
                "Multiple calls to resolve IMultipleImplementation<string> should return unique instances.");

            Assert.IsNotNull(resolver.Resolve<IMultipleImplementation<int>>(),
                "The requested IMultipleImplementation<int> implementation may not be null.");
            Assert.AreSame(resolver.Resolve<IMultipleImplementation<int>>(),
                resolver.Resolve<IMultipleImplementation<int>>(),
                "Multiple calls to resolve IMultipleImplementation<int> should return unique instances.");
        }

        protected void ResolveMultipleTransient(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            Assert.IsNotNull(resolver.Resolve<IMultipleImplementation<string>>(),
                "The requested IMultipleImplementation<string> implementation may not be null.");
            Assert.AreNotSame(resolver.Resolve<IMultipleImplementation<string>>(),
                resolver.Resolve<IMultipleImplementation<string>>(),
                "Multiple calls to resolve IMultipleImplementation<string> should return the same instance.");

            Assert.IsNotNull(resolver.Resolve<IMultipleImplementation<int>>(),
                "The requested IMultipleImplementation<int> implementation may not be null.");
            Assert.AreNotSame(resolver.Resolve<IMultipleImplementation<int>>(),
                resolver.Resolve<IMultipleImplementation<int>>(),
                "Multiple calls to resolve IMultipleImplementation<int> should return the same instance.");
        }

        protected void RegisterCollection(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            Assert.IsFalse(registry.IsRegistered<IServiceDependency>());
            Assert.IsFalse(registry.IsRegistered(typeof(IService)));
            Assert.IsFalse(registry.IsRegistered(typeof(ICollectionDependency)));

            Assert.IsTrue(registry.Register<IServiceDependency, ServiceDependency>(Lifestyle.Transient)
                .IsRegistered<IServiceDependency>());
            Assert.IsTrue(registry.RegisterCollection(typeof(IService),
                new[] { typeof(Service1), typeof(Service2), typeof(Service3) },
                Lifestyle.Singleton).IsRegistered(typeof(IService)));
            Assert.IsTrue(registry.Register<ICollectionDependency, CollectionDependency>(Lifestyle.Transient)
                .IsRegistered<ICollectionDependency>());
        }

        protected void ResolveCollection(IComponentResolver resolver)
        {
            Guard.AgainstNull(resolver, nameof(resolver));

            Assert.AreEqual(3, resolver.ResolveAll<IService>().Count());

            var collectionDependency = resolver.Resolve<ICollectionDependency>();

            Assert.IsNotNull(collectionDependency.Services);
            Assert.AreEqual(3, collectionDependency.Services.Count());
        }
    }
}