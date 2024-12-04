using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.Mock
{
    public class MockedInstallerC : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("C");
        }
    }
}