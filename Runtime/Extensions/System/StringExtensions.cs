// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns <see cref="Color"/> for this formatted string (format like this: (1.0, 1.0, 1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Color"/>.</param>
        /// <returns>The <see cref="Color"/> converted from the formatted <see cref="string"/>.</returns>
        public static Color ToColor(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Color.clear;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 4 })
                return Color.clear;

            float.TryParse(valuesArr[0], out var r);
            float.TryParse(valuesArr[1], out var g);
            float.TryParse(valuesArr[2], out var b);
            float.TryParse(valuesArr[3], out var a);
            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Returns <see cref="Color32"/> for this formatted string (format like this: (255, 255, 255, 255)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Color32"/>.</param>
        /// <returns>The <see cref="Color32"/> converted from the formatted <see cref="string"/>.</returns>
        public static Color32 ToColor32(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 4 })
                return default;

            byte.TryParse(valuesArr[0], out var r);
            byte.TryParse(valuesArr[1], out var g);
            byte.TryParse(valuesArr[2], out var b);
            byte.TryParse(valuesArr[3], out var a);
            return new Color32(r, g, b, a);
        }
        
        /// <summary>
        /// Returns <see cref="Rect"/> for this formatted string (format like this: (0.0, 0.0, 100.0, 100.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Rect"/>.</param>
        /// <returns>The <see cref="Rect"/> converted from the formatted <see cref="string"/>.</returns>
        public static Rect ToRect(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Rect.zero;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 4 })
                return Rect.zero;

            float.TryParse(valuesArr[0], out var x);
            float.TryParse(valuesArr[1], out var y);
            float.TryParse(valuesArr[2], out var width);
            float.TryParse(valuesArr[3], out var height);
            return new Rect(x, y, width, height);
        }

        /// <summary>
        /// Returns <see cref="RectInt"/> for this formatted string (format like this: (0.0, 0.0, 100.0, 100.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="RectInt"/>.</param>
        /// <returns>The <see cref="RectInt"/> converted from the formatted <see cref="string"/>.</returns>
        public static RectInt ToRectInt(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 4 })
                return default;

            int.TryParse(valuesArr[0], out var xMin);
            int.TryParse(valuesArr[1], out var yMin);
            int.TryParse(valuesArr[2], out var width);
            int.TryParse(valuesArr[3], out var height);
            return new RectInt(xMin, yMin, width, height);
        }
        
        /// <summary>
        /// Returns <see cref="Quaternion"/> for this formatted string (format like this: (1.0, 1.0, 1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Quaternion"/>.</param>
        /// <returns>The <see cref="Quaternion"/> converted from the formatted <see cref="string"/>.</returns>
        public static Quaternion ToQuaternion(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Quaternion.identity;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 4 })
                return Quaternion.identity;

            float.TryParse(valuesArr[0], out var x);
            float.TryParse(valuesArr[1], out var y);
            float.TryParse(valuesArr[2], out var z);
            float.TryParse(valuesArr[3], out var w);
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

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 2 })
                return default;

            int.TryParse(valuesArr[0], out var start);
            int.TryParse(valuesArr[1], out var length);
            return new RangeInt(start, length);
        }

        /// <summary>
        /// Returns <see cref="Vector4"/> for this formatted string (format like this: (1.0, 1.0, 1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector4"/>.</param>
        /// <returns>The <see cref="Vector4"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector4 ToVector4(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector4.zero;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 4 })
                return Vector4.zero;

            float.TryParse(valuesArr[0], out var x);
            float.TryParse(valuesArr[1], out var y);
            float.TryParse(valuesArr[2], out var z);
            float.TryParse(valuesArr[3], out var w);
            return new Vector4(x, y, z, w);
        }

        /// <summary>
        /// Returns <see cref="Vector2"/> for this formatted string (format like this: (1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector2"/>.</param>
        /// <returns>The <see cref="Vector2"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector2 ToVector2(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector2.zero;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 2 })
                return Vector2.zero;

            float.TryParse(valuesArr[0], out var x);
            float.TryParse(valuesArr[1], out var y);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Returns <see cref="Vector2Int"/> for this formatted string (format like this: (1, 1)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector2Int"/>.</param>
        /// <returns>The <see cref="Vector2Int"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector2Int ToVector2Int(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector2Int.zero;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 2 })
                return Vector2Int.zero;

            int.TryParse(valuesArr[0], out var x);
            int.TryParse(valuesArr[1], out var y);
            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Returns <see cref="Vector3"/> for this formatted string (format like this: (1.0, 1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="Vector3"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector3 ToVector3(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector3.zero;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 3 })
                return Vector3.zero;

            float.TryParse(valuesArr[0], out var x);
            float.TryParse(valuesArr[1], out var y);
            float.TryParse(valuesArr[2], out var z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns <see cref="Vector3Int"/> for this formatted string (format like this: (1, 1, 1)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector3Int"/>.</param>
        /// <returns>The <see cref="Vector3Int"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector3Int ToVector3Int(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Vector3Int.zero;

            var valuesArr = GetValuesArray(source);
            if (valuesArr is not { Length: 3 })
                return Vector3Int.zero;

            int.TryParse(valuesArr[0], out var x);
            int.TryParse(valuesArr[1], out var y);
            int.TryParse(valuesArr[2], out var z);
            return new Vector3Int(x, y, z);
        }

        /// <summary>
        /// Gets the values array of <see cref="string"/>.
        /// </summary>
        /// <param name="strValue">The value of <see cref="string"/>.</param>
        /// <returns>The array of values.</returns>
        private static string[] GetValuesArray(string strValue)
        {
            if (strValue.IndexOf('(') != 0 || strValue.LastIndexOf(')') != strValue.Length - 1)
                return null;

            var realValuesStr = strValue.Trim('(', ')');
            return realValuesStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}