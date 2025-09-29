// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Help
{
    /// <summary>
    /// Displays the about information.
    /// </summary>
    /// <seealso cref="EditorWindow"/>
    internal class AboutWindow : EditorWindow
    {
        /// <summary>
        /// The menu item priority.
        /// </summary>
        public const int MenuItemPriority = int.MaxValue;

        [MenuItem("UniSharper/Help/About UniSharper...", false, 1000)]
        private static void ShowAboutWindow()
        {
            const string title = "About UniSharper";
            GetWindowWithRect<AboutWindow>(new Rect(100f, 100f, 230f, 110f), true, title);
        }

        private void OnGUI()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Space(20f);
            GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
            GUILayout.Label("UniSharper", new GUIStyle { fontStyle = FontStyle.Bold, fontSize = 30, normal = new GUIStyleState { textColor = Color.white } });
            GUILayout.Space(10f);
            GUILayout.Label("Copyright (c) 2021 Jerry Lee");
            GUILayout.Label("cosmos53076@163.com");
            GUILayout.EndVertical();
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }
    }
}