// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Vector3"/> utilities.
    /// </summary>
    public static class Vector3Utility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Vector3"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector3"/> equivalent. </param>
        /// <returns>A <see cref="Vector3"/> equivalent to the <c>s</c>. </returns>
        public static Vector3 Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;

            var values = StringUtility.GetStringValuesInBrackets(s);
            switch (values.Length)
            {
                case < 2:
                    return default;
                case > 2:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    float.TryParse(values[2], out var z);
                    return new Vector3(x, y, z);
                }
                default:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    return new Vector3(x, y);
                }
            }
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Vector3"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector3"/> equivalent. </param>
        /// <param name="result">A <see cref="Vector3"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Vector3 result)
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
                case > 2:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    float.TryParse(values[2], out var z);
                    result = new Vector3(x, y, z);
                    break;
                }
                default:
                {
                    float.TryParse(values[0], out var x);
                    float.TryParse(values[1], out var y);
                    result = new Vector3(x, y);
                    break;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Converts the string representation of an <see cref="Vector3"/> array equivalent.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Vector3"/> array equivalent. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns>An <see cref="Vector3"/> array equivalent to the <c>s</c>. </returns>
        public static Vector3[] ParseArray(string s, string arrayElementSeparator = "|")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(arrayElementSeparator))
                return default;
            
            var elementStrings = s.Trim().Split(arrayElementSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (elementStrings.Length == 0)
                return default;

            var result = new Vector3[elementStrings.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Parse(elementStrings[i]);
            }

            return result;
        }
        
        /// <summary>
        /// Converts the collection of <see cref="float"/> representation of an <see cref="Vector3"/> array equivalent.
        /// </summary>
        /// <param name="values">The collection of <see cref="float"/> representation of an <see cref="Vector3"/> array equivalent.</param>
        /// <returns>An <see cref="Vector3"/> array equivalent to the <c>values</c>. </returns>
        public static Vector3[] ParseArray(IList<float> values)
        {
            var list = new List<Vector3>();
            for (var i = 0; i < values.Count; i += 3)
            {
                var x = values[i];
                var y = values[i + 1];
                var z = values[i + 2];
                list.Add(new Vector3(x, y, z));
            }

            return list.ToArray();
        }

        /// <summary>
        /// Converts the string representation of an <see cref="Vector3"/> array equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of an <see cref="Vector3"/> array equivalent. </param>
        /// <param name="result">An <see cref="Vector3"/> array equivalent to the <c>s</c>. </param>
        /// <param name="arrayElementSeparator">A string that delimits the element string in this string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(string s, out Vector3[] result, string arrayElementSeparator = "|")
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

            var list = new List<Vector3>(elementStrings.Length);
            foreach (var elementString in elementStrings)
            {
                if(TryParse(elementString, out var element))
                    list.Add(element);
            }
            
            result = list.ToArray();
            return true;
        }
        
        /// <summary>
        /// Converts the collection of <see cref="float"/> representation of an <see cref="Vector3"/> array equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="values">The collection of <see cref="float"/> representation of an <see cref="Vector3"/> array equivalent. </param>
        /// <param name="result">An <see cref="Vector3"/> array equivalent to the <c>values</c>. </param>
        /// <returns><c>true</c> if <c>values</c> was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseArray(IList<float> values, out Vector3[] result)
        {
            if (values is not { Count: > 0 })
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
        
        /// <summary>
        /// Calculates quadratic bezier point.
        /// </summary>
        /// <param name="t">The time. </param>
        /// <param name="p0">The point one. </param>
        /// <param name="p1">The point two. </param>
        /// <param name="p2">The point three. </param>
        /// <returns>The quadratic bezier point. </returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>t</c> is out of range. </exception>
        public static Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            if (t is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(t));

            var f = 1 - t;
            return Mathf.Pow(f, 2) * p0 + 2 * t * f * p1 + Mathf.Pow(t, 2) * p2;
        }
        
        /// <summary>
        /// Calculates cubic bezier point.
        /// </summary>
        /// <param name="t">The time. </param>
        /// <param name="p0">The point one. </param>
        /// <param name="p1">The point two. </param>
        /// <param name="p2">The point three. </param>
        /// <param name="p3">The point four. </param>
        /// <returns>The cubic bezier point. </returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>t</c> is out of range. </exception>
        public static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            if (t is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(t));

            var f = 1 - t;
            return Mathf.Pow(f, 3) * p0 + 3 * t * Mathf.Pow(f, 2) * p1 + 3 * f * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
        }

        /// <summary>
        /// Generates quadratic Bezier points.
        /// </summary>
        /// <param name="startPoint">The position of start point. </param>
        /// <param name="controlPoint">The position of control point. </param>
        /// <param name="endPoint">The position of end point. </param>
        /// <param name="segmentCount">The points count to be generated. </param>
        /// <returns>The <see cref="System.Array"/> of position that quadratic bezier points located. </returns>
        public static Vector3[] GenerateQuadraticBezierPoints(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentCount)
        {
            var points = new Vector3[segmentCount];

            for (var i = 1; i <= segmentCount; i++)
            {
                var t = i / (float)segmentCount;
                var point = CalculateQuadraticBezierPoint(t, startPoint, controlPoint, endPoint);
                points[i - 1] = point;
            }

            return points;
        }
        
        /// <summary>
        /// Generates cubic Bezier points.
        /// </summary>
        /// <param name="startPoint">The position of start point. </param>
        /// <param name="controlPoint1">The position of the first control point. </param>
        /// <param name="controlPoint2">The position of the second control point. </param>
        /// <param name="endPoint">The position of end point. </param>
        /// <param name="segmentCount">The points count to be generated. </param>
        /// <returns>The <see cref="System.Array"/> of position that cubic bezier points located. </returns>
        public static Vector3[] GenerateCubicBezierPoints(Vector3 startPoint, Vector3 controlPoint1, Vector3 controlPoint2, Vector3 endPoint, int segmentCount)
        {
            var points = new Vector3[segmentCount];

            for (var i = 1; i <= segmentCount; i++)
            {
                var t = i / (float)segmentCount;
                var point = CalculateCubicBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
                points[i - 1] = point;
            }

            return points;
        }
        
        /// <summary>
        /// Generates points of Achimedean Spiral.
        /// </summary>
        /// <param name="centerPoint">The position of center point. </param>
        /// <param name="cycles">The cycles of spiral drawing. </param>
        /// <param name="a">The formula parameter a. This parameter will effect the shape of spiral. </param>
        /// <param name="b">The formula parameter b. This parameter control the space between spiral lines. </param>
        /// <param name="pointsCount">The count of points need to be generated. </param>
        /// <returns>The <see cref="System.Array"/> of position that Achimedean Spiral located. </returns>
        public static Vector3[] GenerateAchimedeanSpiralPoints(Vector3 centerPoint, float cycles, float a, float b, int pointsCount)
        {
            var points = new Vector3[pointsCount];
            var theta = 2f * Mathf.PI / pointsCount * cycles;

            for (var i = 0; i < points.Length; i++)
            {
                var radius = a + b * theta;
                var x = radius * Mathf.Cos(theta) + centerPoint.x;
                var y = radius * Mathf.Sin(theta) + centerPoint.y;
                points[i] = new Vector3(x, y, centerPoint.z);
                theta += 2f * Mathf.PI / pointsCount * cycles;
            }

            return points;
        }

        /// <summary>
        /// Generates points of Logarithmic Spiral.
        /// </summary>
        /// <param name="centerPoint">The position of center point. </param>
        /// <param name="cycles">The cycles of spiral drawing. </param>
        /// <param name="a">The formula parameter a. This parameter will effect the shape of spiral. </param>
        /// <param name="b">The formula parameter b. This parameter control the space between spiral lines. </param>
        /// <param name="pointsCount">The count of points need to be generated. </param>
        /// <returns>The <see cref="System.Array"/> of position that Logarithmic Spiral located. </returns>
        public static Vector3[] GenerateLogarithmicSpiralPoints(Vector3 centerPoint, float cycles, float a, float b, int pointsCount)
        {
            var points = new Vector3[pointsCount];
            var theta = 2f * Mathf.PI / pointsCount * cycles;

            for (var i = 0; i < points.Length; i++)
            {
                var radius = a * Mathf.Exp(b * theta);
                var x = radius * Mathf.Cos(theta) + centerPoint.x;
                var y = radius * Mathf.Sin(theta) + centerPoint.y;
                points[i] = new Vector3(x, y, centerPoint.z);
                theta += 2f * Mathf.PI / pointsCount * cycles;
            }

            return points;
        }

        /// <summary>
        /// Gets the angle between <see cref="Vector3"/> a and b when the z value of a and b are zero.
        /// </summary>
        /// <param name="a">The <see cref="Vector3"/> a.</param>
        /// <param name="b">The <see cref="Vector3"/> b.</param>
        /// <returns>The angle between <see cref="Vector3"/> a and b.</returns>
        public static float Get2DAngle(Vector3 a, Vector3 b)
        {
            var isRight = Vector3.Cross(a, b).z < 0;
            var angle = Vector3.Angle(a, b);

            if (isRight)
                angle = -angle;

            return angle;
        }

        /// <summary>
        /// Gets the angle between <see cref="Vector3"/> a and b.
        /// </summary>
        /// <param name="a">The <see cref="Vector3"/> a.</param>
        /// <param name="b">The <see cref="Vector3"/> b.</param>
        /// <returns>The angle between <see cref="Vector3"/> a and b.</returns>
        public static float GetAngle(Vector3 a, Vector3 b) => Mathf.Acos(Vector3.Dot(a.normalized, b.normalized)) * Mathf.Rad2Deg;
    }
}