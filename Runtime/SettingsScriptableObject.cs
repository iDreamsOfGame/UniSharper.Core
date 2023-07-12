// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.IO;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// ScriptableObject class for settings. Implements the <see cref="UnityEngine.ScriptableObject"/>
    /// </summary>
    /// <typeparam name="T">The type of settings object. </typeparam>
    public abstract class SettingsScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
#if UNITY_EDITOR
        public static T Load(string path)
        {
            if (!string.IsNullOrEmpty(path))
                return default;

            return File.Exists(path) ? UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path) : default;
        }
        
        /// <summary>
        /// Save settings data.
        /// </summary>
        public virtual void Save()
        {
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif
    }
}