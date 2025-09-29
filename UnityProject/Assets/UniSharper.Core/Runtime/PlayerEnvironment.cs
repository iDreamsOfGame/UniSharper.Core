// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

// ReSharper disable RedundantUsingDirective

using System;
using System.Globalization;
using ReSharp.Security.Cryptography;
using UnityEngine;
using UnityEngine.Device;
using Application = UnityEngine.Device.Application;
using SystemInfo = UnityEngine.Device.SystemInfo;

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

#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
        private const string AndroidBuildClassFullPath = "android.os.Build";

        private const string AndroidDeviceInfoClassFullPath = "io.github.idreamsofgame.unisharper.plugin.DeviceInfo";
#endif

        /// <summary>
        /// Gets a value that indicates whether the current process is a 64-bit process.
        /// </summary>
        public static bool Is64BitProcess => IntPtr.Size == 8;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Android.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Android; otherwise, <c>false</c>.</value>
        public static bool IsAndroidPlatform => Application.platform == RuntimePlatform.Android;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is iOS.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is iOS platform; otherwise, <c>false</c>.</value>
        public static bool IsiOSPlatform => Application.platform == RuntimePlatform.IPhonePlayer;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Linux Unity Editor.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Linux Unity Editor; otherwise, <c>false</c>.</value>
        public static bool IsLinuxEditor => UnityEngine.Application.platform == RuntimePlatform.LinuxEditor;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Mac OS X Unity Editor.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Mac OS X Unity Editor; otherwise, <c>false</c>.</value>
        public static bool IsMacOSXEditor => UnityEngine.Application.platform == RuntimePlatform.OSXEditor;

        /// <summary>
        /// Gets a value indicating whether the runtime platform is Windows Unity Editor.
        /// </summary>
        /// <value><c>true</c> if the runtime platform is Windows Unity Editor; otherwise, <c>false</c>.</value>
        public static bool IsWindowsEditor => UnityEngine.Application.platform == RuntimePlatform.WindowsEditor;

        private static string deviceIdentifierOnWebGL;

        /// <summary>
        /// Gets a unique device identifier. It is guaranteed to be unique for every device (Read Only).
        /// </summary>
        /// <value>A unique device identifier.</value>
        public static string UniqueDeviceIdentifier
        {
            get
            {
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
                return GetAndroidDeviceIdentifier();
#elif UNITY_WEBGL || WEIXINMINIGAME
                if (string.IsNullOrEmpty(deviceIdentifierOnWebGL))
                {
                    deviceIdentifierOnWebGL = CryptoUtility.Md5HashEncrypt($"{Guid.NewGuid():N}-{SystemInfo.deviceName}-{SystemInfo.deviceModel}-"
                                                                           + $"{SystemInfo.deviceType}-{SystemInfo.graphicsDeviceType}-{SystemInfo.graphicsDeviceID}");
                }

                return deviceIdentifierOnWebGL;
#else
                return SystemInfo.deviceUniqueIdentifier;
#endif
            }
        }

        /// <summary>
        /// Gets the hardware serial number, if available.
        /// </summary>
        public static string SerialNumber
        {
            get
            {
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
                try
                {
                    using var build = new AndroidJavaClass(AndroidBuildClassFullPath);
                    return AndroidSdkInt >= 26 ? build.CallStatic<string>("getSerial") : build.GetStatic<string>("SERIAL");
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e.ToString());
                    return null;
                }
#else
                return UniqueDeviceIdentifier;
#endif
            }
        }

        /// <summary>
        /// Gets the SDK version of the software currently running on Android.
        /// </summary>
        public static int AndroidSdkInt
        {
            get
            {
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
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
#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
                return GetAndroidCountryCode();
#endif

                return RegionInfo.CurrentRegion.TwoLetterISORegionName;
            }
        }

#if !UNITY_EDITOR && UNITY_ANDROID && UNITY_ANDROID_JNI_MODULE
        private static string GetAndroidDeviceIdentifier()
        {
            using (var deviceInfo = new AndroidJavaClass(AndroidDeviceInfoClassFullPath))
            {
                const string methodName = "getUniqueDeviceIdentifier";
                var uniqueDeviceIdentifier = deviceInfo.CallStatic<string>(methodName);
                return CryptoUtility.Md5HashEncrypt(uniqueDeviceIdentifier);
            }
        }

        private static string GetAndroidCountryCode()
        {
            using (var deviceInfo = new AndroidJavaClass(AndroidDeviceInfoClassFullPath))
            {
                const string methodName = "getCountryCode";
                return deviceInfo.CallStatic<string>(methodName);
            }
        }
#endif
    }
}