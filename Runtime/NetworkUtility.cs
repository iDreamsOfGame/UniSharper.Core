// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Provides utilities to handle network operation. This class cannot be inherited.
    /// </summary>
    public static class NetworkUtility
    {
        #region Fields

        private const string AndroidJavaClassName = "io.github.idreamsofgame.unisharper.core.NetUtils";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Opens the URL specified.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        /// <returns><c>true</c>, If can open the url; <c>false</c> can not open the url.</returns>
        public static bool OpenURL(string url)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    return OpenURLOnAndroid(url);

                default:
                    Application.OpenURL(url);
                    return true;
            }
        }

        private static bool OpenURLOnAndroid(string url)
        {
            using (var netUtils = new AndroidJavaClass(AndroidJavaClassName))
            {
                return netUtils.CallStatic<bool>("openURL", url);
            }
        }

        #endregion Methods
    }
}