using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Vector3IntUtilityTests
    {
        private const string ValidString = "(1, 1, 1)";
        
        private const string ValidString2 = "(1, 1)";
        
        private const string InvalidString = "1)";
        
        private static readonly Vector3Int CorrectValue = Vector3Int.one;

        private static readonly Vector3Int CorrectValue2 = new(1, 1);
        
        [Test]
        public void ParseTest()
        {
            var result = Vector3IntUtility.Parse(ValidString);
            Assert.AreEqual(CorrectValue, result);
        }

        [Test]
        public void ParseTest2()
        {
            var result = Vector3IntUtility.Parse(ValidString2);
            Assert.AreEqual(CorrectValue2, result);
        }

        [Test]
        public void TryParseSuccessTest()
        {
            var result = Vector3IntUtility.TryParse(ValidString, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue));
        }

        [Test]
        public void TryParseSuccessTest2()
        {
            var result = Vector3IntUtility.TryParse(ValidString2, out var value);
            Assert.True(result.Equals(true) && value.Equals(CorrectValue2));
        }

        [Test]
        public void TryParseFailTest()
        {
            var result = Vector3IntUtility.TryParse(InvalidString, out _);
            Assert.AreEqual(false, result);
        }
    }
}