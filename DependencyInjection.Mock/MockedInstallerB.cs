using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.Mock
{
    internal class MockedInstallerB : MonoBehaviour, IInstaller
    {
        internal void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("B");
        }
    }
}