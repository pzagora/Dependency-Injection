using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.Mock
{
    public class MockedInstallerB : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("B");
        }
    }
}