// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Vector2"/>.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Determines if the <see cref="Vector2"/> represents an valid value.
        /// </summary>
        /// <param name="source">The <see cref="Vector2"/>.</param>
        /// <returns><c>true</c> if the <see cref="Vector2"/> is an valid value; otherwise, false. </returns>
        public static bool IsValid(this Vector2 source)
        {
            if (float.IsNaN(source.x) || float.IsNaN(source.y))
                return false;
            
            if (float.IsInfinity(source.x) || float.IsInfinity(source.y))
                return false;

            return true;
        }
        
        /// <summary>
        /// Converts the <see cref="Vector2"/> to the accurate <see cref="string"/>.
        /// </summary>
        /// <param name="source">The <see cref="Vector2"/>.</param>
        /// <returns>The accurate <see cref="string"/> of the <see cref="Vector2"/>.</returns>
        public static string ToAccurateString(this Vector2 source) => $"({source.x}, {source.y})";
    }
}