// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using ReSharp.Extensions;
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
            SingleUtility.GenericTryParse(xString, out var x);
            SingleUtility.GenericTryParse(yString, out var y);
            SingleUtility.GenericTryParse(widthString, out var width);
            SingleUtility.GenericTryParse(heightString, out var height);
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
            SingleUtility.GenericTryParse(xString, out var x);
            SingleUtility.GenericTryParse(yString, out var y);
            SingleUtility.GenericTryParse(widthString, out var width);
            SingleUtility.GenericTryParse(heightString, out var height);
            result = new Rect(x, y, width, height);
            return true;
        }
        
        /// <summary>
        /// Converts the string representation of an <see cref="Rect"/> array equivalent.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Rect"/> array equivalent. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns>An <see cref="Rect"/> array equivalent to the <c>s</c>. </returns>
        public static Rect[] ParseArray(string s, string arrayElementSeparator = "|")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(arrayElementSeparator))
                return default;
            
            var elementStrings = s.Trim().Split(arrayElementSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (elementStrings.Length == 0)
                return default;

            var result = new Rect[elementStrings.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Parse(elementStrings[i]);
            }

            return result;
        }
        
        /// <summary>
        /// Converts the collection of <see cref="float"/> representation of an <see cref="Rect"/> array equivalent.
        /// </summary>
        /// <param name="values">The collection of <see cref="float"/> representation of an <see cref="Rect"/> array equivalent.</param>
        /// <returns>An <see cref="Rect"/> array equivalent to the <c>values</c>. </returns>
        public static Rect[] ParseArray(IList<float> values)
        {
            var list = new List<Rect>();
            for (var i = 0; i < values.Count; i += 4)
            {
                var x = values[i];
                var y = values[i + 1];
                var width = values[i + 2];
                var height = values[i + 3];
                list.Add(new Rect(x, y, width, height));
            }

            return list.ToArray();
        }

        /// <summary>
        /// Converts the string representation of an <see cref="Rect"/> array equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Rect"/> array equivalent. </param>
        /// <param name="result">An <see cref="Rect"/> array equivalent to the <c>s</c>. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(string s, out Rect[] result, string arrayElementSeparator = "|")
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

            var list = new List<Rect>(elementStrings.Length);
            foreach (var elementString in elementStrings)
            {
                if(TryParse(elementString, out var element))
                    list.Add(element);
            }
            
            result = list.ToArray();
            return true;
        }
        
        /// <summary>
        /// Converts the collection of <see cref="float"/> representation of an <see cref="Rect"/> array equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="values">The collection of <see cref="float"/> representation of an <see cref="Rect"/> array equivalent. </param>
        /// <param name="result">An <see cref="Rect"/> array equivalent to the <c>values</c>. </param>
        /// <returns><c>true</c> if <c>values</c> was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(IList<float> values, out Rect[] result)
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