// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
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
        
        /// <summary>
        /// Converts the string representation of an <see cref="Vector2Int"/> array equivalent.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Vector2Int"/> array equivalent. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns>An <see cref="Vector2Int"/> array equivalent to the <c>s</c>. </returns>
        public static Vector2Int[] ParseArray(string s, string arrayElementSeparator = "|")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(arrayElementSeparator))
                return default;
            
            var elementStrings = s.Trim().Split(arrayElementSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (elementStrings.Length == 0)
                return default;

            var result = new Vector2Int[elementStrings.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Parse(elementStrings[i]);
            }

            return result;
        }

        /// <summary>
        /// Converts the collection of <see cref="int"/> representation of an <see cref="Vector2Int"/> array equivalent.
        /// </summary>
        /// <param name="values">The collection of <see cref="int"/> representation of an <see cref="Vector2Int"/> array equivalent.</param>
        /// <returns>An <see cref="Vector2Int"/> array equivalent to the <c>values</c>. </returns>
        public static Vector2Int[] ParseArray(IList<int> values)
        {
            var list = new List<Vector2Int>();
            for (var i = 0; i < values.Count; i += 2)
            {
                var x = values[i];
                var y = values[i + 1];
                list.Add(new Vector2Int(x, y));
            }
            return list.ToArray();
        }
        
        /// <summary>
        /// Converts the string representation of an <see cref="Vector2Int"/> array equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Vector2Int"/> array equivalent. </param>
        /// <param name="result">An <see cref="Vector2Int"/> array equivalent to the <c>s</c>. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(string s, out Vector2Int[] result, string arrayElementSeparator = "|")
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

            var list = new List<Vector2Int>(elementStrings.Length);
            foreach (var elementString in elementStrings)
            {
                if(TryParse(elementString, out var element))
                    list.Add(element);
            }
            
            result = list.ToArray();
            return true;
        }

        /// <summary>
        /// Converts the collection of <see cref="int"/> representation of an <see cref="Vector2Int"/> array equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="values">The collection of <see cref="int"/> representation of an <see cref="Vector2Int"/> array equivalent. </param>
        /// <param name="result">An <see cref="Vector2Int"/> array equivalent to the <c>values</c>. </param>
        /// <returns><c>true</c> if <c>values</c> was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(IList<int> values, out Vector2Int[] result)
        {
            if (values is not { Count: > 0 })
            {
                result = default;
                return false;
            }

            if (values.Count % 2 != 0)
            {
                result = default;
                return false;
            }

            result = ParseArray(values);
            return true;
        }
    }
}