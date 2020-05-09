using UnityEngine;

#pragma warning disable 0649

namespace UniSharper.Examples.Attributes
{
    internal class ReadOnlyFieldAttributeExample : MonoBehaviour
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