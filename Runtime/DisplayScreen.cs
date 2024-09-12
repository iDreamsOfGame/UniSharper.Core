// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Provides access to display screen information.
    /// </summary>
    public sealed class DisplayScreen
    {
        private static Rect safeArea;

        /// <summary>
        /// The safe area of the screen in pixels,  or the specific safe area you set.
        /// </summary>
        public static Rect SafeArea
        {
            get
            {
                if (safeArea == Rect.zero)
                {
#if UNITY_2021_1_OR_NEWER
                    return UnityEngine.Device.Screen.safeArea;
#else
                    return Screen.safeArea;
#endif
                }
                
                return safeArea;
            }
            set => safeArea = value;
        }

        /// <summary>
        /// The notch area on the top of display screen.
        /// </summary>
        public static Rect TopNotchArea
        {
            get
            {
                var x = SafeArea.x;
                var y = SafeArea.y + SafeArea.height;
                var width = SafeArea.width;
                var height = Screen.height - y;
                return new Rect(x, y, width, height);
            }
        }

        /// <summary>
        /// The notch area on the bottom of display screen.
        /// </summary>
        public static Rect BottomNotchArea
        {
            get
            {
                var x = SafeArea.x;
                const float y = 0f;
                var width = SafeArea.width;
                var height = SafeArea.y;
                return new Rect(x, y, width, height);
            }
        }

        /// <summary>
        /// The notch area on the left of display screen.
        /// </summary>
        public static Rect LeftNotchArea
        {
            get
            {
                const float x = 0f;
                const float y = 0f;
                var width = SafeArea.x;
                var height = SafeArea.height;
                return new Rect(x, y, width, height);
            }
        }

        /// <summary>
        /// The notch area on the right of display screen.
        /// </summary>
        public static Rect RightNotchArea
        {
            get
            {
                var x = SafeArea.x + SafeArea.width;
                const float y = 0f;
                var width = Screen.width - x;
                var height = SafeArea.height;
                return new Rect(x, y, width, height);
            }
        }
    }
}