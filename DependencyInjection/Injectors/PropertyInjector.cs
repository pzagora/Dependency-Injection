using System;
using System.Reflection;
using DependencyInjection.Core;
using DependencyInjection.Exceptions;

namespace DependencyInjection.Injectors
{
    internal static class PropertyInjector
    {
        internal static void Inject(PropertyInfo property, object instance, Container container)
        {
            try
            {
                property.SetValue(instance, container.Resolve(property.PropertyType));
            }
            catch (Exception e)
            {
                throw new PropertyInjectorException(e);
            }
        }
    }
}