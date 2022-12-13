using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Color32UtilityTests
    {
        private const string ValidString = "RGBA(255, 255, 255, 255)";

        private const string InvalidString = "1.0)";
        
        private static readonly Color32 CorrectValue = Color.white;

        [Test]
        public void ParseTest()
        {
            var result = Color32Utility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = Color32Utility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = Color32Utility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}