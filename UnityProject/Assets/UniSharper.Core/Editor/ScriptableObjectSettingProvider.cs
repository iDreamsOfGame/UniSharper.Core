// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// <see cref="ScriptableObjectSettingProvider"/> is the configuration class
    /// that specifies how a Project setting or a preference should appear in the Settings or Preferences window.
    /// </summary>
    public abstract class ScriptableObjectSettingProvider : SettingsProvider
    {
        private const int Margin = 10;
        
        protected ScriptableObjectSettingProvider(ScriptableObject scriptableObject,
            string path,
            SettingsScope scopes,
            IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
            ScriptableObject = scriptableObject;

            if (ScriptableObject)
            {
                ScriptableObjectEditor = Editor.CreateEditor(ScriptableObject);
                
                guiHandler = _ =>
                {
                    if (!ScriptableObjectEditor)
                        return;

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Space(Margin);
                        using (new EditorGUILayout.VerticalScope())
                        {
                            GUILayout.Space(Margin);
                            ScriptableObjectEditor.OnInspectorGUI();
                            GUILayout.Space(Margin);
                        }
                        GUILayout.Space(Margin);
                    }
                };
            }
        }
        
        /// <summary>
        /// The <see cref="ScriptableObject"/>.
        /// </summary>
        public ScriptableObject ScriptableObject { get; }

        /// <summary>
        /// The <see cref="Editor"/> of the <see cref="ScriptableObject"/>.
        /// </summary>
        public Editor ScriptableObjectEditor { get; }
    }
}