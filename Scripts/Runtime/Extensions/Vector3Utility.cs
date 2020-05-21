using System;

namespace UnityEngine
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Vector3"/> utilities.
    /// </summary>
    public static class Vector3Utility
    {
        private static Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            if(t < 0 || t > 1)
                throw new ArgumentOutOfRangeException(nameof(t));

            var f = 1 - t;
            return Mathf.Pow(f, 2) * p0 + 2 * t * f * p1 + Mathf.Pow(t, 2) * p2;
        }
        
        private static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            if(t < 0 || t > 1)
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
        /// Generates cubic bezier points.
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
                points[i] = new Vector3(x, y, 0f);
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
        public static Vector3[] GenerateLogarithmicSpiralPoints(
            Vector3 centerPoint,
            float cycles,
            float a,
            float b,
            int pointsCount)
        {
            var points = new Vector3[pointsCount];
            var theta = 2f * Mathf.PI / pointsCount * cycles;

            for (var i = 0; i < points.Length; i++)
            {
                var radius= a * Mathf.Exp(b * theta);
                var x = radius * Mathf.Cos(theta) + centerPoint.x;
                var y = radius * Mathf.Sin(theta) + centerPoint.y;
                points[i] = new Vector3(x, y, 0f);
                theta += 2f * Mathf.PI / pointsCount * cycles;
            }

            
            
            return points;
        }
    }
}