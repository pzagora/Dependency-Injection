using System;
using JetBrains.Annotations;

namespace DependencyInjection.Attributes
{
    /// <summary>
    /// Marks constructor to specifically use for injection.
    /// When missing, constructor with most properties will be used.
    /// </summary>
    [MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [AttributeUsage(AttributeTargets.Constructor)]
    public class DependencyInjectionConstructorAttribute : Attribute
    {
    }
}