using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class RangeIntUtilityTests
    {
        private const string ValidString = "(1, 10)";
        
        private const string InvalidString = "1)";
        
        private const string ValidElementsString = "(1, 10)|(2, 20)";

        private const string InvalidElementsString = "1)(2";
        
        private static readonly RangeInt CorrectValue = new RangeInt(1, 10);
        
        private static readonly RangeInt CorrectValue2 = new RangeInt(2, 20);
        
        private static readonly int[] ValidElementValues = { 1, 10, 2, 20 };
        
        private static readonly int[] InvalidElementValues = { 1, 10, 2 };
        
        [Test]
        public void ParseTest()
        {
            var result = RangeIntUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = RangeIntUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = RangeIntUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest1()
        {
            var result = RangeIntUtility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest1()
        {
            var result = RangeIntUtility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest1()
        {
            var result = RangeIntUtility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest2()
        {
            var result = RangeIntUtility.ParseArray(ValidElementValues);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest2()
        {
            var result = RangeIntUtility.TryParseArray(ValidElementValues, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest2()
        {
            var result = RangeIntUtility.TryParseArray(InvalidElementValues, out _);
            Assert.AreEqual(false, result);
        }
    }
}