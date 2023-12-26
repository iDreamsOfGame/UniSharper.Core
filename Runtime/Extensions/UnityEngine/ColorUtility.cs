// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
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

            var values = StringUtility.GetColorRgbaStringValues(s);
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

            var values = StringUtility.GetColorRgbaStringValues(s);
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
        
        /// <summary>
        /// Converts the string representation of an <see cref="Color"/> array equivalent.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Color"/> array equivalent. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns>An <see cref="Color"/> array equivalent to the <c>s</c>. </returns>
        public static Color[] ParseArray(string s, string arrayElementSeparator = "|")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(arrayElementSeparator))
                return default;
            
            var elementStrings = s.Trim().Split(arrayElementSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (elementStrings.Length == 0)
                return default;

            var result = new Color[elementStrings.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Parse(elementStrings[i]);
            }

            return result;
        }
        
        /// <summary>
        /// Converts the collection of <see cref="float"/> representation of an <see cref="Color"/> array equivalent.
        /// </summary>
        /// <param name="values">The collection of <see cref="float"/> representation of an <see cref="Color"/> array equivalent.</param>
        /// <returns>An <see cref="Color"/> array equivalent to the <c>values</c>. </returns>
        public static Color[] ParseArray(IList<float> values)
        {
            var list = new List<Color>();
            for (var i = 0; i < values.Count; i += 4)
            {
                var r = values[i];
                var g = values[i + 1];
                var b = values[i + 2];
                var a = values[i + 3];
                list.Add(new Color(r, g, b, a));
            }

            return list.ToArray();
        }

        /// <summary>
        /// Converts the string representation of an <see cref="Color"/> array equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Color"/> array equivalent. </param>
        /// <param name="result">An <see cref="Color"/> array equivalent to the <c>s</c>. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(string s, out Color[] result, string arrayElementSeparator = "|")
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

            var list = new List<Color>(elementStrings.Length);
            foreach (var elementString in elementStrings)
            {
                if(TryParse(elementString, out var element))
                    list.Add(element);
            }
            
            result = list.ToArray();
            return true;
        }
        
        /// <summary>
        /// Converts the collection of <see cref="float"/> representation of an <see cref="Color"/> array equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="values">The collection of <see cref="float"/> representation of an <see cref="Color"/> array equivalent. </param>
        /// <param name="result">An <see cref="Color"/> array equivalent to the <c>values</c>. </param>
        /// <returns><c>true</c> if <c>values</c> was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(IList<float> values, out Color[] result)
        {
            if (values is not { Count: > 0 })
            {
                result = default;
                return false;
            }

            if (values.Count % 4 != 0)
            {
                result = default;
                return false;
            }

            result = ParseArray(values);
            return true;
        }
    }
}