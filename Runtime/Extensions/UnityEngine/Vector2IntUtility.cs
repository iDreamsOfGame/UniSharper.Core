// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Vector2Int"/> utilities.
    /// </summary>
    public static class Vector2IntUtility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Vector2Int"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector2Int"/> equivalent. </param>
        /// <returns>A <see cref="Vector2Int"/> equivalent to the <c>s</c>. </returns>
        public static Vector2Int Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;
            
            var values = StringUtility.GetStringValuesInBrackets(s);
            if (values.Length < 2)
                return default;

            int.TryParse(values[0], out var x);
            int.TryParse(values[1], out var y);
            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Vector2Int"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector2Int"/> equivalent. </param>
        /// <param name="result">A <see cref="Vector2Int"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Vector2Int result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = default;
                return false;
            }
            
            var values = StringUtility.GetStringValuesInBrackets(s);
            if (values.Length < 2)
            {
                result = default;
                return false;
            }
            
            int.TryParse(values[0], out var x);
            int.TryParse(values[1], out var y);
            result = new Vector2Int(x, y);
            return true;
        }
    }
}