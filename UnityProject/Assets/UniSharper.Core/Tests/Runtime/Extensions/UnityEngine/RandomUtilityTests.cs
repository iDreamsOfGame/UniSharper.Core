using System;
using NUnit.Framework;
using UniSharper.Extensions;

namespace UniSharper.Tests
{
    internal class RandomUtilityTests
    {
        [Test]
        public void RandomSeed_ShouldReturnDifferentValues()
        {
            var seed1 = RandomUtility.RandomSeed;
            var seed2 = RandomUtility.RandomSeed;
            
            // Since it's based on Guid, they should be different
            Assert.AreNotEqual(seed1, seed2);
        }

        [Test]
        public void RangeWithRandomSeed_Int_ShouldReturnValidRange()
        {
            var result = RandomUtility.RangeWithRandomSeed(1, 10);
            
            Assert.GreaterOrEqual(result, 1);
            Assert.LessOrEqual(result, 10);
        }

        [Test]
        public void RangeWithRandomSeed_Int_ExcludingMaxValue_ShouldReturnValidRange()
        {
            var result = RandomUtility.RangeWithRandomSeed(1, 10, false);
            
            Assert.GreaterOrEqual(result, 1);
            Assert.Less(result, 10);
        }

        [Test]
        public void RangeWithSeed_Int_WithSameSeed_ShouldReturnSameValue()
        {
            const int seed = 12345;
            var result1 = RandomUtility.RangeWithSeed(seed, 1, 100);
            var result2 = RandomUtility.RangeWithSeed(seed, 1, 100);
            
            Assert.AreEqual(result1, result2);
        }

        [Test]
        public void RangeWithSeed_Int_IncludingMaxValue_ShouldIncludeMaxValue()
        {
            const int seed = 99999;
            var result = RandomUtility.RangeWithSeed(seed, 1, 10);
            
            Assert.GreaterOrEqual(result, 1);
            Assert.LessOrEqual(result, 10);
        }

        [Test]
        public void RangeWithSeed_Int_ExcludingMaxValue_ShouldExcludeMaxValue()
        {
            const int seed = 99999;
            var result = RandomUtility.RangeWithSeed(seed, 1, 10, false);
            
            Assert.GreaterOrEqual(result, 1);
            Assert.Less(result, 10);
        }

        [Test]
        public void RangeWithSeed_Int_MinEqualsMax_ShouldReturnMinValue()
        {
            const int seed = 12345;
            var result = RandomUtility.RangeWithSeed(seed, 5, 5);
            
            Assert.AreEqual(5, result);
        }

        [Test]
        public void RangeWithRandomSeed_Float_ShouldReturnValidRange()
        {
            var result = RandomUtility.RangeWithRandomSeed(1.0f, 10.0f);
            
            Assert.GreaterOrEqual(result, 1.0f);
            Assert.LessOrEqual(result, 10.0f);
        }

        [Test]
        public void RangeWithSeed_Float_WithSameSeed_ShouldReturnSameValue()
        {
            const int seed = 12345;
            var result1 = RandomUtility.RangeWithSeed(seed, 1.0f, 100.0f);
            var result2 = RandomUtility.RangeWithSeed(seed, 1.0f, 100.0f);
            
            Assert.AreEqual(result1, result2);
        }

        [Test]
        public void RangeWithSeed_Float_MinEqualsMax_ShouldReturnMinValue()
        {
            const int seed = 12345;
            var result = RandomUtility.RangeWithSeed(seed, 5.5f, 5.5f);
            
            Assert.AreEqual(5.5f, result);
        }

        [Test]
        public void GetUniqueRandomValuesWithRandomSeed_ShouldReturnCorrectCount()
        {
            var result = RandomUtility.GetUniqueRandomValuesWithRandomSeed(1, 100, 10);
            
            Assert.AreEqual(10, result.Length);
        }

        [Test]
        public void GetUniqueRandomValuesWithRandomSeed_ShouldReturnUniqueValues()
        {
            var result = RandomUtility.GetUniqueRandomValuesWithRandomSeed(1, 100, 10);
            
            var uniqueValues = new System.Collections.Generic.HashSet<int>(result);
            Assert.AreEqual(result.Length, uniqueValues.Count);
        }

        [Test]
        public void GetUniqueRandomValuesWithRandomSeed_ShouldReturnValuesInRange()
        {
            var result = RandomUtility.GetUniqueRandomValuesWithRandomSeed(1, 100, 10);
            
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, 1);
                Assert.LessOrEqual(value, 100);
            }
        }

        [Test]
        public void GetUniqueRandomValuesWithSeed_ShouldReturnCorrectCount()
        {
            const int seed = 12345;
            var result = RandomUtility.GetUniqueRandomValuesWithSeed(seed, 1, 100, 10);
            
            Assert.AreEqual(10, result.Length);
        }

        [Test]
        public void GetUniqueRandomValuesWithSeed_WithSameSeed_ShouldReturnSameValues()
        {
            const int seed = 12345;
            var result1 = RandomUtility.GetUniqueRandomValuesWithSeed(seed, 1, 100, 10);
            var result2 = RandomUtility.GetUniqueRandomValuesWithSeed(seed, 1, 100, 10);
            
            Assert.AreEqual(result1.Length, result2.Length);
            for (var i = 0; i < result1.Length; i++)
            {
                Assert.AreEqual(result1[i], result2[i]);
            }
        }

        [Test]
        public void GetUniqueRandomValuesWithSeed_ShouldReturnUniqueValues()
        {
            const int seed = 12345;
            var result = RandomUtility.GetUniqueRandomValuesWithSeed(seed, 1, 100, 10);
            
            var uniqueValues = new System.Collections.Generic.HashSet<int>(result);
            Assert.AreEqual(result.Length, uniqueValues.Count);
        }

        [Test]
        public void GetUniqueRandomValuesWithSeed_ShouldReturnValuesInRange()
        {
            const int seed = 12345;
            var result = RandomUtility.GetUniqueRandomValuesWithSeed(seed, 1, 100, 10);
            
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, 1);
                Assert.LessOrEqual(value, 100);
            }
        }

        [Test]
        public void GetUniqueRandomValuesWithSeed_CountGreaterThanRange_ShouldThrowException()
        {
            const int seed = 12345;

            Assert.Throws<InvalidOperationException>(() => 
                RandomUtility.GetUniqueRandomValuesWithSeed(seed, 1, 10, 11));
        }

        [Test]
        public void GetUniqueRandomValuesWithRandomSeed_CountGreaterThanRange_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => 
                RandomUtility.GetUniqueRandomValuesWithRandomSeed(1, 10, 11));
        }

        [Test]
        public void GetUniqueRandomValuesWithSeed_CountEqualsRange_ShouldReturnAllValues()
        {
            const int seed = 12345;
            var result = RandomUtility.GetUniqueRandomValuesWithSeed(seed, 1, 5, 5);
            
            Assert.AreEqual(5, result.Length);
            var uniqueValues = new System.Collections.Generic.HashSet<int>(result);
            Assert.AreEqual(5, uniqueValues.Count);
            
            // Should contain all values from 1 to 5
            for (var i = 1; i <= 5; i++)
            {
                Assert.Contains(i, result);
            }
        }
    }
}