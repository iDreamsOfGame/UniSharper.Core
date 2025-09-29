// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

// ReSharper disable RedundantUsingDirective

using UnityEngine;

// ReSharper disable RedundantJumpStatement

namespace UniSharper
{
    /// <summary>
    /// Provides access to Android widgets.
    /// </summary>
    public sealed class AndroidWidget
    {
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
        private const string AndroidWidgetUtilClassFullPath = "io.github.idreamsofgame.unisharper.plugin.WidgetUtil";
#endif

        /// <summary>
        /// Show native toast message of Android.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="longDuration"></param>
        public static void ShowToast(string message, bool longDuration = false)
        {
            if (string.IsNullOrEmpty(message))
                return;

#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
            using (var widgetUtil = new AndroidJavaClass(AndroidWidgetUtilClassFullPath))
            {
                const string methodName = "showToast";
                widgetUtil.CallStatic(methodName, message, longDuration);
            }
#endif
        }
    }
}