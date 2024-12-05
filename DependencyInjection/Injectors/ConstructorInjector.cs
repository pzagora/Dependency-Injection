using System;
using DependencyInjection.Buffers;
using DependencyInjection.Caching;
using DependencyInjection.Core;
using DependencyInjection.Exceptions;

namespace DependencyInjection.Injectors
{
    /// <summary>
    /// Used for manual object creation with usage of constructor injection.
    /// Use <see cref="DependencyInjection.Attributes.DependencyInjectionConstructorAttribute">DependencyInjectionConstructorAttribute</see> if constructor selection for injection is needed.
    /// </summary>
    public static class ConstructorInjector
    {
        public static object Construct(Type concrete, Container container)
        {
            var info = TypeConstructionInfoCache.Get(concrete);
            var arguments = ExactArrayPool<object>.Shared.Rent(info.ConstructorParameters.Length);

            for (var i = 0; i < info.ConstructorParameters.Length; i++)
            {
                arguments[i] = container.Resolve(info.ConstructorParameters[i]);
            }

            try
            {
                return info.ObjectActivator.Invoke(arguments);
            }
            catch (Exception e)
            {
                throw new ConstructorInjectorException(concrete, e);
            }
            finally
            {
                ExactArrayPool<object>.Shared.Return(arguments);
            }
        }
        
        public static object Construct(Type concrete, object[] arguments)
        {
            var info = TypeConstructionInfoCache.Get(concrete);

            try
            {
                return info.ObjectActivator.Invoke(arguments);
            }
            catch (Exception e)
            {
                throw new ConstructorInjectorException(concrete, e);
            }
        }
    }
}