using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Vector2UtilityTests
    {
        private const string ValidString = "(1.0, 1.0)";
        
        private const string InvalidString = "1.0)";

        private static readonly Vector2 CorrectValue = Vector2.one;
        
        [Test]
        public void ParseTest()
        {
            var result = Vector2Utility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = Vector2Utility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = Vector2Utility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}