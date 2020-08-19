// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.RenderTexture"/>.
    /// </summary>
    public static class RenderTextureExtensions
    {
        /// <summary>
        /// Converts <see cref="UnityEngine.RenderTexture"/> to <see cref="UnityEngine.Texture2D"/>.
        /// </summary>
        /// <param name="renderTexture">The <see cref="UnityEngine.RenderTexture"/> object as source to be converted to <see cref="UnityEngine.Texture2D"/>. </param>
        /// <param name="rect">Rectangular region of the view to read from. </param>
        /// <param name="textureFormat">The format of output texture. </param>
        /// <returns>The texture that draw in <see cref="UnityEngine.RenderTexture"/>. </returns>
        public static Texture2D ToTexture(this RenderTexture renderTexture, Rect rect, TextureFormat textureFormat = TextureFormat.RGBA32)
        {
            var originalActiveRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;
            var output = new Texture2D((int)rect.width, (int)rect.height, textureFormat, false, true);
            output.wrapMode = TextureWrapMode.Clamp;
            output.ReadPixels(rect, 0, 0);
            output.Apply();
            RenderTexture.active = originalActiveRenderTexture;
            return output;
        }
    }
}