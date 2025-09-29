// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using ReSharp.Extensions;
using UniSharper;
using UnityEditor;
using UnityObject = UnityEngine.Object;

namespace UniSharperEditor
{
    /// <summary>
    /// AssetDatabase utility functions.
    /// </summary>
    public static class UniAssetDatabase
    {
        /// <summary>
        /// Load editor resources
        /// </summary>
        /// <param name="name">The name of resources.</param>
        /// <param name="searchInFolder">The folder where the search will start. </param>
        /// <typeparam name="T">The type definition of resources.</typeparam>
        /// <returns>The editor resources, if exists; otherwise return null.</returns>
        public static T[] LoadEditorResources<T>(string name, string searchInFolder = PlayerEnvironment.AssetsFolderName) where T : UnityObject
        {
            var type = typeof(T).Name.ToCamelCase();
            var searchInFolders = string.IsNullOrEmpty(searchInFolder) ? null : new[] { searchInFolder };
            var guids = AssetDatabase.FindAssets($"{name} t: {type}", searchInFolders);

            if (guids == null || guids.Length == 0) 
                return null;
            
            var result = new T[guids.Length];
            for (var i = 0; i < result.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                result[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return result;
        }
    }
}