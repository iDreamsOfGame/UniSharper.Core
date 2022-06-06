// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Copies all components values of the specific <see cref="GameObject"/> to the other <see cref="GameObject"/>.
        /// </summary>
        /// <param name="gameObject">The original <see cref="GameObject"/>.</param>
        /// <param name="targetGameObject">The target <see cref="GameObject"/>.</param>
        /// <param name="excludeTransform">
        /// if set to <c>true</c> exclude the component of <see cref="Transform"/>.
        /// </param>
        public static void CopyAllComponentsValues(this GameObject gameObject, GameObject targetGameObject, bool excludeTransform = true)
        {
            if (!targetGameObject) 
                return;
            
            var components = gameObject.GetAllComponents();

            for (int i = 0, length = components.Length; i < length; ++i)
            {
                var component = components[i];

                if (excludeTransform && component.GetType().FullName == "UnityEngine.Transform")
                {
                    continue;
                }

                component.CopyComponentValues(targetGameObject);
            }
        }

        /// <summary>
        /// Finds the <see cref="GameObject"/> in children.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/>.</param>
        /// <param name="targetName">The name of the target object.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="GameObject"/>.</param>
        /// <returns>The <see cref="GameObject"/> you want to find.</returns>
        public static GameObject FindInChildren(this GameObject gameObject, string targetName, bool includeInactive = true)
        {
            if (string.IsNullOrEmpty(targetName))
                return null;
            
            var transform = gameObject.transform;

            foreach (Transform childTransform in transform)
            {
                if (!includeInactive && !childTransform.gameObject.activeSelf)
                {
                    continue;
                }

                if (childTransform.name == targetName)
                {
                    return childTransform.gameObject;
                }

                var targetGameObject = childTransform.gameObject.FindInChildren(targetName, includeInactive);

                if (targetGameObject != null)
                {
                    return targetGameObject;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns all components of the <see cref="GameObject"/>.
        /// </summary>
        /// <param name="gameObject">The specific <see cref="GameObject"/>.</param>
        /// <returns>The array of all components.</returns>
        public static Component[] GetAllComponents(this GameObject gameObject) => gameObject.GetComponents(typeof(Component));

        /// <summary>
        /// Gets or adds the <see cref="Component"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="gameObject">The <see cref="GameObject"/> need to get or add component.</param>
        /// <returns>The <see cref="Component"/> get or added.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component => gameObject.GetOrAddComponent(typeof(T)) as T;

        /// <summary>
        /// Gets or adds the <see cref="Component"/>.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to get or add component.</param>
        /// <param name="type">The <see cref="Type"/> of the <see cref="GameObject"/>.</param>
        /// <returns>The <see cref="Component"/> get or added.</returns>
        public static Component GetOrAddComponent(this GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (!component)
                component = gameObject.AddComponent(type);

            return component;
        }

        /// <summary>
        /// Determines whether the layer of <see cref="GameObject"/> is in the specific <see cref="LayerMask"/>.
        /// </summary>
        /// <param name="gameObject">The specific <see cref="GameObject"/>.</param>
        /// <param name="layerMask">The value of <see cref="LayerMask"/>.</param>
        /// <returns>
        /// <c>true</c> if the layer of <see cref="GameObject"/> is in the <see cref="LayerMask"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInLayerMask(this GameObject gameObject, LayerMask layerMask)
        {
            if (!gameObject)
                return false;
            
            var objectLayerMaskValue = 1 << gameObject.layer;
            return (layerMask.value & objectLayerMaskValue) > 0;
        }

        /// <summary>
        /// Removes the <see cref="Component"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="gameObject">The <see cref="GameObject"/> to remove component.</param>
        /// <param name="delay">The optional amount of time to delay before removing the component.</param>
        /// <returns>
        /// <c>true</c> if remove <see cref="Component"/> successfully, <c>false</c> otherwise.
        /// </returns>
        public static bool RemoveComponent<T>(this GameObject gameObject, float delay = 0.0f) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (!component) 
                return false;
            
            Object.Destroy(component, delay);
            return true;
        }

        /// <summary>
        /// Removes the <see cref="Component"/> immediately.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="gameObject">The <see cref="GameObject"/> to remove component.</param>
        /// <returns>
        /// <c>true</c> if remove <see cref="Component"/> successfully, <c>false</c> otherwise.
        /// </returns>
        public static bool RemoveComponentImmediate<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (!component)
                return false;
            
            Object.DestroyImmediate(component);
            return true;
        }

        /// <summary>
        /// Sets the children layer.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/>.</param>
        /// <param name="layer">The layer.</param>
        /// <param name="includeParent">if set to <c>true</c> include parent.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="GameObject"/>.</param>
        public static void SetChildrenLayer(this GameObject gameObject, int layer = -1, bool includeParent = true, bool includeInactive = true)
        {
            if (layer < 0) 
                return;
            
            if (includeParent)
            {
                if (!(!gameObject.activeSelf && !includeInactive))
                {
                    gameObject.layer = layer;
                }
            }

            var transform = gameObject.transform;

            foreach (Transform childTransform in transform)
            {
                if (!includeInactive && !childTransform.gameObject.activeSelf)
                {
                    continue;
                }

                childTransform.gameObject.layer = layer;
                childTransform.gameObject.SetChildrenLayer(layer, includeParent, includeInactive);
            }
        }

        /// <summary>
        /// Sets the children layer.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/>.</param>
        /// <param name="layerName">The name of the layer.</param>
        /// <param name="includeParent">if set to <c>true</c> include parent.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="GameObject"/>.</param>
        public static void SetChildrenLayer(this GameObject gameObject, string layerName, bool includeParent = true, bool includeInactive = true)
        {
            if (string.IsNullOrEmpty(layerName)) 
                return;
            
            var layer = LayerMask.NameToLayer(layerName);
            gameObject.SetChildrenLayer(layer, includeParent, includeInactive);
        }
    }
}