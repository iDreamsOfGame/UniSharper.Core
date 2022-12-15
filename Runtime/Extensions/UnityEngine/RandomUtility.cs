// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using Random = UnityEngine.Random;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Random"/> utilities.
    /// </summary>
    public static class RandomUtility
    {
        /// <summary>
        /// Gets the random seed.
        /// </summary>
        public static int RandomSeed => Guid.NewGuid().GetHashCode();
        
        /// <summary>
        /// Return a random int within [minInclusive..maxValue) or [minInclusive..maxValue] on random seed initialized.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. </param>
        /// <param name="includingMaxValue">If the rang of return values includes <c>maxValue</c>. </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to <c>minValue</c> and less than or equal to <c>maxValue</c>;
        /// If <c>minValue</c> equals <c>maxValue</c>, <c>minValue</c> is returned.
        /// </returns>
        public static int RangeWithRandomSeed(int minInclusive, int maxValue, bool includingMaxValue = true)
        {
            Random.InitState(Guid.NewGuid().GetHashCode());
            return includingMaxValue ? Random.Range(minInclusive, maxValue + 1) : Random.Range(minInclusive, maxValue);
        }
        
        /// <summary>
        /// Returns a random float within [minInclusive..maxInclusive] (range is inclusive) on random seed initialized.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxInclusive">The inclusive upper bound of the random number returned. </param>
        /// <returns>
        /// A single greater than or equal to <c>minValue</c> and less than or equal to <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> and <c>maxValue</c>.
        /// If <c>minValue</c> equals <c>maxValue</c>, <c>minValue</c> is returned.
        /// </returns>
        public static float RangeWithRandomSeed(float minInclusive, float maxInclusive)
        {
            Random.InitState(RandomSeed);
            return Random.Range(minInclusive, maxInclusive);
        }
    }
}