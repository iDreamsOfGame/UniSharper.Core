using NUnit.Framework;
using UniSharperEditor;
using UnityEngine;

public class AssetDatabaseUtilityTests
{
    [Test]
    public void LoadEditorResourceInPackageFolder()
    {
        const string packageName = "io.github.idreamsofgame.resharp.core";
        var searchInFolder = $"{EditorEnvironment.PackagesFolderName}/{packageName}";
        var assets = AssetDatabaseUtility.LoadEditorResources<TextAsset>("package", searchInFolder);
        Assert.Greater(assets.Length, 0);
    }
}