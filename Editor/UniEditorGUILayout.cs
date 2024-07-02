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
    public static class UniEditorGUILayout
    {
        /// <summary>
        /// Create a field scope on the editor GUI Layer. 
        /// </summary>
        /// <seealso cref="System.IDisposable"/>
        public class FieldScope : GUI.Scope
        {
            private readonly float cachedLabelWidth;

            /// <summary>
            /// Initializes a new instance of the <see cref="FieldScope"/> class.
            /// </summary>
            /// <param name="labelWidth">Width of the label.</param>
            public FieldScope(float labelWidth = 0)
            {
                cachedLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = labelWidth;
                EditorGUILayout.BeginHorizontal();
            }

            protected override void CloseScope()
            {
                EditorGUILayout.EndHorizontal();
                EditorGUIUtility.labelWidth = cachedLabelWidth;
            }
        }
        
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
            using (new FieldScope(labelWidth))
            {
                EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth - 1));

                var enabled = GUI.enabled;
                GUI.enabled = false;
                EditorGUILayout.TextField(string.Empty, filePath);
                GUI.enabled = enabled;

                if (GUILayout.Button("Browse...", Styles.BrowseButtonStyle))
                {
                    GUIUtility.hotControl = 0;
                    DragAndDrop.activeControlID = 0;
                    GUIUtility.keyboardControl = 0;

                    var path = UnityEditorUtility.OpenFilePanelWithFilters(title, directory, filters);
                    if (!string.IsNullOrEmpty(path))
                    {
                        filePath = path;
                        GUI.changed = true;
                    }
                }

                return filePath;
            }
        }

        /// <summary>
        /// Make a folder field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="title">The title.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="defaultName">The default name.</param>
        /// <param name="labelWidth">Width of the label.</param>
        /// <returns>The path of the folder.</returns>
        public static string FolderField(GUIContent label, string folderPath, string title, string folder = "", string defaultName = "", float labelWidth = 0f)
        {
            using (new FieldScope(labelWidth))
            {
                EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth - 1));

                var enabled = GUI.enabled;
                GUI.enabled = false;
                EditorGUILayout.TextField(string.Empty, folderPath);
                GUI.enabled = enabled;

                if (GUILayout.Button("Browse...", Styles.BrowseButtonStyle))
                {
                    GUIUtility.hotControl = 0;
                    DragAndDrop.activeControlID = 0;
                    GUIUtility.keyboardControl = 0;

                    var path = UnityEditorUtility.OpenFolderPanel(title, folder, defaultName);
                    if (!string.IsNullOrEmpty(path))
                    {
                        folderPath = path;
                        GUI.changed = true;
                    }
                }

                return folderPath;
            }
        }

        /// <summary>
        /// The collection of GUI styles.
        /// </summary>
        private static class Styles
        {
            /// <summary>
            /// The style of browse button.
            /// </summary>
            public static readonly GUIStyle BrowseButtonStyle = new GUIStyle(EditorStyles.miniButton)
            {
                fixedWidth = 75,
                fixedHeight = EditorStyles.miniButtonRight.fixedHeight
            };
        }
    }
}