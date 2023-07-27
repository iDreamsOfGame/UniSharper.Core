using UnityEngine;

namespace UniSharper.Samples
{
    internal class TagFieldAttributeSample : MonoBehaviour
    {
        [TagField]
        [SerializeField]
        private string selectedTag;

        [TagField(false)]
        [SerializeField]
        private string selectedTagWithoutDefaultDrawer;

        private void Start()
        {
            Debug.Log($"Selected tag: {selectedTag}");
            Debug.Log($"Selected tag without default drawer: {selectedTagWithoutDefaultDrawer}");
        }
    }
}