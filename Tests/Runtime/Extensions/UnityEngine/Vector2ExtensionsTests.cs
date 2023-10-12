using NUnit.Framework;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Tests
{
    internal class Vector2ExtensionsTests
    {
        [Test]
        public void IsValidTest1()
        {
            var result = new Vector2(float.NaN, float.NaN).IsValid();
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void IsValidTest2()
        {
            var result = Vector2.positiveInfinity.IsValid();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsValidTest3()
        {
            var result = Vector2.zero.IsValid();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void ToAccurateStringTest()
        {
            var result = new Vector2(0.0001f, 0.0001f).ToAccurateString();
            Assert.AreEqual("(0.0001, 0.0001)", result);
        }
    }
}