using System;
using System.Collections.Generic;
using System.Diagnostics;
using DependencyInjection.Core;
using DependencyInjection.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly] // https://docs.unity3d.com/ScriptReference/Scripting.AlwaysLinkAssemblyAttribute.html

namespace DependencyInjection.Injectors
{
    internal static class UnityInjector
    {
        internal static Action<Scene, SceneScope> OnSceneLoaded;
        internal static Container ProjectContainer { get; private set; }
        internal static Dictionary<Scene, Container> ContainersPerScene { get; } = new();
        internal static Dictionary<Scene, Container> SceneContainerParentOverride { get; } = new();
        internal static Dictionary<Scene, Action<ContainerBuilder>> ScenePreInstaller { get; } = new();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeAwakeOfFirstSceneOnly()
        {
            ReportDependencyInjectionDebuggerStatus();
            
            ContainersPerScene.Clear();
            ProjectContainer = CreateProjectContainer.Create();

            void InjectScene(Scene scene, SceneScope sceneScope)
            {
                DependencyInjectionLogger.Log($"Scene {scene.name} ({scene.GetHashCode()}) loaded", LogLevel.Development);
                var sceneContainer = CreateSceneContainer(scene, ProjectContainer, sceneScope);
                ContainersPerScene.Add(scene, sceneContainer);
                SceneInjector.Inject(scene, sceneContainer);
            }
            
            void DisposeScene(Scene scene)
            {
                DependencyInjectionLogger.Log($"Scene {scene.name} ({scene.GetHashCode()}) unloaded", LogLevel.Development);

                if (ContainersPerScene.Remove(scene, out var sceneContainer)) // Not all scenes has containers
                {
                    sceneContainer.Dispose();
                }
            }
            
            void DisposeProject()
            {
                ProjectContainer.Dispose();
                ProjectContainer = null;
                
                // Unsubscribe from static events ensuring that DependencyInjection works with domain reloading set to false
                OnSceneLoaded -= InjectScene;
                SceneManager.sceneUnloaded -= DisposeScene;
                Application.quitting -= DisposeProject;
            }
            
            OnSceneLoaded += InjectScene;
            SceneManager.sceneUnloaded += DisposeScene;
            Application.quitting += DisposeProject;
        }

        private static Container CreateSceneContainer(Scene scene, Container projectContainer, SceneScope sceneScope)
        {
            var sceneParentContainer = SceneContainerParentOverride.Remove(scene, out var container)
                ? container
                : projectContainer;
            
            return sceneParentContainer.Scope(builder =>
            {
                builder.SetName($"{scene.name} ({scene.GetHashCode()})");

                if (ScenePreInstaller.Remove(scene, out var preInstaller))
                {
                    preInstaller.Invoke(builder);
                }

                sceneScope.InstallBindings(builder);
            });
        }

        [Conditional("DependencyInjection_DEBUG")]
        private static void ReportDependencyInjectionDebuggerStatus()
        {
            DependencyInjectionLogger.Log("Symbol DependencyInjection_DEBUG are defined, performance impacted!", LogLevel.Warning);
        }
    }
}
