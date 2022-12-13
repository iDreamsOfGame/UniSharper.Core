using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class RectUtilityTests
    {
        private const string ValidString = "(x:1.00, y:1.00, width:100.00, height:100.00)";
        
        private const string InvalidString = "1.0)";

        private static readonly Rect CorrectValue = new(1, 1, 100, 100);
        
        [Test]
        public void ParseTest()
        {
            var result = RectUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = RectUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = RectUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}