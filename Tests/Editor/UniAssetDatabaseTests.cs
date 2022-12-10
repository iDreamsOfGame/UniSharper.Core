using NUnit.Framework;
using UnityEngine;

namespace UniSharperEditor.Tests
{
    public class UniAssetDatabaseTests
    {
        [Test]
        public void LoadEditorResourceInPackageFolder()
        {
            const string packageName = "io.github.idreamsofgame.resharp.core";
            var searchInFolder = $"{EditorEnvironment.PackagesFolderName}/{packageName}";
            var assets = UniAssetDatabase.LoadEditorResources<TextAsset>("package", searchInFolder);
            Assert.Greater(assets.Length, 0);
        }
    }
}