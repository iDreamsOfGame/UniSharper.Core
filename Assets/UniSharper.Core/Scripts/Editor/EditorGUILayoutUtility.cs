// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

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
        #region Methods

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
            string text = filePath;

            using (new EditorGUIFieldScope(labelWidth))
            {
                EditorGUILayout.LabelField(label, new GUIContent(text), Styles.PathFieldStyle);
                bool buttonClicked = GUILayout.Button("Browse...", Styles.BrowseButtonStyle);

                if (buttonClicked)
                {
                    string newPath = UnityEditorUtility.OpenFilePanelWithFilters(title, directory, filters);

                    if (!string.IsNullOrEmpty(newPath))
                    {
                        text = newPath;
                    }
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
            string text = filePath;

            using (new EditorGUIFieldScope(labelWidth))
            {
                EditorGUILayout.LabelField(label, new GUIContent(text), Styles.PathFieldStyle);
                bool buttonClicked = GUILayout.Button("Browse...", Styles.BrowseButtonStyle);

                if (buttonClicked)
                {
                    string newPath = UnityEditorUtility.OpenFolderPanel(title, folder, defaultName);

                    if (!string.IsNullOrEmpty(newPath))
                    {
                        text = newPath;
                    }
                }
            }

            return text;
        }

        #endregion Methods

        #region Classes

        /// <summary>
        /// The collection of GUI styles.
        /// </summary>
        private class Styles
        {
            #region Fields

            /// <summary>
            /// The style of browse button.
            /// </summary>
            public static readonly GUIStyle BrowseButtonStyle = new GUIStyle(EditorStyles.miniButton)
            {
                fixedWidth = 75,
                fixedHeight = EditorStyles.miniButton.fixedHeight + 16
            };

            /// <summary>
            /// The style of path field.
            /// </summary>
            public static readonly GUIStyle PathFieldStyle = new GUIStyle(EditorStyles.textField)
            {
                normal = {
                    background = EditorStyles.textField.normal.background,
                    scaledBackgrounds =  EditorStyles.textField.normal.scaledBackgrounds,
                    textColor = Color.grey
                }
            };

            #endregion Fields
        }

        #endregion Classes
    }
}