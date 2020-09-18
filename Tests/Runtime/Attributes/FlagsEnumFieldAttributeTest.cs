using System;
using UnityEngine;

namespace UniSharper.Tests.Attributes
{
    internal class FlagsEnumFieldAttributeTest : MonoBehaviour
    {
        #region Fields

        [FlagsEnumField]
        public EnumFlagsTest enumFlagsField = EnumFlagsTest.None;

        #endregion Fields

        #region Enums

        [Flags]
        public enum EnumFlagsTest
        {
            None = 0,
            Value1 = 1,
            Value2 = 2,
            Value3 = 4,
            Value4 = 8
        }

        #endregion Enums

        #region Methods

        private void Start()
        {
            Debug.Log((int)(EnumFlagsTest.Value1 | EnumFlagsTest.Value2));
        }

        #endregion Methods
    }
}