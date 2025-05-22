using DependencyInjection.Core;
using UnityEngine;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DependencyInjection.EditModeTests")]
namespace DependencyInjection.Mock
{
    internal class MockedInstallerA : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton("A");
        }
    }
}