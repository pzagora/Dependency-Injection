using System.Runtime.CompilerServices;
using DependencyInjection.Core;
using DependencyInjection.Logging;

namespace DependencyInjection.Extensions
{
    public static class ContainerExtensions
    {
        private static readonly ConditionalWeakTable<Container, ContainerDebugProperties> _containerDebugProperties = new();

        internal static ContainerDebugProperties GetDebugProperties(this Container container)
        {
            return _containerDebugProperties.GetOrCreateValue(container);
        }
    }
}