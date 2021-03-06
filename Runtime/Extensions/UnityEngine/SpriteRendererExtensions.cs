// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.SpriteRenderer"/>.
    /// </summary>
    public static class SpriteRendererExtensions
    {
        #region Methods

        /// <summary>
        /// Set the alpha value of <see cref="UnityEngine.SpriteRenderer"/>.
        /// </summary>
        /// <param name="spriteRenderer">The <see cref="UnityEngine.SpriteRenderer"/> object.</param>
        /// <param name="alpha">The new alpha value.</param>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
        }

        #endregion Methods
    }
}