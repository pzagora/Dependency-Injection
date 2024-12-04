using System;
using JetBrains.Annotations;

namespace DependencyInjection.Attributes
{
    [MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [AttributeUsage(AttributeTargets.Constructor)]
    public class DependencyInjectionConstructorAttribute : Attribute
    {
    }
}