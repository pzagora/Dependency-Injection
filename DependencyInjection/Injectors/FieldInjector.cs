using System;
using System.Reflection;
using DependencyInjection.Core;
using DependencyInjection.Exceptions;

namespace DependencyInjection.Injectors
{
    internal static class FieldInjector
    {
        internal static void Inject(FieldInfo field, object instance, Container container)
        {
            try
            {
                field.SetValue(instance, container.Resolve(field.FieldType));
            }
            catch (Exception e)
            {
                throw new FieldInjectorException(e);
            }
        }
    }
}