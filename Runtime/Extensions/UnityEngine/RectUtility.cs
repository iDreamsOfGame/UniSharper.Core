// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Rect"/> utilities.
    /// </summary>
    public static class RectUtility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Rect"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Rect"/> equivalent. </param>
        /// <returns>A <see cref="Rect"/> equivalent to the <c>s</c>. </returns>
        public static Rect Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;

            var pairs = StringUtility.GetKeyValueStringPairsInBrackets(s);
            if (pairs.Count < 4)
                return default;
            
            pairs.TryGetValue("x", out var xString);
            pairs.TryGetValue("y", out var yString);
            pairs.TryGetValue("width", out var widthString);
            pairs.TryGetValue("height", out var heightString);
            float.TryParse(xString, out var x);
            float.TryParse(yString, out var y);
            float.TryParse(widthString, out var width);
            float.TryParse(heightString, out var height);
            return new Rect(x, y, width, height);
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Rect"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Rect"/> equivalent. </param>
        /// <param name="result">A <see cref="Rect"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Rect result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = default;
                return false;
            }

            var pairs = StringUtility.GetKeyValueStringPairsInBrackets(s);
            if (pairs.Count < 4)
            {
                result = default;
                return false;
            }

            pairs.TryGetValue("x", out var xString);
            pairs.TryGetValue("y", out var yString);
            pairs.TryGetValue("width", out var widthString);
            pairs.TryGetValue("height", out var heightString);
            float.TryParse(xString, out var x);
            float.TryParse(yString, out var y);
            float.TryParse(widthString, out var width);
            float.TryParse(heightString, out var height);
            result = new Rect(x, y, width, height);
            return true;
        }
    }
}