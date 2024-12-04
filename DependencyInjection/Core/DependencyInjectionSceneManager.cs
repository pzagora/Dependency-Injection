using System;
using JetBrains.Annotations;
using DependencyInjection.Injectors;
using UnityEngine.SceneManagement;

namespace DependencyInjection.Core
{
    public static class DependencyInjectionSceneManager
    {
        [PublicAPI]
        public static void PreInstallScene(Scene scene, Action<ContainerBuilder> builder)
        {
            UnityInjector.ScenePreInstaller.Add(scene, builder);
        }
        
        [PublicAPI]
        public static void OverrideSceneParentContainer(Scene scene, Container parent)
        {
            UnityInjector.SceneContainerParentOverride.Add(scene, parent);
        }
    }
}