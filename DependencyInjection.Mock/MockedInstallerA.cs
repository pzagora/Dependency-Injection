using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.Mock
{
    public class MockedInstallerA : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("A");
        }
    }
}