using UnityEngine;

namespace UniSharper.Examples.Attributes
{
    public class ReadOnlyFieldAttributeExample : MonoBehaviour
    {
        #region Fields

        [ReadOnlyField]
        public int PublicReadonlyField;

        [ReadOnlyField]
        [SerializeField]
        private float privateReadonlyField;

        #endregion Fields
    }
}