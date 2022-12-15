// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.IO;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Provides utilities to handle prefab operation. 
    /// </summary>
    public static class PrefabUtility
    {
        /// <summary>
        /// Gets all prefabs under the specific path.
        /// </summary>
        /// <param name="dirPath">The path to search prefabs.</param>
        /// <returns>All prefabs under the specific path.</returns>
        public static GameObject[] GetPrefabs(string dirPath)
        {
            var guids = AssetDatabase.FindAssets("t:gameObject", new[] { dirPath });
            var prefabs = new GameObject[guids.Length];

            for (var i = 0; i < prefabs.Length; i++)
            {
                var guid = guids[i];
                var path = AssetDatabase.GUIDToAssetPath(guid);
                prefabs[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            }

            return prefabs;
        }
        
        /// <summary>
        /// Gets the names of all prefabs under the specific path.
        /// </summary>
        /// <param name="dirPath">The path to search prefabs.</param>
        /// <returns>The names of all prefabs under the specific path.</returns>
        public static string[] GetPrefabNames(string dirPath)
        {
            var guids = AssetDatabase.FindAssets("t:gameObject", new[] { dirPath });
            var prefabNames = new string[guids.Length];

            for (var i = 0; i < prefabNames.Length; i++)
            {
                var guid = guids[i];
                var path = AssetDatabase.GUIDToAssetPath(guid);
                prefabNames[i] = Path.GetFileNameWithoutExtension(path);
            }
            
            return prefabNames;
        }
    }
}