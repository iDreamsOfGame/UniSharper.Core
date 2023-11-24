using System.IO;
using UnityEngine;

// ReSharper disable NotAccessedField.Local

namespace UniSharper.Samples
{
    internal class DirectoryFilesFieldAttributeSample : MonoBehaviour
    {
        [SerializeField, DirectoryFilesField(PlayerEnvironment.AssetsFolderName, SearchPattern.CSharpScriptFiles, SearchOption.AllDirectories, true)]
        private string fileName;

        [SerializeField, DirectoryFilesField(PlayerEnvironment.AssetsFolderName, SearchPattern.CSharpScriptFiles, SearchOption.AllDirectories)]
        private string fileNameWithoutExtension;

        [SerializeField, DirectoryFilesField(new[] { PlayerEnvironment.AssetsFolderName, "Packages/com.unity.ugui" }, SearchPattern.CSharpScriptFiles, SearchOption.AllDirectories, true)]
        private string fileNameInDirectories;
        
        [SerializeField, DirectoryFilesField(new[] { PlayerEnvironment.AssetsFolderName, "Packages/com.unity.ugui" }, SearchPattern.CSharpScriptFiles, SearchOption.AllDirectories, true, true)]
        private string fileNameInDirectoriesWithDescendingOrder;
    }
}