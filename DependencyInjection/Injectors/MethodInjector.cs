using System;
using DependencyInjection.Buffers;
using DependencyInjection.Caching;
using DependencyInjection.Core;
using DependencyInjection.Exceptions;

namespace DependencyInjection.Injectors
{
    internal static class MethodInjector
    {
        internal static void Inject(InjectedMethodInfo method, object instance, Container container)
        {
            var arguments = ExactArrayPool<object>.Shared.Rent(method.Parameters.Length);

            try
            {
                for (var i = 0; i < method.Parameters.Length; i++)
                {
                    arguments[i] = container.Resolve(method.Parameters[i].ParameterType);
                }

                method.MethodInfo.Invoke(instance, arguments);
            }
            catch (Exception e)
            {
                throw new MethodInjectorException(instance, method.MethodInfo, e);
            }
            finally
            {
                ExactArrayPool<object>.Shared.Return(arguments);
            }
        }
    }
}