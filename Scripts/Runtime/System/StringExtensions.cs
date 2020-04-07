// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using UnityEngine;

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods

        /// <summary>
        /// Returns <see cref="Quaternion"/> for this formatted string (format like this: (1.0, 1.0,
        /// 1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Quaternion"/>.</param>
        /// <returns>The <see cref="Quaternion"/> converted from the formatted <see cref="string"/>.</returns>
        public static Quaternion ToQuaternion(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                string[] valuesArr = GetValuesArray(source);

                if (valuesArr != null && valuesArr.Length == 4)
                {
                    float x = 0;
                    float y = 0;
                    float z = 0;
                    float w = 0;
                    float.TryParse(valuesArr[0], out x);
                    float.TryParse(valuesArr[1], out y);
                    float.TryParse(valuesArr[2], out z);
                    float.TryParse(valuesArr[3], out w);
                    return new Quaternion(x, y, z, w);
                }
            }

            return Quaternion.identity;
        }

        /// <summary>
        /// Returns <see cref="Vector2"/> for this formatted string (format like this: (1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector2"/>.</param>
        /// <returns>The <see cref="Vector2"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector2 ToVector2(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                string[] valuesArr = GetValuesArray(source);

                if (valuesArr != null && valuesArr.Length == 2)
                {
                    float x = 0;
                    float y = 0;
                    float.TryParse(valuesArr[0], out x);
                    float.TryParse(valuesArr[1], out y);
                    return new Vector2(x, y);
                }
            }

            return Vector2.zero;
        }

        /// <summary>
        /// Returns <see cref="Vector3"/> for this formatted string (format like this: (1.0, 1.0, 1.0)).
        /// </summary>
        /// <param name="source">The <see cref="string"/> to convert to <see cref="Vector3"/>.</param>
        /// <returns>The <see cref="Vector3"/> converted from the formatted <see cref="string"/>.</returns>
        public static Vector3 ToVector3(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                string[] valuesArr = GetValuesArray(source);

                if (valuesArr != null && valuesArr.Length == 3)
                {
                    float x = 0;
                    float y = 0;
                    float z = 0;
                    float.TryParse(valuesArr[0], out x);
                    float.TryParse(valuesArr[1], out y);
                    float.TryParse(valuesArr[2], out z);
                    return new Vector3(x, y, z);
                }
            }

            return Vector3.zero;
        }

        /// <summary>
        /// Gets the values array of <see cref="string"/>.
        /// </summary>
        /// <param name="strValue">The value of <see cref="string"/>.</param>
        /// <returns>The array of values.</returns>
        private static string[] GetValuesArray(string strValue)
        {
            if (strValue.IndexOf('(') == 0 && strValue.LastIndexOf(')') == strValue.Length - 1)
            {
                string realValuesStr = strValue.Trim('(', ')');
                return realValuesStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return null;
        }

        #endregion Methods
    }
}