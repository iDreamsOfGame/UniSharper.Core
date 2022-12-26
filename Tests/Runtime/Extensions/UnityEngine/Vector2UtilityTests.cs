using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Vector2UtilityTests
    {
        private const string ValidString = "(1.0, 1.0)";
        
        private const string InvalidString = "1.0)";

        private const string ValidElementsString = "(1.0, 1.0)|(2.0, 2.0)";

        private const string InvalidElementsString = "1.0)(2";

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
        
        [Test]
        public void ParseArrayTest()
        {
            var result = Vector2Utility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArraySuccessTest()
        {
            var result = Vector2Utility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArrayFailTest()
        {
            var result = Vector2Utility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
    }
}