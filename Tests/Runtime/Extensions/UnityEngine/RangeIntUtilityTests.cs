using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class RangeIntUtilityTests
    {
        private const string ValidString = "(1, 10)";
        
        private const string InvalidString = "1)";
        
        private static readonly RangeInt CorrectValue = new RangeInt(1, 10);
        
        [Test]
        public void ParseTest()
        {
            var result = RangeIntUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = RangeIntUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = RangeIntUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}