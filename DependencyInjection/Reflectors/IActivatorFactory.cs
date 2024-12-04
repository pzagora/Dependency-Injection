using System;
using System.Reflection;
using DependencyInjection.Delegates;

namespace DependencyInjection.Reflectors
{
    internal interface IActivatorFactory
    {
        ObjectActivator GenerateActivator(Type type, ConstructorInfo constructor, Type[] parameters);
        ObjectActivator GenerateDefaultActivator(Type type);
    }
}