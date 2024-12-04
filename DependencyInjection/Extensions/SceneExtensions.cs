using DependencyInjection.Core;
using DependencyInjection.Injectors;
using UnityEngine.SceneManagement;

namespace DependencyInjection.Extensions
{
    public static class SceneExtensions
    {
        public static Container GetSceneContainer(this Scene scene)
        {
            return UnityInjector.ContainersPerScene[scene];
        }
    }
}