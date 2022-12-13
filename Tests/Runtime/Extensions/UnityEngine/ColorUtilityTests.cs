using NUnit.Framework;
using UnityEngine;
using UniColorUtility = UniSharper.Extensions.ColorUtility;

namespace UniSharper.Tests
{
    internal class ColorUtilityTests
    {
        private const string ValidString = "RGBA(1.0, 1.0, 1.0, 1.0)";
        
        private const string ValidString2 = "RGBA(0.0, 0.0, 1.0)";
        
        private const string InvalidString = "1.0)";
        
        private static readonly Color CorrectValue = Color.white;

        private static readonly Color CorrectValue2 = Color.blue;
        
        [Test]
        public void ParseTest()
        {
            var result = UniColorUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }
        
        [Test]
        public void ParseTest2()
        {
            var result = UniColorUtility.Parse(ValidString2);
            Assert.AreEqual(CorrectValue2, result);
        }
        
        [Test]
        public void TryParseSuccessTest()
        {
            var result = UniColorUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }
        
        [Test]
        public void TryParseSuccessTest2()
        {
            var result = UniColorUtility.TryParse(ValidString2, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue2));
        }
        
        [Test]
        public void TryParseFailTest()
        {
            var result = UniColorUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}