// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Patterns
{
    /// <summary>
    /// Abstract class for implementing singleton pattern for which is inherited from <see cref="MonoBehaviour"/>.
    /// </summary>
    /// <typeparam name="T">The type of the class.</typeparam>
    /// <seealso cref="MonoBehaviour"/>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Fields

        private static bool destroyed = false;
        
        private static T instance = null;

        private static bool instantiated = false;

        #endregion Fields

        #region Properties

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
                    Debug.LogWarningFormat("SingletonMonoBehaviour<{0}> already destroyed. Return null.", typeof(T));
                    return null;
                }

                if (instantiated && instance != null) 
                    return instance;
                
                var foundObjects = FindObjectsOfType<T>();

                if (foundObjects.Length > 0)
                {
                    // Found instances already have.
                    Instance = foundObjects[0];

                    if (foundObjects.Length == 1) 
                        return instance;
                    
                    Debug.LogWarningFormat("There are more than one instance of MonoBehaviourSingleton of type \"{0}\". Keeping the first. Destroying the others.", typeof(T).ToString());

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
                instance = value;
                instantiated = value != null;

                if (Application.isPlaying && value != null)
                {
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Called when script receive message Destroy.
        /// </summary>
        protected virtual void OnDestroy()
        {
            destroyed = true;
        }

        #endregion Methods
    }
}