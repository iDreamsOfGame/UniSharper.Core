// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using UnityEditor;
using UnityEngine;
using UnityEditorUtility = UnityEditor.EditorUtility;

namespace UniSharperEditor
{
    /// <summary>
    /// Utilities function to handle editor GUI layout.
    /// </summary>
    public static class EditorGUILayoutUtility
    {
        /// <summary>
        /// Make a file field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="title">The title.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="labelWidth">Width of the label.</param>
        /// <returns>The path of the file.</returns>
        public static string FileField(GUIContent label, string filePath, string title, string directory = "", string[] filters = null, float labelWidth = 0f)
        {
            var text = filePath;

            using (new EditorGUIFieldScope(labelWidth))
            {
                EditorGUILayout.LabelField(label, new GUIContent(text), Styles.PathFieldStyle);
                try
                {
                    if (GUILayout.Button("Browse...", Styles.BrowseButtonStyle))
                    {
                        var newPath = UnityEditorUtility.OpenFilePanelWithFilters(title, directory, filters);
                        if (!string.IsNullOrEmpty(newPath))
                            text = newPath;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return text;
        }

        /// <summary>
        /// Make a folder field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="title">The title.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="defaultName">The default name.</param>
        /// <param name="labelWidth">Width of the label.</param>
        /// <returns>The path of the folder.</returns>
        public static string FolderField(GUIContent label, string filePath, string title, string folder = "", string defaultName = "", float labelWidth = 0f)
        {
            var text = filePath;

            using (new EditorGUIFieldScope(labelWidth))
            {
                EditorGUILayout.LabelField(label, new GUIContent(text), Styles.PathFieldStyle);
                try
                {
                    if (GUILayout.Button("Browse...", Styles.BrowseButtonStyle))
                    {
                        var newPath = UnityEditorUtility.OpenFolderPanel(title, folder, defaultName);
                        if (!string.IsNullOrEmpty(newPath))
                            text = newPath;

                        GUIUtility.ExitGUI();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return text;
        }

        /// <summary>
        /// The collection of GUI styles.
        /// </summary>
        private static class Styles
        {
            /// <summary>
            /// The style of browse button.
            /// </summary>
            public static readonly GUIStyle BrowseButtonStyle = new(EditorStyles.miniButton)
            {
                fixedWidth = 75,
                fixedHeight = EditorStyles.miniButtonRight.fixedHeight
            };

            /// <summary>
            /// The style of path field.
            /// </summary>
            public static readonly GUIStyle PathFieldStyle = new(EditorStyles.textField)
            {
                normal = {
                    background = EditorStyles.textField.normal.background,
                    scaledBackgrounds =  EditorStyles.textField.normal.scaledBackgrounds,
                    textColor = Color.grey
                }
            };
        }
    }
}