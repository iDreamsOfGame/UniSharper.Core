// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// Provides utilities to handle network operation. This class cannot be inherited.
    /// </summary>
    public static class NetworkUtility
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        private const string AndroidJavaClassName = "io.github.idreamsofgame.unisharper.plugin.NetUtils";
#endif
        
        /// <summary>
        /// If the internet is reachable.
        /// </summary>
        public static bool IsInternetReachable => Application.internetReachability != NetworkReachability.NotReachable;

        /// <summary>
        /// Opens the URL specified.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        /// <returns><c>true</c>, If can open the url; <c>false</c> can not open the url.</returns>
        public static bool OpenURL(string url)
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            return OpenURLOnAndroid(url);
#endif
            
            Application.OpenURL(url);
            return true;
        }

#if !UNITY_EDITOR && UNITY_ANDROID
        private static bool OpenURLOnAndroid(string url)
        {
            using var netUtils = new AndroidJavaClass(AndroidJavaClassName);
            return netUtils.CallStatic<bool>("openURL", url);
        }
#endif
    }
}