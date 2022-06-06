// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UniSharperEditor
{
    /// <summary>
    /// AssetDatabase utility functions.
    /// </summary>
    public static class AssetDatabaseUtility
    {
        /// <summary>
        /// Load editor resources
        /// </summary>
        /// <param name="name">The name of resources.</param>
        /// <typeparam name="T">The type definition of resources.</typeparam>
        /// <returns>The editor resources, if exists; otherwise return null.</returns>
        public static T[] LoadEditorResources<T>(string name) where T : Object
        {
            var type = typeof(T).Name.ToCamelCase();
            var guids = AssetDatabase.FindAssets($"{name} t: {type}", new[] { EditorEnvironment.AssetsFolderName });

            if (guids.Length > 0)
            {
                var result = new T[guids.Length];

                for (var i = 0; i < result.Length; i++)
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                    result[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                }

                return result;
            }

            return null;
        }
    }
}