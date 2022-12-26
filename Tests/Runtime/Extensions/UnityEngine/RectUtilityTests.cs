using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class RectUtilityTests
    {
        private const string ValidString = "(x:1.00, y:1.00, width:100.00, height:100.00)";
        
        private const string InvalidString = "1.0)";
        
        private const string ValidElementsString = "(x:1.00, y:1.00, width:100.00, height:100.00)|(x:2.00, y:2.00, width:200.00, height:200.00)";

        private const string InvalidElementsString = "1.0)(2";

        private static readonly Rect CorrectValue = new(1, 1, 100, 100);
        
        private static readonly Rect CorrectValue2 = new(2, 2, 200, 200);
        
        [Test]
        public void ParseTest()
        {
            var result = RectUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = RectUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = RectUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void ParseArrayTest()
        {
            var result = RectUtility.ParseArray(ValidElementsString);
            Assert.True(result[0].Equals(CorrectValue) && result[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArraySuccessTest()
        {
            var result = RectUtility.TryParseArray(ValidElementsString, out var value);
            Assert.True(result.Equals(true) && value[0].Equals(CorrectValue) && value[1].Equals(CorrectValue2));
        }

        [Test]
        public void TryParseArrayFailTest()
        {
            var result = RectUtility.TryParseArray(InvalidElementsString, out _, string.Empty);
            Assert.AreEqual(false, result);
        }
    }
}