using UnityEngine;

#pragma warning disable 0649

namespace UniSharper.Tests.Attributes
{
    internal class ReadOnlyFieldAttributeTest : MonoBehaviour
    {
        #region Fields

        [ReadOnlyField]
        public int ReadonlyField;

        [ReadOnlyField]
        [SerializeField]
        private float privateReadonlyField;

        #endregion Fields
    }
}