using DependencyInjection.Core;
using DependencyInjection.Enums;
using DependencyInjection.Generics;

namespace DependencyInjection.Resolvers
{
    internal sealed class SingletonValueResolver : IResolver
    {
        private readonly object _value;
        private readonly DisposableCollection _disposables = new();
        public Lifetime Lifetime => Lifetime.Singleton;

        public SingletonValueResolver(object value)
        {
            Diagnosis.RegisterCallSite(this);
            Diagnosis.RegisterInstance(this, value);
            _value = value;
            _disposables.TryAdd(value);
        }

        public object Resolve(Container container)
        {
            Diagnosis.IncrementResolutions(this);
            return _value;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}