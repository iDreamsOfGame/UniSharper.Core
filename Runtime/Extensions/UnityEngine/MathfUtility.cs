// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Mathf"/> utilities.
    /// </summary>
    public static class MathfUtility
    {
        /// <summary>
        /// Clamps the angle between -180 and 180.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="min">The minimum angle.</param>
        /// <param name="max">The maximum angle.</param>
        /// <returns>The angle between -180 and 180.</returns>
        public static float ClampAngle(float angle, float min, float max)
        {
            angle = NormalizeAngle(angle);

            switch (angle)
            {
                case > 180:
                    angle -= 360;
                    break;
                case < -180:
                    angle += 360;
                    break;
            }

            min = NormalizeAngle(min);

            switch (min)
            {
                case > 180:
                    min -= 360;
                    break;
                case < -180:
                    min += 360;
                    break;
            }

            max = NormalizeAngle(max);

            switch (max)
            {
                case > 180:
                    max -= 360;
                    break;
                case < -180:
                    max += 360;
                    break;
            }

            return Mathf.Clamp(angle, min, max);
        }

        /// <summary>
        /// Normalizes the angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The normalized angle value.</returns>
        public static float NormalizeAngle(float angle)
        {
            while (angle > 360)
            {
                angle -= 360;
            }

            while (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }
    }
}