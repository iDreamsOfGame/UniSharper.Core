// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident

namespace UniSharper.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Camera"/>.
    /// </summary>
    public static class CameraExtensions
    {
        private static readonly Rect ScreenRect = new Rect(0, 0, Screen.width, Screen.height);

        /// <summary>
        /// Captures a screenshot of the camera view into a <see cref="UnityEngine.Texture2D"/> object.
        /// </summary>
        /// <param name="camera">The <see cref="UnityEngine.Camera"/> to capture.</param>
        /// <param name="depth">Number of bits in depth buffer (0, 16 or 24). Note that only 24 bit depth has stencil buffer.</param>
        /// <param name="renderTextureFormat">The texture format of render texture. </param>
        /// <param name="screenshotTextureFormat">The texture format of output texture. </param>
        /// <returns>The <see cref="UnityEngine.Texture2D"/> object represents the screenshot of the camera view. </returns>
        public static Texture2D CaptureScreenshotTexture(
            this Camera camera,
            int depth = 0,
            RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGB32,
            TextureFormat screenshotTextureFormat = TextureFormat.RGBA32) =>
            camera.CaptureScreenshotTexture(ScreenRect, depth, renderTextureFormat, screenshotTextureFormat);

        /// <summary>
        /// Captures a screenshot of the camera view in the rectangle area into a <see cref="UnityEngine.Texture2D"/> object.
        /// </summary>
        /// <param name="camera">The <see cref="UnityEngine.Camera"/> to capture.</param>
        /// <param name="rect">The rectangle area to limit screenshot. </param>
        /// <param name="depth">Number of bits in depth buffer (0, 16 or 24). Note that only 24 bit depth has stencil buffer. </param>
        /// <param name="renderTextureFormat">The texture format of render texture. </param>
        /// <param name="screenshotTextureFormat">The texture format of output texture. </param>
        /// <returns>The <see cref="UnityEngine.Texture2D"/> object represents the screenshot of the camera view. </returns>
        public static Texture2D CaptureScreenshotTexture(
            this Camera camera,
            Rect rect,
            int depth = 0,
            RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGB32,
            TextureFormat screenshotTextureFormat = TextureFormat.RGBA32)
        {
            var canvas = new RenderTexture((int)ScreenRect.width, (int)ScreenRect.height, depth, renderTextureFormat)
            {
                antiAliasing = 1,
                filterMode = FilterMode.Bilinear,
                anisoLevel = 0,
                useMipMap = false,
                wrapMode = TextureWrapMode.Clamp
            };

            var originalTargetTexture = camera.targetTexture;
            camera.targetTexture = canvas;
            var old = RenderTexture.active;
            RenderTexture.active = canvas;
            GL.Clear(true, true, Color.clear);
            camera.Render();
            RenderTexture.active = old;

            var output = canvas.ToTexture(rect, screenshotTextureFormat);
            camera.targetTexture = originalTargetTexture;
            Object.Destroy(canvas);
            return output;
        }
    }
}