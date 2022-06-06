using System;
using UnityEngine;

namespace UniSharper.Samples
{
    internal class FlagsEnumFieldAttributeSample : MonoBehaviour
    {
        [FlagsEnumField]
        public EnumFlagsTest enumFlagsField = EnumFlagsTest.None;

        [Flags]
        public enum EnumFlagsTest
        {
            None = 0,
            Value1 = 1,
            Value2 = 2,
            Value3 = 4,
            Value4 = 8
        }
        
        private void Start()
        {
            Debug.Log((int)(EnumFlagsTest.Value1 | EnumFlagsTest.Value2));
        }
    }
}