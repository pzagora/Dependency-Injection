using System;
using DependencyInjection.Core;
using DependencyInjection.Enums;

namespace DependencyInjection.Resolvers
{
    public interface IResolver : IDisposable
    {
        Lifetime Lifetime { get; }
        object Resolve(Container container);
    }
}