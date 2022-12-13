// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.RangeInt"/> utilities.
    /// </summary>
    public static class RangeIntUtility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="RangeInt"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="RangeInt"/> equivalent. </param>
        /// <returns>A <see cref="RangeInt"/> equivalent to the <c>s</c>. </returns>
        public static RangeInt Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;
            
            var values = StringUtility.GetStringValuesInBrackets(s);
            if (values.Length < 2)
                return default;

            int.TryParse(values[0], out var start);
            int.TryParse(values[1], out var length);
            return new RangeInt(start, length);
        }

        /// <summary>
        /// Converts the string representation of a <see cref="RangeInt"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="RangeInt"/> equivalent. </param>
        /// <param name="result">A <see cref="RangeInt"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out RangeInt result)
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
            
            int.TryParse(values[0], out var start);
            int.TryParse(values[1], out var length);
            result = new RangeInt(start, length);
            return true;
        }
    }
}