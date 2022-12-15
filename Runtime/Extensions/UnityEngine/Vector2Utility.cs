// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable PossibleMultipleEnumeration

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Vector2"/> utilities.
    /// </summary>
    public static class Vector2Utility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Vector2"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector2"/> equivalent. </param>
        /// <returns>A <see cref="Vector2"/> equivalent to the <c>s</c>. </returns>
        public static Vector2 Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;

            var values = StringUtility.GetStringValuesInBrackets(s);
            if (values.Length < 2)
                return default;

            float.TryParse(values[0], out var x);
            float.TryParse(values[1], out var y);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Vector2"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Vector2"/> equivalent. </param>
        /// <param name="result">A <see cref="Vector2"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Vector2 result)
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

            float.TryParse(values[0], out var x);
            float.TryParse(values[1], out var y);
            result = new Vector2(x, y);
            return true;
        }
        
        /// <summary>
        /// Calculates the average of multiple <see cref="Vector2"/> s.
        /// </summary>
        /// <param name="points">The source <see cref="Vector2"/> s.</param>
        /// <returns>The average of multiple <see cref="Vector2"/> s.</returns>
        public static Vector2 CalculateAverage(IEnumerable<Vector2> points)
        {
            var sum = points.Aggregate(Vector2.zero, (current, point) => current + point);
            return sum / points.Count();
        }
        
        /// <summary>
        /// Calculates the variance of multiple <see cref="Vector2"/> s.
        /// </summary>
        /// <param name="points">The source <see cref="Vector2"/> s.</param>
        /// <returns>The variance of multiple <see cref="Vector2"/> s.</returns>
        public static Vector2 CalculateVariance(IEnumerable<Vector2> points)
        {
            var squareSum = Vector2.zero;
            var average = CalculateAverage(points);
            foreach (var point in points)
            {
                var x = Mathf.Pow(point.x - average.x, 2);
                var y = Mathf.Pow(point.y - average.y, 2);
                squareSum += new Vector2(x, y);
            }

            var result = squareSum / points.Count();
            result.x = Mathf.Sqrt(result.x);
            result.y = Mathf.Sqrt(result.y);
            return result;
        }

        /// <summary>
        /// Rotate a vector by a angle value.
        /// </summary>
        /// <param name="from">Which vector start from.</param>
        /// <param name="angle">The angle value to rotate.</param>
        /// <returns>The target vector through rotating a start vector.</returns>
        public static Vector2 Rotate(Vector2 from, float angle) => Quaternion.AngleAxis(angle, Vector3.forward) * from;

        /// <summary>
        /// Calculates quadratic bezier point.
        /// </summary>
        /// <param name="t">The time. </param>
        /// <param name="p0">The point one. </param>
        /// <param name="p1">The point two. </param>
        /// <param name="p2">The point three. </param>
        /// <returns>The quadratic bezier point. </returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>t</c> is out of range. </exception>
        public static Vector2 CalculateQuadraticBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
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
        public static Vector2 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            if (t is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(t));

            var f = 1 - t;
            return Mathf.Pow(f, 3) * p0 + 3 * t * Mathf.Pow(f, 2) * p1 + 3 * f * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
        }

        /// <summary>
        /// Generates quadratic bezier points.
        /// </summary>
        /// <param name="startPoint">The position of start point. </param>
        /// <param name="controlPoint">The position of control point. </param>
        /// <param name="endPoint">The position of end point. </param>
        /// <param name="segmentCount">The points count to be generated. </param>
        /// <returns>The <see cref="System.Array"/> of position that quadratic bezier points located. </returns>
        public static Vector2[] GenerateQuadraticBezierPoints(Vector2 startPoint, Vector2 controlPoint, Vector2 endPoint, int segmentCount)
        {
            var points = new Vector2[segmentCount];

            for (var i = 1; i <= segmentCount; i++)
            {
                var t = i / (float)segmentCount;
                var point = CalculateQuadraticBezierPoint(t, startPoint, controlPoint, endPoint);
                points[i - 1] = point;
            }

            return points;
        }

        /// <summary>
        /// Generates cubic bezier points.
        /// </summary>
        /// <param name="startPoint">The position of start point. </param>
        /// <param name="controlPoint1">The position of the first control point. </param>
        /// <param name="controlPoint2">The position of the second control point. </param>
        /// <param name="endPoint">The position of end point. </param>
        /// <param name="segmentCount">The points count to be generated. </param>
        /// <returns>The <see cref="System.Array"/> of position that cubic bezier points located. </returns>
        public static Vector2[] GenerateCubicBezierPoints(Vector2 startPoint, Vector2 controlPoint1, Vector2 controlPoint2, Vector2 endPoint, int segmentCount)
        {
            var points = new Vector2[segmentCount];

            for (var i = 1; i <= segmentCount; i++)
            {
                var t = i / (float)segmentCount;
                var point = CalculateCubicBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
                points[i - 1] = point;
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
        public static Vector2[] GenerateLogarithmicSpiralPoints(Vector2 centerPoint, float cycles, float a, float b, int pointsCount)
        {
            var points = new Vector2[pointsCount];
            var theta = 2f * Mathf.PI / pointsCount * cycles;

            for (var i = 0; i < points.Length; i++)
            {
                var radius = a * Mathf.Exp(b * theta);
                var x = radius * Mathf.Cos(theta) + centerPoint.x;
                var y = radius * Mathf.Sin(theta) + centerPoint.y;
                points[i] = new Vector2(x, y);
                theta += 2f * Mathf.PI / pointsCount * cycles;
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
        public static Vector2[] GenerateAchimedeanSpiralPoints(Vector2 centerPoint, float cycles, float a, float b, int pointsCount)
        {
            var points = new Vector2[pointsCount];
            var theta = 2f * Mathf.PI / pointsCount * cycles;

            for (var i = 0; i < points.Length; i++)
            {
                var radius = a + b * theta;
                var x = radius * Mathf.Cos(theta) + centerPoint.x;
                var y = radius * Mathf.Sin(theta) + centerPoint.y;
                points[i] = new Vector3(x, y);
                theta += 2f * Mathf.PI / pointsCount * cycles;
            }

            return points;
        }
    }
}