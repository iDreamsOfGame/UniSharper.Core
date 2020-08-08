// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UnityEngine
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Mathf"/> utilities.
    /// </summary>
    public static class MathfUtility
    {
        #region Methods

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

            if (angle > 180)
            {
                angle -= 360;
            }
            else if (angle < -180)
            {
                angle += 360;
            }

            min = NormalizeAngle(min);

            if (min > 180)
            {
                min -= 360;
            }
            else if (min < -180)
            {
                min += 360;
            }

            max = NormalizeAngle(max);

            if (max > 180)
            {
                max -= 360;
            }
            else if (max < -180)
            {
                max += 360;
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

        #endregion Methods
    }
}