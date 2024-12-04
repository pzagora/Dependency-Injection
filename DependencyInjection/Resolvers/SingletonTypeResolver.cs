using System;
using DependencyInjection.Core;
using DependencyInjection.Enums;
using DependencyInjection.Generics;

namespace DependencyInjection.Resolvers
{
    internal sealed class SingletonTypeResolver : IResolver
    {
        private object _instance;
        private readonly Type _concreteType;
        private readonly DisposableCollection _disposables = new();
        public Lifetime Lifetime => Lifetime.Singleton;

        public SingletonTypeResolver(Type concreteType)
        {
            Diagnosis.RegisterCallSite(this);
            _concreteType = concreteType;
        }

        public object Resolve(Container container)
        {
            Diagnosis.IncrementResolutions(this);

            if (_instance == null)
            {
                _instance = container.Construct(_concreteType);
                _disposables.TryAdd(_instance);
                Diagnosis.RegisterInstance(this, _instance);
            }

            return _instance;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}