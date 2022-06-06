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
        /// Returns <see cref="Quaternion"/> for this formatted string (format like this: (1.0, 1.0,
        /// 1.0, 1.0)).
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