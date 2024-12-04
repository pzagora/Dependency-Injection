using System;
using DependencyInjection.Core;
using DependencyInjection.Enums;

namespace DependencyInjection.Resolvers
{
    internal sealed class TransientTypeResolver : IResolver
    {
        private readonly Type _concreteType;
        public Lifetime Lifetime => Lifetime.Transient;

        public TransientTypeResolver(Type concreteType)
        {
            Diagnosis.RegisterCallSite(this);
            _concreteType = concreteType;
        }

        public object Resolve(Container container)
        {
            Diagnosis.IncrementResolutions(this);
            var instance = container.Construct(_concreteType);
            container.Disposables.TryAdd(instance);
            Diagnosis.RegisterInstance(this, instance);
            return instance;
        }

        public void Dispose()
        {
        }
    }
}