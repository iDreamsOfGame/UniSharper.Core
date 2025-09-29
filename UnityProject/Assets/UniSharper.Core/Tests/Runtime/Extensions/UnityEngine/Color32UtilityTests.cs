using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Color32UtilityTests
    {
        private const string ValidString = "RGBA(255, 255, 255, 255)";

        private const string InvalidString = "1.0)";
        
        private const string ValidElementsString = "RGBA(255, 255, 255, 255)|RGBA(0, 0, 255, 255)";
        
        private const string InvalidElementsString = "1.0)(2";
        
        private static readonly Color32 CorrectValue = Color.white;
        
        private static readonly Color32 CorrectValue2 = Color.blue;
        
        private static readonly byte[] ValidElementValues = { 255, 255, 255, 255, 0, 0, 255, 255 };
        
        private static readonly byte[] InvalidElementValues = { 255, 255, 255, 0, 0, 0 };

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
        
        [Test]
        public void ParseArrayTest1()
        {
            var result = Color32Utility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest1()
        {
            var result = Color32Utility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest1()
        {
            var result = Color32Utility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest2()
        {
            var result = Color32Utility.ParseArray(ValidElementValues);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest2()
        {
            var result = Color32Utility.TryParseArray(ValidElementValues, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest2()
        {
            var result = Color32Utility.TryParseArray(InvalidElementValues, out _);
            Assert.AreEqual(false, result);
        }
    }
}