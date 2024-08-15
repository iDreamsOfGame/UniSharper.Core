// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

#if UNITY_WEBGL
namespace UniSharper
{
    /// <summary>
    /// Provides access to WebGL navigator object.
    /// </summary>
    public sealed class Navigator
    {
#if !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string GetUserAgent();
        
        [DllImport("__Internal")]
        private static extern bool IsMobilePlatform();
#endif

        /// <summary>
        /// The user agent string for the current browser.
        /// </summary>
        public static string UserAgent
        {
            get
            {
#if !UNITY_EDITOR
                return GetUserAgent();
#else
                return string.Empty;
#endif
            }
        }

        /// <summary>
        /// Detects if it is a mobile platform.
        /// </summary>
        public static bool IsMobile
        {
            get
            {
#if !UNITY_EDITOR
                return IsMobilePlatform();
#else
                return false;
#endif
            }
        }
    }
}
#endif