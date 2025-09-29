// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

#if UNITY_WEBGL
namespace UniSharper.WebGL
{
    /// <summary>
    /// Provides access to WebGL window object.
    /// </summary>
    public sealed class Window
    {
#if !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern double GetDevicePixelRatio();
#endif

        /// <summary>
        /// Gets the ratio of the resolution in physical pixels to the resolution.
        /// </summary>
        public static double DevicePixelRatio
        {
            get
            {
#if !UNITY_EDITOR
                return GetDevicePixelRatio();
#else
                return 1;
#endif
            }
        }
    }
}
#endif