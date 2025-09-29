using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Vector2IntUtilityTests
    {
        private const string ValidString = "(1, 1)";
        
        private const string InvalidString = "1)";
        
        private const string ValidElementsString = "(1, 1)|(2, 2)";

        private const string InvalidElementsString = "1)(2";
        
        private static readonly Vector2Int CorrectValue = Vector2Int.one;
        
        private static readonly int[] ValidElementValues = { 1, 1, 2, 2 };
        
        private static readonly int[] InvalidElementValues = { 1, 1, 2 };
        
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
        
        [Test]
        public void ParseArrayTest1()
        {
            var result = Vector2IntUtility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArraySuccessTest1()
        {
            var result = Vector2IntUtility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArrayFailTest1()
        {
            var result = Vector2IntUtility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest2()
        {
            var result = Vector2IntUtility.ParseArray(ValidElementValues);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArraySuccessTest2()
        {
            var result = Vector2IntUtility.TryParseArray(ValidElementValues, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue * 2));
        }

        [Test]
        public void TryParseArrayFailTest2()
        {
            var result = Vector2IntUtility.TryParseArray(InvalidElementValues, out _);
            Assert.AreEqual(false, result);
        }
    }
}