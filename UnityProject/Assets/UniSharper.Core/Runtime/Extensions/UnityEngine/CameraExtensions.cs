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
        private static readonly Rect FullScreenRect = new Rect(0, 0, DisplayScreen.Width, DisplayScreen.Height);

        /// <summary>
        /// Converts a rectangle in screen coordinates to a rectangle in viewport coordinates.
        /// </summary>
        /// <param name="camera">The <see cref="UnityEngine.Camera"/> object. </param>
        /// <param name="screenRect">A rectangle in screen coordinates. </param>
        /// <returns>The rectangle in viewport coordinates. </returns>
        public static Rect ScreenRectToViewportRect(this Camera camera, Rect screenRect)
        {
            var viewportX = screenRect.x / FullScreenRect.width;
            var viewportY = screenRect.y / FullScreenRect.height;
            var viewportWidth = screenRect.width / FullScreenRect.width;
            var viewportHeight = screenRect.height / FullScreenRect.height;
            return new Rect(viewportX, viewportY, viewportWidth, viewportHeight);
        }

        /// <summary>
        /// Converts a rectangle in viewport coordinates to a rectangle in world coordinates.
        /// </summary>
        /// <param name="camera">The <see cref="UnityEngine.Camera"/> object. </param>
        /// <param name="viewportRect">A rectangle in viewport coordinates. </param>
        /// <returns>The rectangle in world coordinates. </returns>
        public static Rect ViewportRectToWorldRect(this Camera camera, Rect viewportRect)
        {
            var corners = new[] {
                new Vector3(viewportRect.x, viewportRect.y, 0),                 // Bottom Left
                new Vector3(viewportRect.x, viewportRect.yMax, 0),              // Top Left
                new Vector3(viewportRect.xMax, viewportRect.y, 0),              // Bottom Right
                new Vector3(viewportRect.xMax, viewportRect.yMax, 0)             // Top Right
            };
            
            for (var i = 0; i < 4; i++)
            {
                corners[i] = camera.ViewportToWorldPoint(corners[i]);
            }

            var worldX = corners[0].x;
            var worldY = corners[0].y;
            var worldWidth = corners[2].x - worldX;
            var worldHeight = corners[1].y - worldY;
            return new Rect(worldX, worldY, worldWidth, worldHeight);
        }

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
            camera.CaptureScreenshotTexture(FullScreenRect, depth, renderTextureFormat, screenshotTextureFormat);

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
            var canvas = new RenderTexture((int)FullScreenRect.width, (int)FullScreenRect.height, depth, renderTextureFormat)
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