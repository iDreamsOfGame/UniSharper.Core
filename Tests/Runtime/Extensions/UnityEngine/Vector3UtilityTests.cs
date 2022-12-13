using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Vector3UtilityTests
    {
        private const string ValidString = "(1.0, 1.0, 1.0)";
        
        private const string ValidString2 = "(1.0, 1.0)";
        
        private const string InvalidString = "1.0)";
        
        private static readonly Vector3 CorrectValue = Vector3.one;

        private static readonly Vector3 CorrectValue2 = Vector2.one;
        
        [Test]
        public void ParseTest()
        {
            var result = Vector3Utility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void ParseTest2()
        {
            var result = Vector3Utility.Parse(ValidString2);
            Assert.AreEqual(CorrectValue2, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = Vector3Utility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseSuccessTest2()
        {
            var result = Vector3Utility.TryParse(ValidString2, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue2));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = Vector3Utility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}