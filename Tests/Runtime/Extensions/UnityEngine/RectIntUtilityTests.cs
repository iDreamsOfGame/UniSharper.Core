using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class RectIntUtilityTests
    {
        private const string ValidString = "(x:1, y:1, width:100, height:100)";
        
        private const string InvalidString = "1)";

        private const string ValidElementsString = "(x:1, y:1, width:100, height:100)|(x:2, y:2, width:200, height:200)";

        private const string InvalidElementsString = "1.0)(2";

        private static readonly RectInt CorrectValue = new(1, 1, 100, 100);
        
        private static readonly RectInt CorrectValue2 = new(2, 2, 200, 200);
        
        private static readonly int[] ValidElementValues = { 1, 1, 100, 100, 2, 2, 200, 200 };
        
        private static readonly int[] InvalidElementValues = { 1, 1, 1, 2, 2, 2 };
        
        [Test]
        public void ParseTest()
        {
            var result = RectIntUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = RectIntUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = RectIntUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest1()
        {
            var result = RectIntUtility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest1()
        {
            var result = RectIntUtility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest1()
        {
            var result = RectIntUtility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest2()
        {
            var result = RectIntUtility.ParseArray(ValidElementValues);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest2()
        {
            var result = RectIntUtility.TryParseArray(ValidElementValues, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest2()
        {
            var result = RectIntUtility.TryParseArray(InvalidElementValues, out _);
            Assert.AreEqual(false, result);
        }
    }
}