// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Extensions
{
    internal static class MonoScriptMetadataFixer
    {
        [MenuItem("Assets/Reserialize Mono Script Metadata", true)]
        private static bool ReserializeMonoScriptsMetadataValidator()
        {
            var selectedObjects = Selection.GetFiltered<Object>(SelectionMode.Assets);
            foreach (var selectedObject in selectedObjects)
            {
                if (selectedObject is MonoScript)
                    return true;
                
                var assetPath = AssetDatabase.GetAssetPath(selectedObject);
                if (Directory.Exists(assetPath))
                    return AssetDatabase.FindAssets("t:monoScript", new[] { assetPath }).Length > 0;
            }

            return false;
        }

        [MenuItem("Assets/Reserialize Mono Script Metadata", false, 102)]
        private static void ReserializeMonoScriptsMetadata()
        {
            var targets = new List<string>();
            var selectedObjects = Selection.GetFiltered<Object>(SelectionMode.Assets);
            foreach (var selectedObject in selectedObjects)
            {
                var assetPath = AssetDatabase.GetAssetPath(selectedObject);
                if (Directory.Exists(assetPath))
                {
                    var guids = AssetDatabase.FindAssets("t:monoScript", new[] { assetPath });
                    targets.AddRange(guids.Select(AssetDatabase.GUIDToAssetPath));
                }
                else if (selectedObject is MonoScript)
                {
                    targets.Add(assetPath);
                }
            }

            if (targets.Count > 0)
                ReserializeAllMonoScriptsMetadata(targets.ToArray());
        }
        
        private static void ReserializeAllMonoScriptsMetadata(string[] monoScriptAssets)
        {
            if (monoScriptAssets == null || monoScriptAssets.Length == 0) 
                return;
            
            AssetDatabase.ForceReserializeAssets(monoScriptAssets, ForceReserializeAssetsOptions.ReserializeMetadata);
            foreach (var monoScriptAsset in monoScriptAssets)
            {
                Debug.Log($"MonoScript metadata has been reserialized: {monoScriptAsset}.");
            }
        }
    }
}