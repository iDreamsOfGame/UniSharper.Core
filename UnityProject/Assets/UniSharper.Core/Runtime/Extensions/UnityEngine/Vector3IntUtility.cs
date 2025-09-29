// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace UniSharper.Extensions
{
    public static class Vector3IntUtility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Vector3Int"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector3Int"/> equivalent. </param>
        /// <returns>A <see cref="Vector3Int"/> equivalent to the <c>s</c>. </returns>
        public static Vector3Int Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;

            var values = StringUtility.GetStringValuesInBrackets(s);
            if (values.Length < 2)
                return default;
            
            if (values.Length > 2)
            {
                int.TryParse(values[0], out var x);
                int.TryParse(values[1], out var y);
                int.TryParse(values[2], out var z);
                return new Vector3Int(x, y, z);
            }
            else
            {
                int.TryParse(values[0], out var x);
                int.TryParse(values[1], out var y);
                return new Vector3Int(x, y, 0);
            }
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Vector3Int"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector3Int"/> equivalent. </param>
        /// <param name="result">A <see cref="Vector3Int"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Vector3Int result)
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

            if (values.Length > 2)
            {
                int.TryParse(values[0], out var x);
                int.TryParse(values[1], out var y);
                int.TryParse(values[2], out var z);
                result = new Vector3Int(x, y, z);
            }
            else
            {
                int.TryParse(values[0], out var x);
                int.TryParse(values[1], out var y);
                result = new Vector3Int(x, y, 0);
            }

            return true;
        }
        
        /// <summary>
        /// Converts the string representation of an <see cref="Vector3Int"/> array equivalent.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Vector3Int"/> array equivalent. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns>An <see cref="Vector3Int"/> array equivalent to the <c>s</c>. </returns>
        public static Vector3Int[] ParseArray(string s, string arrayElementSeparator = "|")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(arrayElementSeparator))
                return default;
            
            var elementStrings = s.Trim().Split(arrayElementSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (elementStrings.Length == 0)
                return default;

            var result = new Vector3Int[elementStrings.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Parse(elementStrings[i]);
            }

            return result;
        }
        
        /// <summary>
        /// Converts the collection of <see cref="int"/> representation of an <see cref="Vector3Int"/> array equivalent.
        /// </summary>
        /// <param name="values">The collection of <see cref="int"/> representation of an <see cref="Vector3Int"/> array equivalent.</param>
        /// <returns>An <see cref="Vector3Int"/> array equivalent to the <c>values</c>. </returns>
        public static Vector3Int[] ParseArray(IList<int> values)
        {
            var list = new List<Vector3Int>();
            for (var i = 0; i < values.Count; i += 3)
            {
                var x = values[i];
                var y = values[i + 1];
                var z = values[i + 2];
                list.Add(new Vector3Int(x, y, z));
            }
            return list.ToArray();
        }
        
        /// <summary>
        /// Converts the string representation of an <see cref="Vector3Int"/> array equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Vector3Int"/> array equivalent. </param>
        /// <param name="result">An <see cref="Vector3Int"/> array equivalent to the <c>s</c>. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(string s, out Vector3Int[] result, string arrayElementSeparator = "|")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(arrayElementSeparator))
            {
                result = default;
                return false;
            }

            var elementStrings = s.Trim().Split(arrayElementSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (elementStrings.Length == 0)
            {
                result = default;
                return false;
            }

            var list = new List<Vector3Int>(elementStrings.Length);
            foreach (var elementString in elementStrings)
            {
                if(TryParse(elementString, out var element))
                    list.Add(element);
            }
            
            result = list.ToArray();
            return true;
        }
        
        /// <summary>
        /// Converts the collection of <see cref="int"/> representation of an <see cref="Vector3Int"/> array equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="values">The collection of <see cref="int"/> representation of an <see cref="Vector3Int"/> array equivalent. </param>
        /// <param name="result">An <see cref="Vector3Int"/> array equivalent to the <c>values</c>. </param>
        /// <returns><c>true</c> if <c>values</c> was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(IList<int> values, out Vector3Int[] result)
        {
            if (values == null || values.Count == 0)
            {
                result = default;
                return false;
            }

            if (values.Count % 3 != 0)
            {
                result = default;
                return false;
            }

            result = ParseArray(values);
            return true;
        }
    }
}