using System.Collections.Generic;

namespace DependencyInjection.Logging
{
    public sealed class ContainerDebugProperties
    {
        public List<CallSite> BuildCallsite { get; } = new();
    }
}