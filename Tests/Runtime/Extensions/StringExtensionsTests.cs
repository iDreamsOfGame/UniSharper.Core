using System;
using NUnit.Framework;
using UnityEngine;

namespace UniSharper.Tests
{
    public class StringExtensionsTests
    {
        [Test]
        public void ToColorTest()
        {
            var value = "RGBA(1.0, 1.0, 1.0, 1.0)";
            var color = value.ToColor();
            Assert.AreEqual(Color.white, color);
        }
        
        [Test]
        public void ToColor32Test()
        {
            var value = "RGBA(255, 255, 255, 255)";
            var color32 = value.ToColor32();
            Assert.AreEqual(Color.white, (Color)color32);
        }

        [Test]
        public void ToRectTest()
        {
            var value = "(x:0.00, y:1.00, width:10.00, height:10.00)";
            var rect = value.ToRect();
            Assert.AreEqual(new Rect(0, 1, 10, 10), rect);
        }

        [Test]
        public void ToRectIntTest()
        {
            var value = "(x:0, y:0, width:100, height:100)";
            var rect = value.ToRectInt();
            Assert.AreEqual(new RectInt(0, 0, 100, 100), rect);
        }

        [Test]
        public void ToQuaternionTest()
        {
            var value = "(1.0, 1.0, 1.0, 1.0)";
            var quaternion = value.ToQuaternion();
            Assert.AreEqual(new Quaternion(1, 1, 1, 1), quaternion);
        }

        [Test]
        public void ToRangeIntTest()
        {
            var value = "(0, 10)";
            var rangeInt = value.ToRangeInt();
            Assert.AreEqual(new RangeInt(0, 10), rangeInt);
        }

        [Test]
        public void ToVector4Test()
        {
            var value = "(1.0, 1.0, 1.0, 1.0)";
            var vector4 = value.ToVector4();
            Assert.AreEqual(Vector4.one, vector4);
        }

        [Test]
        public void ToVector3Test()
        {
            var value = "(1.0, 1.0, 1.0)";
            var vector3 = value.ToVector3();
            Assert.AreEqual(Vector3.one, vector3);
        }

        [Test]
        public void ToVector3IntTest()
        {
            var value = "(1, 1, 1)";
            var vector3Int = value.ToVector3Int();
            Assert.AreEqual(Vector3Int.one, vector3Int);
        }

        [Test]
        public void ToVector2Test()
        {
            var value = "(1.0, 1.0)";
            var vector2 = value.ToVector2();
            Assert.AreEqual(Vector2.one, vector2);
        }

        [Test]
        public void ToVector2IntTest()
        {
            var value = "(1, 1)";
            var vector2Int = value.ToVector2Int();
            Assert.AreEqual(Vector2Int.one, vector2Int);
        }
    }
}
