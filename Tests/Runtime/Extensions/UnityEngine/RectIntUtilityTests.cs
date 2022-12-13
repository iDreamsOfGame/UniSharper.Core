using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class RectIntUtilityTests
    {
        private const string ValidString = "(x:1, y:1, width:100, height:100)";
        
        private const string InvalidString = "1)";

        private static readonly RectInt CorrectValue = new(1, 1, 100, 100);
        
        [Test]
        public void ParseTest()
        {
            var result = RectIntUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = RectIntUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = RectIntUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}