// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Color"/> utilities.
    /// </summary>
    public static class ColorUtility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Color"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Color"/> equivalent. </param>
        /// <returns>A <see cref="Color"/> equivalent to the <c>s</c>. </returns>
        public static Color Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;

            var values = StringUtility.GetColorRgbaValues(s);
            switch (values.Length)
            {
                case < 3:
                    return default;
                case 3:
                {
                    float.TryParse(values[0], out var r);
                    float.TryParse(values[1], out var g);
                    float.TryParse(values[2], out var b);
                    return new Color(r, g, b);
                }
                default:
                {
                    float.TryParse(values[0], out var r);
                    float.TryParse(values[1], out var g);
                    float.TryParse(values[2], out var b);
                    float.TryParse(values[3], out var a);
                    return new Color(r, g, b, a);
                }
            }
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Color"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Color"/> equivalent. </param>
        /// <param name="result">A <see cref="Color"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Color result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = default;
                return false;
            }

            var values = StringUtility.GetColorRgbaValues(s);
            switch (values.Length)
            {
                case < 3:
                    result = default;
                    return false;
                case 3:
                {
                    float.TryParse(values[0], out var r);
                    float.TryParse(values[1], out var g);
                    float.TryParse(values[2], out var b);
                    result = new Color(r, g, b);
                    break;
                }
                default:
                {
                    float.TryParse(values[0], out var r);
                    float.TryParse(values[1], out var g);
                    float.TryParse(values[2], out var b);
                    float.TryParse(values[3], out var a);
                    result = new Color(r, g, b, a);
                    break;
                }
            }

            return true;
        }
    }
}