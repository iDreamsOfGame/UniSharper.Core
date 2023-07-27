using UnityEngine;

namespace UniSharper.Samples
{
    internal class SortingLayerFieldAttributeSample : MonoBehaviour
    {
        [SortingLayerField]
        [SerializeField]
        private string selectedSortingLayer;

        private void Start()
        {
            Debug.Log($"Selected sorting layer: {selectedSortingLayer}");
        }
    }
}