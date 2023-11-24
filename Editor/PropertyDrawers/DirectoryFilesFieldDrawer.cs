// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using UniSharper;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Property drawer for <see cref="UniSharper.DirectoryFilesFieldDrawer"/>.
    /// </summary>
    /// <seealso cref="PropertyDrawer"/>
    [CustomPropertyDrawer(typeof(DirectoryFilesFieldAttribute))]
    internal class DirectoryFilesFieldDrawer : PropertyDrawer
    {
        private class InternalPostprocessor : AssetPostprocessor
        {
            private static void OnPostprocessAllAssets(string[] importedAssets,
                string[] deletedAssets,
                string[] movedAssets,
                string[] movedFromAssetPaths)
            {
                directoryFileNamesMap?.Clear();
            }
        }

        private static Dictionary<string, string[]> directoryFileNamesMap;

        /// <summary>
        /// Override this method to make your own GUI for the property.
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);

                var fieldAttribute = (DirectoryFilesFieldAttribute)attribute;
                var directoryPaths = fieldAttribute.Directories;
                var searchPattern = fieldAttribute.SearchPattern;
                var searchOption = fieldAttribute.SearchOption;
                var withExtension = fieldAttribute.WithExtension;
                var orderByDescending = fieldAttribute.OrderByDescending;
                if (directoryPaths != null)
                {
                    var directoryFiles = new List<string>();
                    
                    foreach (var directoryPath in directoryPaths)
                    {
                        var fileNames = GetDirectoryFileNames(property, directoryPath, searchPattern, searchOption, withExtension);
                        directoryFiles.AddRange(fileNames);
                    }
                    
                    if (directoryFiles.Count > 0)
                    {
                        directoryFiles.Sort();
                        if (orderByDescending)
                            directoryFiles.Reverse();
                        
                        var index = !string.IsNullOrEmpty(property.stringValue) ? directoryFiles.IndexOf(property.stringValue) : 0;
                        index = Math.Max(index, 0);
                        index = EditorGUI.Popup(position, label.text, index, directoryFiles.ToArray());
                        property.stringValue = directoryFiles[index];
                    }
                    else
                    {
                        EditorGUI.Popup(position, label.text, 0, Array.Empty<string>());
                        property.stringValue = string.Empty;
                    }
                }
                else
                {
                    property.stringValue = string.Empty;
                }

                EditorGUI.EndProperty();
            }
            else
            {
                property.stringValue = string.Empty;
                EditorGUI.PropertyField(position, property, label);
            }
        }

        private static string[] GetDirectoryFileNames(SerializedProperty property,
            string dirPath,
            string searchPattern,
            SearchOption searchOption = SearchOption.TopDirectoryOnly,
            bool withExtension = false)
        {
            directoryFileNamesMap ??= new Dictionary<string, string[]>();
            var key = $"{property.serializedObject.GetHashCode()}.{property.propertyPath}.{Path.Combine(dirPath, searchPattern)}";

            if (!directoryFileNamesMap.ContainsKey(key) && Directory.Exists(dirPath))
            {
                string[] files;
                
                try
                {
                    files = Directory.GetFiles(dirPath, searchPattern, searchOption);
                }
                catch (Exception)
                {
                    files = Array.Empty<string>();
                }
                
                if (files.Length > 0)
                {
                    var fileNames = new string[files.Length];
                    for (var i = 0; i < fileNames.Length; i++)
                    {
                        fileNames[i] = withExtension ? Path.GetFileName(files[i]) : Path.GetFileNameWithoutExtension(files[i]);
                    }

                    directoryFileNamesMap.Add(key, fileNames);
                }
            }

            return directoryFileNamesMap.TryGetValue(key, out var result) ? result : Array.Empty<string>();
        }
    }
}