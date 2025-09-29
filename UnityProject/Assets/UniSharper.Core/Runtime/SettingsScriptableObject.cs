﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
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
        public static T Load(string path) => string.IsNullOrEmpty(path) || !File.Exists(path) ? default : UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);

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