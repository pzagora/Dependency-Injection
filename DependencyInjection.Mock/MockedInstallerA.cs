using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.Mock
{
    internal class MockedInstallerA : MonoBehaviour, IInstaller
    {
        internal void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("A");
        }
    }
}