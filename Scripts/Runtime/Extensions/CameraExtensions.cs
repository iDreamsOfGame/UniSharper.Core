// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEnigne.Camera"/>.
    /// </summary>
    public static class CameraExtensions
    {
        private static readonly Rect ScreenRect = new Rect(0, 0, Screen.width, Screen.height);

        public static Texture2D CaptureScreenshotTexture(
            this Camera camera,
            int depth = 0,
            TextureFormat screenshotTextureFormat = TextureFormat.RGBA32) =>
            camera.CaptureScreenshotTexture(ScreenRect, depth, screenshotTextureFormat);

        public static Texture2D CaptureScreenshotTexture(this Camera camera, Rect rect, int depth = 0, TextureFormat screenshotTextureFormat = TextureFormat.RGBA32)
        {
            var canvas  = new RenderTexture((int)ScreenRect.width, (int)ScreenRect.height, depth);
            var originalTargetTexture = camera.targetTexture;
            camera.targetTexture = canvas;
            camera.Render();

            var output = canvas.ToTexture(rect, screenshotTextureFormat);
            camera.targetTexture = originalTargetTexture;
            Object.Destroy(canvas);
            return output;
        }
    }
}