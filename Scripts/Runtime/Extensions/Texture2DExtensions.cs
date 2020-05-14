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
        /// Blend this texture as background with other texture which will be foreground.
        /// </summary>
        /// <param name="source">The texture will be background. </param>
        /// <param name="foregroundTexture">The texture will be foreground. </param>
        /// <param name="foregroundTexturePosition">The position that foreground texture will be in background texture. </param>
        /// <returns>The composite texture which blended background texture and foreground texture.</returns>
        /// <exception cref="ArgumentNullException"><c>foregroundTexture</c> is <c>null</c>. </exception>
        public static Texture2D BlendTexture(
            this Texture2D source,
            Texture2D foregroundTexture,
            Vector2Int foregroundTexturePosition)
        {
            if(!foregroundTexture)
                throw new ArgumentNullException(nameof(foregroundTexture));
            
            if (SystemInfo.supportsComputeShaders)
            {
                var renderTexture = new RenderTexture(source.width, source.height, 24) { enableRandomWrite = true };
                renderTexture.Create();

                var shader = Resources.Load<ComputeShader>("Shaders/BlendTexture");
                var kernel = shader.FindKernel("BlendTexture");
                shader.SetTexture(kernel, "backgroundTexture", source);
                shader.SetTexture(kernel, "foregroundTexture", foregroundTexture);
                shader.SetInts("foregroundTexturePosition", foregroundTexturePosition.x, foregroundTexturePosition.y);
                shader.SetTexture(kernel, "result", renderTexture);
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

                        if (x - foregroundTexturePosition.x >= 0 && y - foregroundTexturePosition.y >= 0)
                        {
                            color2 = foregroundTexture.GetPixel(x - foregroundTexturePosition.x, y - foregroundTexturePosition.y);
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