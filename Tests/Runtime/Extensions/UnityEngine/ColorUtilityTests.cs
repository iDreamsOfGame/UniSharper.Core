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
        
        private const string ValidElementsString = "RGBA(1.0, 1.0, 1.0, 1.0)|RGBA(0.0, 0.0, 1.0)";

        private const string InvalidElementsString = "1.0)(2";
        
        private static readonly Color CorrectValue = Color.white;

        private static readonly Color CorrectValue2 = Color.blue;
        
        private static readonly float[] ValidElementValues = { 1f, 1f, 1f, 1f, 0f, 0f, 1f, 1f };
        
        private static readonly float[] InvalidElementValues = { 1f, 1f, 1f, 2f, 2f, 2f };
        
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
        
        [Test]
        public void ParseArrayTest1()
        {
            var result = UniColorUtility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest1()
        {
            var result = UniColorUtility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest1()
        {
            var result = UniColorUtility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest2()
        {
            var result = UniColorUtility.ParseArray(ValidElementValues);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest2()
        {
            var result = UniColorUtility.TryParseArray(ValidElementValues, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest2()
        {
            var result = UniColorUtility.TryParseArray(InvalidElementValues, out _);
            Assert.AreEqual(false, result);
        }
    }
}