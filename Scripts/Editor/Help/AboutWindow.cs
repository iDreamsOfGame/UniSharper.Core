// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

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
        #region Fields

        /// <summary>
        /// The menu item priority.
        /// </summary>
        public const int MenuItemPriority = int.MaxValue;

        #endregion Fields

        #region Methods

        [MenuItem("UniSharper/Help/About UniSharper...", false, 1000)]
        private static void ShowAboutWindow()
        {
            AboutWindow windowWithRect = GetWindowWithRect<AboutWindow>(new Rect(100f, 100f, 230f, 110f), true, "About UniSharper");
        }

        private void OnGUI()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.Space(20f);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            GUILayout.Label("UniSharper", new GUIStyle() { fontStyle = FontStyle.Bold, fontSize = 30, normal = new GUIStyleState() { textColor = Color.white } });
            GUILayout.Space(10f);
            GUILayout.Label("Copyright (c) 2020 Jerry Lee");
            GUILayout.Label("cosmos53076@163.com");
            GUILayout.EndVertical();
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }

        #endregion Methods
    }
}