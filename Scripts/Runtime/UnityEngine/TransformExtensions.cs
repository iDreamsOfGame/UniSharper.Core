// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="Transform"/>.
    /// </summary>
    public static class TransformExtensions
    {
        #region Methods

        /// <summary>
        /// Finds the <see cref="Transform"/> in children.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="targetName">The name of the target object.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="Transform"/>.</param>
        /// <returns>The <see cref="Transform"/> you want to find.</returns>
        public static Transform FindInChildren(this Transform transform, string targetName, bool includeInactive = true)
        {
            GameObject go = transform.gameObject.FindInChildren(targetName, includeInactive);

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
        public static Component[] GetAllComponents(this Transform transform)
        {
            return transform.gameObject.GetAllComponents();
        }

        /// <summary>
        /// The reverse direction of blue axis of the <see cref="Transform"/> in world space.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The backward direction.</returns>
        public static Vector3 GetBackwardDirection(this Transform transform)
        {
            return -transform.forward;
        }

        /// <summary>
        /// The reverse direction of green axis of the <see cref="Transform"/> in world space.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The down direction.</returns>
        public static Vector3 GetDownDirection(this Transform transform)
        {
            return -transform.up;
        }

        /// <summary>
        /// The reverse direction of red axis of the <see cref="Transform"/> in world space.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The left direction.</returns>
        public static Vector3 GetLeftDirection(this Transform transform)
        {
            return -transform.right;
        }

        /// <summary>
        /// Gets the value of axis <c>x</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>x</c> in the local coordinate.</returns>
        public static float GetLocalPositionX(this Transform transform)
        {
            return transform.localPosition.x;
        }

        /// <summary>
        /// Gets the value of axis <c>y</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>y</c> in the local coordinate.</returns>
        public static float GetLocalPositionY(this Transform transform)
        {
            return transform.localPosition.y;
        }

        /// <summary>
        /// Gets the value of axis <c>z</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>z</c> in the local coordinate.</returns>
        public static float GetLocalPositionZ(this Transform transform)
        {
            return transform.localPosition.z;
        }

        /// <summary>
        /// Gets the value of axis <c>x</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>x</c> in the world coordinate.</returns>
        public static float GetPositionX(this Transform transform)
        {
            return transform.position.x;
        }

        /// <summary>
        /// Gets the value of axis <c>y</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>y</c> in the world coordinate.</returns>
        public static float GetPositionY(this Transform transform)
        {
            return transform.position.y;
        }

        /// <summary>
        /// Gets the value of axis <c>z</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>z</c> in the world coordinate.</returns>
        public static float GetPositionZ(this Transform transform)
        {
            return transform.position.z;
        }

        /// <summary>
        /// Removes the <see cref="Component"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="transform">The <see cref="Transform"/> to remove component.</param>
        /// <param name="delay">The optional amount of time to delay before removing the component.</param>
        /// <returns>
        /// <c>true</c> if remove <see cref="Component"/> successfully, <c>false</c> otherwise.
        /// </returns>
        public static bool RemoveComponent<T>(this Transform transform, float delay = 0.0f) where T : Component
        {
            return transform.gameObject.RemoveComponent<T>(delay);
        }

        /// <summary>
        /// Removes the <see cref="Component"/> immediately.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Component"/>.</typeparam>
        /// <param name="transform">The <see cref="Transform"/> to remove component.</param>
        /// <returns>
        /// <c>true</c> if remove <see cref="Component"/> successfully, <c>false</c> otherwise.
        /// </returns>
        public static bool RemoveComponentImmediate<T>(this Transform transform) where T : Component
        {
            return transform.gameObject.RemoveComponentImmediate<T>();
        }

        /// <summary>
        /// Sets the children layer.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="layer">The layer.</param>
        /// <param name="includeParent">if set to <c>true</c> include parent.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="Transform"/>.</param>
        public static void SetChildrenLayer(this Transform transform, int layer = -1, bool includeParent = true, bool includeInactive = true)
        {
            transform.gameObject.SetChildrenLayer(layer, includeParent, includeInactive);
        }

        /// <summary>
        /// Sets the children layer.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="layerName">The name of the layer.</param>
        /// <param name="includeParent">if set to <c>true</c> include parent.</param>
        /// <param name="includeInactive">if set to <c>true</c> include inactive <see cref="Transform"/>.</param>
        public static void SetChildrenLayer(this Transform transform, string layerName, bool includeParent = true, bool includeInactive = true)
        {
            transform.gameObject.SetChildrenLayer(layerName, includeParent, includeInactive);
        }

        /// <summary>
        /// Sets the value of axis <c>x</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="value">The value of axis <c>x</c> in the local coordinate.</param>
        public static void SetLocalPositionX(this Transform transform, float value)
        {
            if (transform.localPosition.x != value)
            {
                transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
            }
        }

        /// <summary>
        /// Sets the value of axis <c>y</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>y</c> in the local coordinate.</returns>
        public static void SetLocalPositionY(this Transform transform, float value)
        {
            if (transform.localPosition.y != value)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
            }
        }

        /// <summary>
        /// Sets the value of axis <c>z</c> in the local coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>z</c> in the local coordinate.</returns>
        public static void SetLocalPositionZ(this Transform transform, float value)
        {
            if (transform.localPosition.z != value)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, value);
            }
        }

        /// <summary>
        /// Sets the value of axis <c>x</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <param name="value">The value of axis <c>x</c> in the world coordinate.</param>
        public static void SetPositionX(this Transform transform, float value)
        {
            if (transform.position.x != value)
            {
                transform.position = new Vector3(value, transform.position.y, transform.position.z);
            }
        }

        /// <summary>
        /// Sets the value of axis <c>y</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>y</c> in the world coordinate.</returns>
        public static void SetPositionY(this Transform transform, float value)
        {
            if (transform.position.y != value)
            {
                transform.position = new Vector3(transform.position.x, value, transform.position.z);
            }
        }

        /// <summary>
        /// Sets the value of axis <c>z</c> in the world coordinate.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/>.</param>
        /// <returns>The value of axis <c>z</c> in the world coordinate.</returns>
        public static void SetPositionZ(this Transform transform, float value)
        {
            if (transform.position.z != value)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, value);
            }
        }

        #endregion Methods
    }
}