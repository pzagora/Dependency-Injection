using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.Mock
{
    public class MockedInstallerB : MonoBehaviour, IInstaller
    {
        internal void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("B");
        }
    }
}