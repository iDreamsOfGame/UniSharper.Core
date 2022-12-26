using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class QuaternionUtilityTests
    {
        private const string ValidString = "(0.0, 0.0, 0.0, 1.0)";

        private const string InvalidString = "1.0)";
        
        private const string ValidElementsString = "(0.0, 0.0, 0.0, 1.0)|(0.0, 0.0, 0.0, 2.0)";

        private const string InvalidElementsString = "1.0)(2";
        
        private static readonly Quaternion CorrectValue = Quaternion.identity;

        private static readonly Quaternion CorrectValue2 = new(0, 0, 0, 2);

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
        
        [Test]
        public void ParseArrayTest()
        {
            var result = QuaternionUtility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest()
        {
            var result = QuaternionUtility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest()
        {
            var result = QuaternionUtility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
    }
}