using System;
using UnityEngine;

namespace UniSharper.Examples.Attributes
{
    public class FlagsEnumFieldAttributeExample : MonoBehaviour
    {
        #region Fields

        [FlagsEnumField]
        public EnumFlagsTest enumFlagsField;

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