using System;
using DependencyInjection.Configuration;
using UnityEngine;

namespace DependencyInjection.Logging
{
    internal static class DependencyInjectionLogger
    {
        private static LogLevel _logLevel;

        static DependencyInjectionLogger()
        {
            var dependencyInjectionSettings = Resources.Load<DependencyInjectionSettings>(nameof(DependencyInjectionSettings));

            _logLevel = dependencyInjectionSettings != null
                ? dependencyInjectionSettings.LogLevel
                : LogLevel.Info;
            
            Log($"DependencyInjection LogLevel set to {_logLevel}", LogLevel.Info);
        }

        public static void UpdateLogLevel(LogLevel logLevel)
        {
            if (logLevel != _logLevel)
            {
                _logLevel = logLevel;
                Log($"DependencyInjection LogLevel set to {_logLevel}", LogLevel.Info);
            }
        }
        
        public static void Log(object message, LogLevel logLevel, UnityEngine.Object context = null)
        {
            if (logLevel < _logLevel)
            {
                return;
            }
            
            switch (logLevel)
            {
                case LogLevel.Development: Debug.Log(message, context); break;
                case LogLevel.Info: Debug.Log(message, context); break;
                case LogLevel.Warning: Debug.LogWarning(message, context); break;
                case LogLevel.Error: Debug.LogError(message, context); break;
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}