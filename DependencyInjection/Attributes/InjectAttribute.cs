using System;
using JetBrains.Annotations;

namespace DependencyInjection.Attributes
{
    /// <summary>
    /// Marks Field, Property or Method for injection.
    /// </summary>
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
    }
}