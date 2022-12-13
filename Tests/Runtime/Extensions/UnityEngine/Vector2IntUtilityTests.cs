using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Vector2IntUtilityTests
    {
        private const string ValidString = "(1, 1)";
        
        private const string InvalidString = "1)";
        
        private static readonly Vector2Int CorrectValue = Vector2Int.one;
        
        [Test]
        public void ParseTest()
        {
            var result = Vector2IntUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = Vector2IntUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = Vector2IntUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}