// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Quaternion"/>.
    /// </summary>
    public static class QuaternionExtensions
    {
        #region Methods

        /// <summary>
        /// Converts the <see cref="Quaternion"/> to the accurate <see cref="string"/>.
        /// </summary>
        /// <param name="source">The <see cref="Quaternion"/>.</param>
        /// <returns>The accurate <see cref="string"/> of the <see cref="Quaternion"/>.</returns>
        public static string ToAccurateString(this Quaternion source)
        {
            return string.Format("({0}, {1}, {2}, {3})", source.x, source.y, source.z, source.w);
        }

        #endregion Methods
    }
}