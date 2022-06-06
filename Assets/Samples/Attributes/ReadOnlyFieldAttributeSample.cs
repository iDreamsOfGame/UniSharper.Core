using UnityEngine;

#pragma warning disable 0649

namespace UniSharper.Samples
{
    internal class ReadOnlyFieldAttributeSample : MonoBehaviour
    {
        [ReadOnlyField]
        public int ReadonlyField;

        [ReadOnlyField]
        [SerializeField]
        private float privateReadonlyField;
    }
}