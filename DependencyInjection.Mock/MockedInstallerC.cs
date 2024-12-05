using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.Mock
{
    internal class MockedInstallerC : MonoBehaviour, IInstaller
    {
        internal void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("C");
        }
    }
}