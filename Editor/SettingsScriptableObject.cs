// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using UnityEditor;
using UnityEngine;
using UnityEditorUtility = UnityEditor.EditorUtility;

namespace UniSharperEditor
{
    /// <summary>
    /// ScriptableObject class for settings. Implements the <see cref="UnityEngine.ScriptableObject"/>
    /// </summary>
    /// <seealso cref="UnityEngine.ScriptableObject"/>
    public abstract class SettingsScriptableObject : ScriptableObject
    {
        /// <summary>
        /// Save settings data.
        /// </summary>
        public virtual void Save()
        {
            UnityEditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}