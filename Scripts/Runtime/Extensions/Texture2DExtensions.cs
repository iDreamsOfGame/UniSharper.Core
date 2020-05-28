// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Texture2D"/>.
    /// </summary>
    public static class Texture2DExtensions
    {
        /// <summary>
        /// Blend other texture as overlay.
        /// </summary>
        /// <param name="source">The texture background. </param>
        /// <param name="overlay">The texture overlay. </param>
        /// <param name="overlayPosition">The position that overlay texture will be blended in background texture. </param>
        /// <returns>The texture that has blended background texture and overlay texture.</returns>
        /// <exception cref="ArgumentNullException"><c>overlay</c> is <c>null</c>. </exception>
        public static Texture2D BlendTextureOverlay(
            this Texture2D source,
            Texture2D overlay,
            Vector2Int overlayPosition)
        {
            if(!overlay)
                throw new ArgumentNullException(nameof(overlay));
            
            if (SystemInfo.supportsComputeShaders) 
            {
                var renderTexture = new RenderTexture(source.width, source.height, 24) { enableRandomWrite = true };
                renderTexture.Create();

                var shader = Resources.Load<ComputeShader>("Shaders/BlendTextureOverlay");
                var kernel = shader.FindKernel("blendTextureOverlay");
                shader.SetTexture(kernel, "source", source);
                shader.SetTexture(kernel, "overlay", overlay);
                shader.SetInts("overlayDimensions", overlay.width, overlay.height);
                shader.SetInts("overlayPosition", overlayPosition.x, overlayPosition.y);
                shader.SetTexture(kernel, "output", renderTexture);
                shader.Dispatch(kernel, source.width, source.height, 1);
                var output = renderTexture.ToTexture(new Rect(0, 0, renderTexture.width, renderTexture.height));
                Object.Destroy(renderTexture);
                return output;
            }
            else
            {
                Debug.LogWarning($"The platform {Application.platform.ToString()} do not support compute shader, so blend texture will use CPU to process!");
                var output = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false)
                {
                    wrapMode = TextureWrapMode.Clamp
                };

                for (var x = 0; x < source.width; x++)
                {
                    for (var y = 0; y < source.height; y++)
                    {
                        var color1 = source.GetPixel(x, y);
                        var color2 = Color.clear;
                        var position = new Vector2Int(x - overlayPosition.x, y - overlayPosition.y);

                        if (0 <= position.x && position.x <= overlay.width && 0 <= position.y && position.y <= overlay.height)
                        {
                            color2 = overlay.GetPixel(position.x, position.y);
                        }

                        output.SetPixel(x, y, color1 + color2);
                    }
                }
            
                output.Apply();
                return output;
            }
        }
    }
}