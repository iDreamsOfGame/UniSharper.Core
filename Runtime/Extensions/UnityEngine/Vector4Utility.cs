// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Vector4"/> utilities.
    /// </summary>
    public static class Vector4Utility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Vector4"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector4"/> equivalent. </param>
        /// <returns>A <see cref="Vector4"/> equivalent to the <c>s</c>. </returns>
        public static Vector4 Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;

            var values = StringUtility.GetStringValuesInBrackets(s);
            switch (values.Length)
            {
                case < 2:
                    return default;
                case 2:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    return new Vector4(x, y);
                }
                case 3:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    float.TryParse(values[2], out var z);
                    return new Vector4(x, y, z);
                }
                default:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    float.TryParse(values[2], out var z);
                    float.TryParse(values[3], out var w);
                    return new Vector4(x, y, z, w);
                }
            }
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Vector4"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector4"/> equivalent. </param>
        /// <param name="result">A <see cref="Vector4"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Vector4 result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = default;
                return false;
            }

            var values = StringUtility.GetStringValuesInBrackets(s);
            switch (values.Length)
            {
                case < 2:
                    result = default;
                    return false;
                case 2:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    result = new Vector4(x, y);
                    break;
                }
                case 3:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    float.TryParse(values[2], out var z);
                    result = new Vector4(x, y, z);
                    break;
                }
                default:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    float.TryParse(values[2], out var z);
                    float.TryParse(values[3], out var w);
                    result = new Vector4(x, y, z, w);
                    break;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Converts the string representation of an <see cref="Vector4"/> array equivalent.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Vector4"/> array equivalent. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns>An <see cref="Vector4"/> array equivalent to the <c>s</c>. </returns>
        public static Vector4[] ParseArray(string s, string arrayElementSeparator = "|")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(arrayElementSeparator))
                return default;
            
            var elementStrings = s.Trim().Split(arrayElementSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (elementStrings.Length == 0)
                return default;

            var result = new Vector4[elementStrings.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Parse(elementStrings[i]);
            }

            return result;
        }

        /// <summary>
        /// Converts the string representation of an <see cref="Vector4"/> array equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Vector4"/> array equivalent. </param>
        /// <param name="result">An <see cref="Vector4"/> array equivalent to the <c>s</c>. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(string s, out Vector4[] result, string arrayElementSeparator = "|")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(arrayElementSeparator))
            {
                result = default;
                return false;
            }

            var elementStrings = s.Trim().Split(arrayElementSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (elementStrings.Length == 0)
            {
                result = default;
                return false;
            }

            var list = new List<Vector4>(elementStrings.Length);
            foreach (var elementString in elementStrings)
            {
                if(TryParse(elementString, out var element))
                    list.Add(element);
            }
            
            result = list.ToArray();
            return true;
        }
    }
}