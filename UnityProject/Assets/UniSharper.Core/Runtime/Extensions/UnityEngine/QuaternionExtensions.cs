// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Quaternion"/>.
    /// </summary>
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Converts the <see cref="Quaternion"/> to the accurate <see cref="string"/>.
        /// </summary>
        /// <param name="source">The <see cref="Quaternion"/>.</param>
        /// <returns>The accurate <see cref="string"/> of the <see cref="Quaternion"/>.</returns>
        public static string ToAccurateString(this Quaternion source) => $"({source.x}, {source.y}, {source.z}, {source.w})";
    }
}