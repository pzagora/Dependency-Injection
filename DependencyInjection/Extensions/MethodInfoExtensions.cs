using System;
using System.Reflection;

namespace DependencyInjection.Extensions
{
    internal static class MethodInfoExtensions
    {
        public static T CreateDelegate<T>(this MethodInfo methodInfo) where T : Delegate
        {
            return (T) methodInfo.CreateDelegate(typeof(T));
        }
    }
}