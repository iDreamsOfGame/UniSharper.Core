// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns <see cref="Color"/> for this formatted string (format like this: RGBA(1.0, 1.0, 1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Color"/>.</param>
        /// <returns>The <see cref="Color"/> converted from the formatted <see cref="string"/>.</returns>
        public static Color ToColor(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Color.clear;

            var values = GetColorRgbaValues(source);
            if (values is not { Length: 4 })
                return Color.clear;

            float.TryParse(values[0], out var r);
            float.TryParse(values[1], out var g);
            float.TryParse(values[2], out var b);
            float.TryParse(values[3], out var a);
            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Returns <see cref="Color32"/> for this formatted string (format like this: RGBA(255, 255, 255, 255)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Color32"/>.</param>
        /// <returns>The <see cref="Color32"/> converted from the formatted <see cref="string"/>.</returns>
        public static Color32 ToColor32(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default;

            var values = GetColorRgbaValues(source);
            if (values is not { Length: 4 })
                return default;

            byte.TryParse(values[0], out var r);
            byte.TryParse(values[1], out var g);
            byte.TryParse(values[2], out var b);
            byte.TryParse(values[3], out var a);
            return new Color32(r, g, b, a);
        }
        
        /// <summary>
        /// Returns <see cref="Rect"/> for this formatted string (format like this: (x:0.00, y:0.00, width:0.00, height:0.00)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Rect"/>.</param>
        /// <returns>The <see cref="Rect"/> converted from the formatted <see cref="string"/>.</returns>
        public static Rect ToRect(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Rect.zero;

            var pairs = GetKeyValurPairs(source);
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
        /// Returns <see cref="RectInt"/> for this formatted string (format like this: (x:0, y:0, width:0, height:0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="RectInt"/>.</param>
        /// <returns>The <see cref="RectInt"/> converted from the formatted <see cref="string"/>.</returns>
        public static RectInt ToRectInt(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default;

            var pairs = GetKeyValurPairs(source);
            pairs.TryGetValue("x", out var xString);
            pairs.TryGetValue("y", out var yString);
            pairs.TryGetValue("width", out var widthString);
            pairs.TryGetValue("height", out var heightString);
            int.TryParse(xString, out var x);
            int.TryParse(yString, out var y);
            int.TryParse(widthString, out var width);
            int.TryParse(heightString, out var height);
            return new RectInt(x, y, width, height);
        }
        
        /// <summary>
        /// Returns <see cref="Quaternion"/> for this formatted string (format like this: (0.00000, 0.00000, 0.00000, 1.00000)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Quaternion"/>.</param>
        /// <returns>The <see cref="Quaternion"/> converted from the formatted <see cref="string"/>.</returns>
        public static Quaternion ToQuaternion(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Quaternion.identity;

            var values = GetStringValues(source);
            if (values is not { Length: 4 })
                return Quaternion.identity;

            float.TryParse(values[0], out var x);
            float.TryParse(values[1], out var y);
            float.TryParse(values[2], out var z);
            float.TryParse(values[3], out var w);
            return new Quaternion(x, y, z, w);
        }
        
        /// <summary>
        /// Returns <see cref="RangeInt"/> for this formatted string (format like this: (0, 10)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="RangeInt"/>.</param>
        /// <returns>The <see cref="RangeInt"/> converted from the formatted <see cref="string"/>.</returns>
        public static RangeInt ToRangeInt(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default;

            var values = GetStringValues(source);
            if (values is not { Length: 2 })
                return default;

            int.TryParse(values[0], out var start);
            int.TryParse(values[1], out var length);
            return new RangeInt(start, length);
        }

        /// <summary>
        /// Returns <see cref="Vector4"/> for this formatted string (format like this: (0.00, 0.00, 0.00, 0.00)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector4"/>.</param>
        /// <returns>The <see cref="Vector4"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector4 ToVector4(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector4.zero;

            var values = GetStringValues(source);
            if (values is not { Length: 4 })
                return Vector4.zero;

            float.TryParse(values[0], out var x);
            float.TryParse(values[1], out var y);
            float.TryParse(values[2], out var z);
            float.TryParse(values[3], out var w);
            return new Vector4(x, y, z, w);
        }
        
        /// <summary>
        /// Returns <see cref="Vector3"/> for this formatted string (format like this: (0.00, 0.00, 0.00)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="Vector3"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector3 ToVector3(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector3.zero;

            var values = GetStringValues(source);
            if (values is not { Length: 3 })
                return Vector3.zero;

            float.TryParse(values[0], out var x);
            float.TryParse(values[1], out var y);
            float.TryParse(values[2], out var z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns <see cref="Vector3Int"/> for this formatted string (format like this: (0, 0, 0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector3Int"/>.</param>
        /// <returns>The <see cref="Vector3Int"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector3Int ToVector3Int(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector3Int.zero;

            var valuesArray = GetStringValues(source);
            if (valuesArray is not { Length: 3 })
                return Vector3Int.zero;

            int.TryParse(valuesArray[0], out var x);
            int.TryParse(valuesArray[1], out var y);
            int.TryParse(valuesArray[2], out var z);
            return new Vector3Int(x, y, z);
        }

        /// <summary>
        /// Returns <see cref="Vector2"/> for this formatted string (format like this: (0.00, 0.00)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector2"/>.</param>
        /// <returns>The <see cref="Vector2"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector2 ToVector2(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector2.zero;

            var values = GetStringValues(source);
            if (values is not { Length: 2 })
                return Vector2.zero;

            float.TryParse(values[0], out var x);
            float.TryParse(values[1], out var y);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Returns <see cref="Vector2Int"/> for this formatted string (format like this: (0, 0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector2Int"/>.</param>
        /// <returns>The <see cref="Vector2Int"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector2Int ToVector2Int(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector2Int.zero;

            var values = GetStringValues(source);
            if (values is not { Length: 2 })
                return Vector2Int.zero;

            int.TryParse(values[0], out var x);
            int.TryParse(values[1], out var y);
            return new Vector2Int(x, y);
        }

        private static string[] GetColorRgbaValues(string value)
        {
            value = value.Trim();
            if (value.IndexOf("RGBA(", StringComparison.Ordinal) != 0 || value.LastIndexOf(')') != value.Length - 1)
                return null;

            value = value.Replace("RGBA(", "(");
            return GetStringValues(value);
        }

        private static Dictionary<string, string> GetKeyValurPairs(string value)
        {
            value = value.Trim();
            if (value.IndexOf('(') != 0 || value.LastIndexOf(')') != value.Length - 1)
                return null;
            
            value = value.Trim('(', ')');
            var pairs = value.Split(',');
            if (pairs.Length == 0)
                return null;

            var result = new Dictionary<string, string>();
            foreach (var pair in pairs)
            {
                var kv = pair.Split(':');
                if (kv.Length != 2) 
                    continue;
                
                var k = kv[0].Trim();
                var v = kv[1].Trim();
                result.Add(k, v);
            }

            return result;
        }

        private static string[] GetStringValues(string value)
        {
            value = value.Trim();
            if (value.IndexOf('(') != 0 || value.LastIndexOf(')') != value.Length - 1)
                return null;
            
            value = value.Trim('(', ')');
            return value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}