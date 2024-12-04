using DependencyInjection.Logging;
using UnityEngine;

namespace DependencyInjection.Configuration
{
    internal sealed class DependencyInjectionSettings : ScriptableObject
    {
        [field: SerializeField] public LogLevel LogLevel { get; private set; }

        private void OnValidate()
        {
            DependencyInjectionLogger.UpdateLogLevel(LogLevel);
        }
    }
}