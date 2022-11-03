// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditorUtility = UnityEditor.EditorUtility;

namespace UniSharperEditor.Rendering
{
    /// <summary>
    /// Displays the window for generating lightmap for adding scenes.
    /// </summary>
    /// <seealso cref="EditorWindow"/>
    internal class LightingGenerationWindow : EditorWindow
    {
        private readonly List<SceneAsset> scenes = new();

        private Vector2 scrollPosition;

        [MenuItem("UniSharper/Rendering/Generate Lighting for Scenes", false, 1)]
        public static void ShowWindow()
        {
            const string title = "Lighting Generation";
            GetWindowWithRect<LightingGenerationWindow>(new Rect(200, 200, 500, 500), true, title, true);
        }

        private void GenerateLighting()
        {
            const string progressBarTitle = "Baking...";
            var scenePaths = new List<string>();

            foreach (var sceneAsset in scenes)
            {
                var scenePath = AssetDatabase.GetAssetPath(sceneAsset);

                if (!string.IsNullOrEmpty(scenePath))
                {
                    scenePaths.Add(scenePath);
                }
            }

            for (int i = 0, length = scenePaths.Count; i < length; i++)
            {
                var scenePath = scenePaths[i];
                var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                UnityEditorUtility.DisplayProgressBar(progressBarTitle, $"Baking the scene {scene.name}... {i + 1}/{length}", (float)(i + 1) / length);
                UnityEditor.Lightmapping.Bake();
                EditorSceneManager.SaveScene(scene);
            }

            UnityEditorUtility.ClearProgressBar();
        }

        private void OnGUI()
        {
            const string labelText = "Scenes to generate lighting:";
            const string addButtonText = "Add";
            const string generateButtonText = "Generate Lighting for Scenes";

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label(labelText, EditorStyles.boldLabel);
            for (var i = 0; i < scenes.Count; ++i)
            {
                scenes[i] = (SceneAsset)EditorGUILayout.ObjectField(scenes[i], typeof(SceneAsset), false);
            }
            if (GUILayout.Button(addButtonText))
            {
                scenes.Add(null);
                GUIUtility.ExitGUI();
            }

            GUILayout.Space(8);

            if (GUILayout.Button(generateButtonText))
            {
                GenerateLighting();
                GUIUtility.ExitGUI();
            }

            GUILayout.EndScrollView();
        }
    }
}