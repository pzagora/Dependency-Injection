using System;
using DependencyInjection.Core;
using DependencyInjection.Enums;
using DependencyInjection.Generics;

namespace DependencyInjection.Resolvers
{
    internal sealed class SingletonFactoryResolver : IResolver
    {
        private object _instance;
        private readonly Func<Container, object> _factory;
        private readonly DisposableCollection _disposables = new();

        public Lifetime Lifetime => Lifetime.Singleton;

        public SingletonFactoryResolver(Func<Container, object> factory)
        {
            Diagnosis.RegisterCallSite(this);
            _factory = factory;
        }

        public object Resolve(Container container)
        {
            Diagnosis.IncrementResolutions(this);

            if (_instance == null)
            {
                _instance = _factory.Invoke(container);
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