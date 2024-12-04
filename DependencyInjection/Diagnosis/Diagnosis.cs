﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DependencyInjection.Core;
using DependencyInjection.Extensions;
using DependencyInjection.Resolvers;
using UnityEngine;

namespace DependencyInjection
{
    internal static class Diagnosis
    {
        [Conditional("DependencyInjection_DEBUG")]
        internal static void IncrementResolutions(IResolver resolver)
        {
            resolver.GetDebugProperties().Resolutions++;
        }
        
        [Conditional("DependencyInjection_DEBUG")]
        internal static void RegisterInstance(IResolver resolver, object instance)
        {
            resolver.GetDebugProperties().Instances.Add((new WeakReference(instance), GetCallSite(3)));
        }
        
        [Conditional("DependencyInjection_DEBUG")]
        internal static void ClearInstances(IResolver resolver)
        {
            resolver.GetDebugProperties().Instances.Clear();
        }

        [Conditional("DependencyInjection_DEBUG")]
        internal static void RegisterCallSite(IResolver resolver)
        {
            resolver.GetDebugProperties().BindingCallsite.AddRange(GetCallSite(2));
        }
        
        [Conditional("DependencyInjection_DEBUG")]
        internal static void RegisterBuildCallSite(Container container)
        {
            container.GetDebugProperties().BuildCallsite.AddRange(GetCallSite(2));
        }
        
        private static List<CallSite> GetCallSite(int skipFrames)
        {
            var result = new List<CallSite>();
            var stackTrace = new StackTrace(skipFrames, true);
            var frames = stackTrace.GetFrames();

            foreach (var frame in frames.Where(f => f.GetFileName() != null))
            {
                var methodName = frame.GetMethod()?.Name;
                var className = frame.GetMethod()?.DeclaringType?.FullName;
                var lineNumber = frame.GetFileLineNumber();
                var filePath = GetUnityPath(frame.GetFileName());
                result.Add(new CallSite(className, methodName, filePath, lineNumber));
            }

            return result;
        }

        private static string GetUnityPath(string path)
        {
            return path.Replace("\\", "/").Replace(Application.dataPath, "Assets");
        }
    }
}