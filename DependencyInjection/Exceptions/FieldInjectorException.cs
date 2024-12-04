using System;

namespace DependencyInjection.Exceptions
{
    internal sealed class FieldInjectorException : Exception
    {
        public FieldInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}