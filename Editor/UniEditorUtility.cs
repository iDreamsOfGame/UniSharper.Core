// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Reflection;
using ReSharp.Extensions;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UniSharperEditor
{
    /// <summary>
    /// Editor utility functions.
    /// </summary>
    public static class UniEditorUtility
    {
        /// <summary>
        /// Shows the folder content in Project window.
        /// </summary>
        /// <param name="directoryPath">The directory path relative to the project folder. </param>
        public static void ShowFolderContents(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
                return;
            
            if (directoryPath[directoryPath.Length - 1] == '/')
                directoryPath = directoryPath.Substring(0, directoryPath.Length - 1);
            
            EditorUtility.FocusProjectWindow();
            var dirAsset = AssetDatabase.LoadAssetAtPath(directoryPath, typeof(Object));
            var type = Type.GetType("UnityEditor.ProjectBrowser,UnityEditor");
            var fieldValue = type?.GetField("s_LastInteractedProjectBrowser", BindingFlags.Static | BindingFlags.Public)?.GetValue(null);
            var methodInfo = type?.GetMethod("ShowFolderContents", BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo?.Invoke(fieldValue, new object[] { dirAsset.GetInstanceID(), true });
        }
        
        /// <summary>
        /// Clears all the console information.
        /// </summary>
        public static void ClearConsole()
        {
            Assembly.Load("UnityEditor")?.GetType("UnityEditor.LogEntries")?.InvokeStaticMethod("Clear");
        }
    }
}