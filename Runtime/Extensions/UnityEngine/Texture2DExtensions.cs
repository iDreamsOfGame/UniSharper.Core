// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace UniSharper.Extensions
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
        /// <param name="depth">Number of bits in depth buffer (0, 16 or 24). Note that only 24 bit depth has stencil buffer.</param>
        /// <param name="canvasRenderTextureFormat">The texture format of canvas render texture. </param>
        /// <returns>The texture that has blended background texture and overlay texture.</returns>
        /// <exception cref="ArgumentNullException"><c>overlay</c> is <c>null</c>. </exception>
        public static Texture2D BlendTextureOverlay(
            this Texture2D source,
            Texture2D overlay,
            Vector2Int overlayPosition,
            int depth = 0,
            RenderTextureFormat canvasRenderTextureFormat = RenderTextureFormat.ARGB32)
        {
            if (!overlay)
                throw new ArgumentNullException(nameof(overlay));

            if (SystemInfo.supportsComputeShaders)
            {
                var renderTexture = new RenderTexture(source.width, source.height, depth, canvasRenderTextureFormat) { enableRandomWrite = true };
                renderTexture.Create();

                var shader = Resources.Load<ComputeShader>("Shaders/BlendTextureOverlay");
                var kernel = shader.FindKernel("blendTextureOverlay");
                shader.SetTexture(kernel, "source", source);
                shader.SetTexture(kernel, "overlay", overlay);
                shader.SetInts("overlayDimensions", overlay.width, overlay.height);
                shader.SetInts("overlayPosition", overlayPosition.x, overlayPosition.y);
                shader.SetTexture(kernel, "canvas", renderTexture);
                shader.Dispatch(kernel, source.width, source.height, 1);
                var output = renderTexture.ToTexture(new Rect(0, 0, renderTexture.width, renderTexture.height));
                UnityObject.Destroy(renderTexture);
                return output;
            }
            else
            {
                Debug.LogWarning($"The platform {Application.platform} do not support compute shader, so blend texture will use CPU to process!");
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
        
        /// <summary>
        /// Resize the texture and generate new texture instance.
        /// </summary>
        /// <param name="source">The original texture. </param>
        /// <param name="width">The width of new texture. </param>
        /// <param name="height">The height of new texture. </param>
        /// <param name="preserveAspect">If keep the aspect of original texture. </param>
        /// <param name="mipmap">Generate mipmap or not. </param>
        /// <param name="filter">The filter mode of texture. </param>
        /// <returns></returns>
        public static Texture2D Resize(this Texture2D source,
            int width,
            int height,
            bool preserveAspect,
            bool mipmap = true,
            FilterMode filter = FilterMode.Bilinear)
        {
            var targetWidth = width;
            var targetHeight = height;

            if (preserveAspect)
            {
                var scaleWidth = width / (float)source.width;
                var scaleHeight = height / (float)source.height;
                var scale = Mathf.Min(scaleWidth, scaleHeight);
                targetWidth = Mathf.FloorToInt(source.width * scale);
                targetHeight = Mathf.FloorToInt(source.height * scale);
            }

            // Create a temporary RenderTexture with the target size
            var renderTexture = RenderTexture.GetTemporary(targetWidth, targetHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);

            // Set the active RenderTexture to the temporary texture so we can read from it
            var previous = RenderTexture.active;
            RenderTexture.active = renderTexture;

            // Copy the texture data on the GPU - this is where the magic happens [(;]
            Graphics.Blit(source, renderTexture);
            
            // Create new texture
            var newTexture = new Texture2D(source.width, source.height, source.format, false);
            newTexture.Reinitialize(targetWidth, targetHeight, newTexture.format, mipmap);
            newTexture.filterMode = filter;

            try
            {
                // Reads the pixel values from the temporary RenderTexture onto the resized texture
                newTexture.ReadPixels(new Rect(0f, 0f, targetWidth, targetHeight), 0, 0);
                
                // Actually upload the changed pixels to the graphics card
                newTexture.Apply();
            }
            catch
            {
                Debug.LogError("Read/Write is not enabled on texture " + source.name);
            }

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);

            return newTexture;
        }
    }
}