// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
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
        /// Return a random int within [minInclusive, maxValue) or [minInclusive, maxValue] on random seed initialized.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. </param>
        /// <param name="includingMaxValue">If the rang of return values includes <c>maxValue</c>. </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to <c>minValue</c> and less than or equal to <c>maxValue</c>;
        /// If <c>minValue</c> equals <c>maxValue</c>, <c>minValue</c> is returned.
        /// </returns>
        public static int RangeWithRandomSeed(int minInclusive, int maxValue, bool includingMaxValue = true) => 
            RangeWithSeed(RandomSeed, minInclusive, maxValue, includingMaxValue);

        /// <summary>
        /// Return a random int within [minInclusive, maxValue) or [minInclusive, maxValue] with specified seed.
        /// </summary>
        /// <param name="seed">The seed used to initialize the random state.</param>
        /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned.</param>
        /// <param name="includingMaxValue">If the range of return values includes <c>maxValue</c>.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to <c>minInclusive</c> and less than or equal to <c>maxValue</c>;
        /// If <c>minInclusive</c> equals <c>maxValue</c>, <c>minInclusive</c> is returned.
        /// </returns>
        public static int RangeWithSeed(int seed,
            int minInclusive,
            int maxValue,
            bool includingMaxValue = true)
        {
            Random.InitState(seed);
            return includingMaxValue ? Random.Range(minInclusive, maxValue + 1) : Random.Range(minInclusive, maxValue);
        }
        
        /// <summary>
        /// Returns a random float within [minInclusive, maxInclusive] (range is inclusive) on random seed initialized.
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
            return RangeWithSeed(RandomSeed, minInclusive, maxInclusive);
        }

        /// <summary>
        /// Returns a random float within [minInclusive, maxInclusive] (range is inclusive) with specified seed.
        /// </summary>
        /// <param name="seed">The seed used to initialize the random state.</param>
        /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxInclusive">The inclusive upper bound of the random number returned.</param>
        /// <returns>
        /// A single-precision floating-point number greater than or equal to <c>minInclusive</c> and less than or equal to <c>maxInclusive</c>;
        /// that is, the range of return values includes both <c>minInclusive</c> and <c>maxInclusive</c>.
        /// If <c>minInclusive</c> equals <c>maxInclusive</c>, <c>minInclusive</c> is returned.
        /// </returns>
        public static float RangeWithSeed(int seed, float minInclusive, float maxInclusive)
        {
            Random.InitState(seed);
            return Random.Range(minInclusive, maxInclusive);
        }
        
        /// <summary>
        /// Gets unique random integer values within the specified range using a random seed.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random values.</param>
        /// <param name="maxValue">The inclusive upper bound of the random values.</param>
        /// <param name="count">The number of unique random values to generate.</param>
        /// <returns>An array of unique random integers within the specified range.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <c>count</c> is greater than the total number of available values in the range.</exception>
        public static int[] GetUniqueRandomValuesWithRandomSeed(int minValue, 
            int maxValue,
            int count)
        {
            return GetUniqueRandomValuesWithSeed(RandomSeed, minValue, maxValue, count);
        }

        /// <summary>
        /// Gets unique random integer values within the specified range using a specified seed.
        /// </summary>
        /// <param name="seed">The seed used to initialize the random state.</param>
        /// <param name="minValue">The inclusive lower bound of the random values.</param>
        /// <param name="maxValue">The inclusive upper bound of the random values.</param>
        /// <param name="count">The number of unique random values to generate.</param>
        /// <returns>An array of unique random integers within the specified range.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <c>count</c> is greater than the total number of available values in the range.</exception>
        public static int[] GetUniqueRandomValuesWithSeed(int seed, 
            int minValue,
            int maxValue,
            int count)
        {
            if (count > maxValue - minValue + 1)
                throw new InvalidOperationException();
            
            var allValues = new List<int>();
            for (var i = minValue; i <= maxValue; i++)
            {
                allValues.Add(i);
            }
            
            for (var i = allValues.Count - 1; i > 0; i--)
            {
                var randomIndex = RangeWithSeed(seed, 0, i);
                (allValues[i], allValues[randomIndex]) = (allValues[randomIndex], allValues[i]);
            }
            
            return allValues.GetRange(0, count).ToArray();
        }
    }
}