using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class QuaternionUtilityTests
    {
        private const string ValidString = "(0.0, 0.0, 0.0, 1.0)";

        private const string InvalidString = "1.0)";
        
        private static readonly Quaternion CorrectValue = Quaternion.identity;

        [Test]
        public void ParseTest()
        {
            var result = QuaternionUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = QuaternionUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = QuaternionUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}