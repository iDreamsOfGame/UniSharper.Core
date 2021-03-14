// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Vector3"/>.
    /// </summary>
    public static class Vector3Extensions
    {
        #region Methods

        /// <summary>
        /// Converts the <see cref="Vector3"/> to the accurate <see cref="string"/>.
        /// </summary>
        /// <param name="source">The <see cref="Vector3"/>.</param>
        /// <returns>The accurate <see cref="string"/> of the <see cref="Vector3"/>.</returns>
        public static string ToAccurateString(this Vector3 source) => $"({source.x}, {source.y}, {source.z})";

        #endregion Methods
    }
}