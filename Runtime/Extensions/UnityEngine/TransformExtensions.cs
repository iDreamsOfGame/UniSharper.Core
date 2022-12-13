// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Text;
using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Get the path of <see cref="UnityEngine.Transform"/> in hierarchy. 
        /// </summary>
        /// <param name="transform">The <see cref="UnityEngine.Transform"/> instance. </param>
        /// <returns>The path of <see cref="UnityEngine.Transform"/> in hierarchy. </returns>
        public static string GetPath(this Transform transform)
        {
            if (!transform)
                return string.Empty;

            var builder = new StringBuilder(transform.name);
            var root = transform.root;
            var parent = transform.parent;

            // 过滤掉无法编辑的对象
            if(root.gameObject.hideFlags.HasFlag(HideFlags.NotEditable))
                root = root.GetChild(0);

            while (parent && parent != root)
            {
                builder.Insert(0, '/');
                builder.Insert(0, parent.name);
                parent = parent.parent;
            }

            return builder.ToString();
        }
        
        /// <summary>
        /// Finds the <see cref="Transform"/> in children.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="targetName">The name of the target object.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="Transform"/>.</param>
        /// <returns>The <see cref="Transform"/> you want to find.</returns>
        public static Transform FindInChildren(this Transform transform, string targetName, bool includeInactive = true)
        {
            var go = transform.gameObject.FindInChildren(targetName, includeInactive);

            if (go)
            {
                return go.transform;
            }

            return null;
        }

        /// <summary>
        /// Returns all components of the <see cref="Transform"/>.
        /// </summary>
        /// <param name="gameObject">The specific <see cref="Transform"/>.</param>
        /// <returns>The array of all components.</returns>
        public static Component[] GetAllComponents(this Transform transform) => transform.gameObject.GetAllComponents();

        /// <summary>
        /// The reverse direction of blue axis of the <see cref="Transform"/> in world space.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The backward direction.</returns>
        public static Vector3 GetBackwardDirection(this Transform transform) => -transform.forward;

        /// <summary>
        /// The reverse direction of green axis of the <see cref="Transform"/> in world space.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The down direction.</returns>
        public static Vector3 GetDownDirection(this Transform transform) => -transform.up;

        /// <summary>
        /// The reverse direction of red axis of the <see cref="Transform"/> in world space.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The left direction.</returns>
        public static Vector3 GetLeftDirection(this Transform transform) => -transform.right;

        /// <summary>
        /// Gets the value of axis <c>x</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>x</c> in the local coordinate.</returns>
        public static float GetLocalPositionX(this Transform transform) => transform.localPosition.x;

        /// <summary>
        /// Gets the value of axis <c>y</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>y</c> in the local coordinate.</returns>
        public static float GetLocalPositionY(this Transform transform) => transform.localPosition.y;

        /// <summary>
        /// Gets the value of axis <c>z</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>z</c> in the local coordinate.</returns>
        public static float GetLocalPositionZ(this Transform transform) => transform.localPosition.z;

        /// <summary>
        /// Gets the value of axis <c>x</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>x</c> in the world coordinate.</returns>
        public static float GetPositionX(this Transform transform) => transform.position.x;

        /// <summary>
        /// Gets the value of axis <c>y</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>y</c> in the world coordinate.</returns>
        public static float GetPositionY(this Transform transform) => transform.position.y;

        /// <summary>
        /// Gets the value of axis <c>z</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>z</c> in the world coordinate.</returns>
        public static float GetPositionZ(this Transform transform) => transform.position.z;

        /// <summary>
        /// Removes the <see cref="Component"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="transform">The <see cref="Transform"/> to remove component.</param>
        /// <param name="delay">The optional amount of time to delay before removing the component.</param>
        /// <returns>
        /// <c>true</c> if remove <see cref="Component"/> successfully, <c>false</c> otherwise.
        /// </returns>
        public static bool RemoveComponent<T>(this Transform transform, float delay = 0.0f) where T : Component => transform.gameObject.RemoveComponent<T>(delay);

        /// <summary>
        /// Removes the <see cref="Component"/> immediately.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="transform">The <see cref="Transform"/> to remove component.</param>
        /// <returns>
        /// <c>true</c> if remove <see cref="Component"/> successfully, <c>false</c> otherwise.
        /// </returns>
        public static bool RemoveComponentImmediate<T>(this Transform transform) where T : Component => transform.gameObject.RemoveComponentImmediate<T>();

        /// <summary>
        /// Sets the children layer.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="layer">The layer.</param>
        /// <param name="includeParent">if set to <c>true</c> include parent.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="Transform"/>.</param>
        public static void SetChildrenLayer(this Transform transform, int layer = -1, bool includeParent = true, bool includeInactive = true) => transform.gameObject.SetChildrenLayer(layer, includeParent, includeInactive);

        /// <summary>
        /// Sets the children layer.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="layerName">The name of the layer.</param>
        /// <param name="includeParent">if set to <c>true</c> include parent.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="Transform"/>.</param>
        public static void SetChildrenLayer(this Transform transform, string layerName, bool includeParent = true, bool includeInactive = true) => transform.gameObject.SetChildrenLayer(layerName, includeParent, includeInactive);

        /// <summary>
        /// Sets the value of axis <c>x</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="value">The value of axis <c>x</c> in the local coordinate.</param>
        public static void SetLocalPositionX(this Transform transform, float value)
        {
            var localPosition = transform.localPosition;
            transform.localPosition = new Vector3(value, localPosition.y, localPosition.z);
        }

        /// <summary>
        /// Sets the value of axis <c>y</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>y</c> in the local coordinate.</returns>
        public static void SetLocalPositionY(this Transform transform, float value)
        {
            var localPosition = transform.localPosition;
            transform.localPosition = new Vector3(localPosition.x, value, localPosition.z);
        }

        /// <summary>
        /// Sets the value of axis <c>z</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>z</c> in the local coordinate.</returns>
        public static void SetLocalPositionZ(this Transform transform, float value)
        {
            var localPosition = transform.localPosition;
            transform.localPosition = new Vector3(localPosition.x, localPosition.y, value);
        }

        /// <summary>
        /// Sets the value of axis <c>x</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="value">The value of axis <c>x</c> in the world coordinate.</param>
        public static void SetPositionX(this Transform transform, float value)
        {
            var position = transform.position;
            transform.position = new Vector3(value, position.y, position.z);
        }

        /// <summary>
        /// Sets the value of axis <c>y</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>y</c> in the world coordinate.</returns>
        public static void SetPositionY(this Transform transform, float value)
        {
            var position = transform.position;
            transform.position = new Vector3(position.x, value, position.z);
        }

        /// <summary>
        /// Sets the value of axis <c>z</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>z</c> in the world coordinate.</returns>
        public static void SetPositionZ(this Transform transform, float value)
        {
            var position = transform.position;
            transform.position = new Vector3(position.x, position.y, value);
        }
    }
}
