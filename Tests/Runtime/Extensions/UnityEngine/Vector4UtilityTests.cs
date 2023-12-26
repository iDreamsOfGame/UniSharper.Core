using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Vector4UtilityTests
    {
        private const string ValidString = "(1.0, 1.0, 1.0, 1.0)";
        
        private const string ValidString2 = "(1.0, 1.0, 1.0)";

        private const string ValidString3 = "(1.0, 1.0)";
        
        private const string InvalidString = "1.0)";
        
        private const string ValidElementsString = "(1.0, 1.0, 1.0, 1.0)|(2.0, 2.0, 2.0, 2.0)";

        private const string InvalidElementsString = "1.0)(2";
        
        private static readonly Vector4 CorrectValue = Vector4.one;

        private static readonly Vector4 CorrectValue2 = Vector3.one;

        private static readonly Vector4 CorrectValue3 = Vector2.one;
        
        private static readonly float[] ValidElementValues = { 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 2.0f, 2.0f, 2.0f };
        
        private static readonly float[] InvalidElementValues = { 1.0f, 1.0f, 1.0f, 2.0f, 2.0f, 2.0f };
        
        [Test]
        public void ParseTest()
        {
            var result = Vector4Utility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void ParseTest2()
        {
            var result = Vector4Utility.Parse(ValidString2);
            Assert.AreEqual(CorrectValue2, result);
        }
        
        [Test]
        public void ParseTest3()
        {
            var result = Vector4Utility.Parse(ValidString3);
            Assert.AreEqual(CorrectValue3, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = Vector4Utility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseSuccessTest2()
        {
            var result = Vector4Utility.TryParse(ValidString2, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue2));
        }
        
        [Test]
        public void TryParseSuccessTest3()
        {
            var result = Vector4Utility.TryParse(ValidString3, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue3));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = Vector4Utility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest1()
        {
            var result = Vector4Utility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArraySuccessTest1()
        {
            var result = Vector4Utility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArrayFailTest1()
        {
            var result = Vector4Utility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest2()
        {
            var result = Vector4Utility.ParseArray(ValidElementValues);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArraySuccessTest2()
        {
            var result = Vector4Utility.TryParseArray(ValidElementValues, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArrayFailTest2()
        {
            var result = Vector4Utility.TryParseArray(InvalidElementValues, out _);
            Assert.AreEqual(false, result);
        }
    }
}