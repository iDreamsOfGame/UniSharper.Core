﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

// ReSharper disable RedundantUsingDirective

using System.Globalization;
using ReSharp.Security.Cryptography;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Provides information about, and means to manipulate, the current unity player environment
    /// and platform. This class cannot be inherited.
    /// </summary>
    public static class PlayerEnvironment
    {
        /// <summary>
        /// The folder name of <c>Assets</c>.
        /// </summary>
        public const string AssetsFolderName = "Assets";
        
        /// <summary>
        /// The folder name of <c>Packages</c>.
        /// </summary>
        public const string PackagesFolderName = "Packages";
        
        /// <summary>
        /// Gets the newline string defined for windows.
        /// </summary>
        /// <value>The newline string defined for windows.</value>
        public const string WindowsNewLine = "\r\n";

#if !UNITY_EDITOR && UNITY_ANDROID
        private const string AndroidDeviceInfoClassFullPath = "io.github.idreamsofgame.unisharper.plugin.DeviceInfo";
#endif

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Android.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Android; otherwise, <c>false</c>.</value>
        public static bool IsAndroidPlatform => Application.platform == RuntimePlatform.Android;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Unity Editor.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Unity Editor; otherwise, <c>false</c>.</value>
        public static bool IsEditorPlatform => IsWindowsEditorPlatform || IsMacOSXEditorPlatform || IsLinuxEditorPlatform;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is iOS.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is iOS is ios platform; otherwise, <c>false</c>.</value>
        public static bool IsiOSPlatform => Application.platform == RuntimePlatform.IPhonePlayer;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Linux Unity Editor.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Linux Unity Editor; otherwise, <c>false</c>.</value>
        public static bool IsLinuxEditorPlatform => Application.platform == RuntimePlatform.LinuxEditor;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Mac OS X Unity Editor.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Mac OS X Unity Editor; otherwise, <c>false</c>.</value>
        public static bool IsMacOSXEditorPlatform => Application.platform == RuntimePlatform.OSXEditor;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Windows Unity Editor.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Windows Unity Editor; otherwise, <c>false</c>.</value>
        public static bool IsWindowsEditorPlatform => Application.platform == RuntimePlatform.WindowsEditor;

        /// <summary>
        /// Gets a unique device identifier. It is guaranteed to be unique for every device (Read Only).
        /// </summary>
        /// <value>A unique device identifier.</value>
        public static string UniqueDeviceIdentifier
        {
            get
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                return GetAndroidDeviceIdentifier();
#endif
                
                return SystemInfo.deviceUniqueIdentifier;
            }
        }

        /// <summary>
        /// Gets the SDK version of the software currently running on Android.
        /// </summary>
        public static int AndroidSdkInt
        {
            get
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                var os = SystemInfo.operatingSystem;
                var sections = os.Split(' ');

                foreach (var section in sections)
                {
                    var stringArray = section.Split('-');
                    if (stringArray.Length <= 1 || stringArray[0] != "API") 
                        continue;
                    
                    int.TryParse(stringArray[1], out var sdkInt);
                    return sdkInt;
                }
#endif
                
                return 0;
            }
        }

        public static string CountryCode
        {
            get
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                return GetAndroidCountryCode();
#endif
                
                return RegionInfo.CurrentRegion.Name;
            }
        }

#if !UNITY_EDITOR && UNITY_ANDROID
        private static string GetAndroidDeviceIdentifier()
        {
            using var deviceInfo = new AndroidJavaClass(AndroidDeviceInfoClassFullPath);
            const string methodName = "getUniqueDeviceIdentifier";
            var uniqueDeviceIdentifier = deviceInfo.CallStatic<string>(methodName);
            return CryptoUtility.Md5HashEncrypt(uniqueDeviceIdentifier);
        }

        private static string GetAndroidCountryCode()
        {
            using var deviceInfo = new AndroidJavaClass(AndroidDeviceInfoClassFullPath);
            const string methodName = "getCountryCode";
            return deviceInfo.CallStatic<string>(methodName);
        }
#endif
    }
}