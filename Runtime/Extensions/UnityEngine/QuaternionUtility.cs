// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Quaternion"/> utilities.
    /// </summary>
    public static class QuaternionUtility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Quaternion"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Quaternion"/> equivalent. </param>
        /// <returns>A <see cref="Quaternion"/> equivalent to the <c>s</c>. </returns>
        public static Quaternion Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;

            var values = StringUtility.GetStringValuesInBrackets(s);
            if (values.Length < 4)
                return default;
            
            float.TryParse(values[0], out var x);
            float.TryParse(values[1], out var y);
            float.TryParse(values[2], out var z);
            float.TryParse(values[3], out var w);
            return new Quaternion(x, y, z, w);
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Quaternion"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Quaternion"/> equivalent. </param>
        /// <param name="result">A <see cref="Quaternion"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Quaternion result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = default;
                return false;
            }

            var values = StringUtility.GetStringValuesInBrackets(s);
            if (values.Length < 4)
            {
                result = default;
                return false;
            }

            float.TryParse(values[0], out var x);
            float.TryParse(values[1], out var y);
            float.TryParse(values[2], out var z);
            float.TryParse(values[3], out var w);
            result = new Quaternion(x, y, z, w);
            return true;
        }
    }
}