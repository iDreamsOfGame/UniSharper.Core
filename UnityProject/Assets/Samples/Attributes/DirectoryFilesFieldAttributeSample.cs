using System.IO;
using UnityEngine;

// ReSharper disable NotAccessedField.Local

namespace UniSharper.Samples
{
    internal class DirectoryFilesFieldAttributeSample : MonoBehaviour
    {
        [SerializeField, DirectoryFilesField(new[] { "Assets" }, "*.cs", SearchOption.AllDirectories, true)]
        private string fileName;

        [SerializeField, DirectoryFilesField(new[] { "Assets" }, "*.cs", SearchOption.AllDirectories)]
        private string fileNameWithoutExtension;

        [SerializeField, DirectoryFilesField(new[] { "Assets", "Packages/com.unity.ugui" }, "*.cs", SearchOption.AllDirectories, true)]
        private string fileNameInDirectories;
        
        [SerializeField, DirectoryFilesField(new[] { "Assets", "Packages/com.unity.ugui" }, "*.cs", SearchOption.AllDirectories, true, true)]
        private string fileNameInDirectoriesWithDescendingOrder;
    }
}