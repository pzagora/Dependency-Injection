using System;

namespace DependencyInjection.Exceptions
{
    internal sealed class PropertyInjectorException : Exception
    {
        public PropertyInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}