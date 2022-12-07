using UnityEngine;

namespace UniSharper.Samples
{
    internal class LayerFieldAttributeSample : MonoBehaviour
    {
        [LayerField]
        [SerializeField]
        private int selectedLayer;

        [LayerField(false)]
        [SerializeField]
        private int selectedWithoutDefaultDrawer;

        private void Start()
        {
            Debug.Log($"Selected layer: {LayerMask.LayerToName(selectedLayer)}");
            Debug.Log($"Selected layer without default drawer: {LayerMask.LayerToName(selectedWithoutDefaultDrawer)}");
        }
    }
}