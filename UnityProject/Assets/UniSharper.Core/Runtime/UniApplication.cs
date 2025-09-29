// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Access to application runtime data.
    /// This class contains static methods for looking up information about and controlling the runtime data.
    /// </summary>
    public static class UniApplication
    {
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
        private const string AppUtilsAndroidJavaClassName = "io.github.idreamsofgame.unisharper.plugin.AppUtils";

        private const string NetUtilsAndroidJavaClassName = "io.github.idreamsofgame.unisharper.plugin.NetUtils";
#endif

        /// <summary>
        /// If the internet is reachable.
        /// </summary>
        public static bool IsInternetReachable => Application.internetReachability != NetworkReachability.NotReachable;

        /// <summary>
        /// Opens the URL specified.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        /// <returns><c>true</c>, can open the url; <c>false</c> can not open the url.</returns>
        public static bool OpenURL(string url)
        {
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
            return OpenURLOnAndroid(url);
#endif

            Application.OpenURL(url);
            return true;
        }

        /// <summary>
        /// Quits the player application.
        /// </summary>
        /// <param name="killAndroidProcess">Whether kill Android process. </param>
        public static void Quit(bool killAndroidProcess = false)
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                if (Application.platform == RuntimePlatform.Android)
                {
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
                    try
                    {
                        using var appUtils = new AndroidJavaClass(AppUtilsAndroidJavaClassName);
                        appUtils.CallStatic<bool>("quit", killAndroidProcess);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogWarning($"Application quit has exception {e}");
                    }
                    finally
                    {
                        Application.Quit();
                    }
#else
                    Application.Quit();
#endif
                }
                else
                {
                    Application.Quit();
                }
            }
        }

        /// <summary>
        /// Restarts the application.
        /// </summary>
        public static void Restart()
        {
            if (Application.isEditor)
            {
                Debug.LogWarning("Can not restart app on editor!");
            }
            else
            {
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
                using var appUtils = new AndroidJavaClass(AppUtilsAndroidJavaClassName);
                appUtils.CallStatic("restart");
#endif
            }
        }

#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
        private static bool OpenURLOnAndroid(string url)
        {
            using var netUtils = new AndroidJavaClass(NetUtilsAndroidJavaClassName);
            return netUtils.CallStatic<bool>("openURL", url);
        }
#endif
    }
}