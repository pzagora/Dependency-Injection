using System.Collections.Generic;
using DependencyInjection.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace DependencyInjection.Injectors
{
    /// <summary>
    /// Used for manual injection for GameObjects.
    /// </summary>
    public static class GameObjectInjector
    {
        /// <summary>
        /// Injects dependencies to only the first MonoBehaviour found on the given GameObject.
        /// </summary>
        /// <param name="gameObject">GameObject to inject dependencies into.</param>
        /// <param name="container">Container with depencencies.</param>
        public static void InjectSingle(GameObject gameObject, Container container)
        {
            if (gameObject.TryGetComponent<MonoBehaviour>(out var monoBehaviour))
            {
                AttributeInjector.Inject(monoBehaviour, container);
            }
        }

        /// <summary>
        /// Injects dependencies to all MonoBehaviours found on the given GameObject (not recursively, so it does not account for children).
        /// </summary>
        /// <param name="gameObject">GameObject to inject dependencies into.</param>
        /// <param name="container">Container with depencencies.</param>
        public static void InjectObject(GameObject gameObject, Container container)
        {
            using var pooledObject = ListPool<MonoBehaviour>.Get(out var monoBehaviours);
            gameObject.GetComponents<MonoBehaviour>(monoBehaviours);

            for (int i = 0; i < monoBehaviours.Count; i++)
            {
                var monoBehaviour = monoBehaviours[i];

                if (monoBehaviour != null)
                {
                    AttributeInjector.Inject(monoBehaviour, container);
                }
            }
        }

        /// <summary>
        /// Injects dependencies to all MonoBehaviours found on the given GameObject and its children recursively.
        /// </summary>
        /// <param name="gameObject">GameObject to inject dependencies into.</param>
        /// <param name="container">Container with depencencies.</param>
        public static void InjectRecursive(GameObject gameObject, Container container)
        {
            using var pooledObject = ListPool<MonoBehaviour>.Get(out var monoBehaviours);
            gameObject.GetComponentsInChildren<MonoBehaviour>(true, monoBehaviours);

            for (int i = 0; i < monoBehaviours.Count; i++)
            {
                var monoBehaviour = monoBehaviours[i];

                if (monoBehaviour != null)
                {
                    AttributeInjector.Inject(monoBehaviour, container);
                }
            }
        }

        /// <summary>
        /// Applies <see cref="InjectRecursive"/> to multiple objects at once.
        /// </summary>
        /// <param name="gameObjects">List of GameObjects to inject dependencies into.</param>
        /// <param name="container">Container with depencencies.</param>
        public static void InjectRecursiveMany(List<GameObject> gameObjects, Container container)
        {
            using var pooledObject = ListPool<MonoBehaviour>.Get(out var monoBehaviours);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].GetComponentsInChildren<MonoBehaviour>(true, monoBehaviours);

                for (int j = 0; j < monoBehaviours.Count; j++)
                {
                    var monoBehaviour = monoBehaviours[j];

                    if (monoBehaviour != null)
                    {
                        AttributeInjector.Inject(monoBehaviour, container);
                    }
                }
            }
        }
    }
}