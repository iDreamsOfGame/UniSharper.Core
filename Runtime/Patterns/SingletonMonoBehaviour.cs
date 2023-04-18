// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

// ReSharper disable StaticMemberInGenericType

namespace UniSharper.Patterns
{
    /// <summary>
    /// Abstract class for implementing singleton pattern for which is inherited from <see cref="MonoBehaviour"/>.
    /// </summary>
    /// <typeparam name="T">The type of the class.</typeparam>
    /// <seealso cref="MonoBehaviour"/>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool instantiated;

        private static bool destroyed;

        private static T instance;

        /// <summary>
        /// Gets or sets the singleton instance.
        /// </summary>
        /// <value>The singleton instance.</value>
        public static T Instance
        {
            get
            {
                if (destroyed)
                {
                    Debug.LogWarning($"SingletonMonoBehaviour<{typeof(T)}> already destroyed. Return null.");
                    return null;
                }

                if (instance && instantiated)
                    return instance;

                var foundObjects = FindObjectsOfType<T>();

                if (foundObjects.Length > 0)
                {
                    // Found instances already have.
                    Instance = foundObjects[0];

                    if (foundObjects.Length == 1)
                        return instance;

                    Debug.LogWarning($"There are more than one instance of MonoBehaviourSingleton of type {typeof(T)}. Keeping the first. Destroying the others.");

                    for (int i = 1, length = foundObjects.Length; i < length; ++i)
                    {
                        var behaviour = foundObjects[i];
                        Destroy(behaviour.gameObject);
                    }
                }
                else
                {
                    // Make new one.
                    var targetGameObject = new GameObject { name = typeof(T).Name };
                    Instance = targetGameObject.AddComponent<T>();
                }

                return instance;
            }

            private set
            {
                if (!Application.isPlaying || !value)
                    return;

                instance = value;
                instantiated = value;

                if (instance.transform.root == instance.transform)
                {
                    DontDestroyOnLoad(instance.gameObject);
                }
                else
                {
                    DontDestroyOnLoad(instance.transform.root);
                }
            }
        }

        /// <summary>
        /// Called when script receive message Destroy.
        /// </summary>
        protected virtual void OnDestroy()
        {
            destroyed = true;
        }
    }
}