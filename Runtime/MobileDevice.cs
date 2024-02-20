// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

// ReSharper disable once RedundantUsingDirective

using UniSharper.Extensions;

namespace UniSharper
{
    /// <summary>
    /// Interface into mobile specific functionality.
    /// </summary>
    public sealed class MobileDevice
    {
        /// <summary>
        /// Request mobile app rating and review from the user.
        /// </summary>
        /// <param name="googlePlayReviewInAppUrl">The URL of Google Play review in app.</param>
        /// <param name="googlePlayReviewInBrowserUrl">The URL of Google Play review in browser.</param>
        /// <param name="appStoreReviewUrl">The URL of App Store review.</param>
        public static void RequestStoreReview(string googlePlayReviewInAppUrl, string googlePlayReviewInBrowserUrl, string appStoreReviewUrl)
        {
#if UNITY_ANDROID
            if (!UniApplication.OpenURL(googlePlayReviewInAppUrl))
                UniApplication.OpenURL(googlePlayReviewInBrowserUrl);
#elif UNITY_IOS
            if (!UnityEngine.iOS.Device.RequestStoreReview())
                UniApplication.OpenURL(appStoreReviewUrl);
#endif
        }
    }
}