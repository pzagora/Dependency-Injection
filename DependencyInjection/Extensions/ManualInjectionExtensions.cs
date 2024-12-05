using DependencyInjection.Injectors;
using UnityEngine.SceneManagement;

namespace DependencyInjection.Extensions
{
    public static class ManualInjectionExtensions
    {
        /// <summary>
        /// Tries to inject into all fields, properties and methods marked with [<see cref="DependencyInjection.Attributes.InjectAttribute">Inject</see>] attribute, using current scene container.
        /// </summary>
        /// <param name="item">Object to inject bindings into.</param>
        /// <typeparam name="T">Class type.</typeparam>
        /// <returns></returns>
        public static T InjectAttributes<T>(this T item) 
            where T : class
        {
            if (item is null)
                return null;
            
            AttributeInjector.Inject(item, SceneManager.GetActiveScene().GetSceneContainer());
            return item;
        }
    }
}
