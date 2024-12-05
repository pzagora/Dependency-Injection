using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.Mock
{
    internal class MockedInstallerC : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("C");
        }
    }
}