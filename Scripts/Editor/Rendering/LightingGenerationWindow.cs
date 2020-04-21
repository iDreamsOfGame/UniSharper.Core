// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditorUtility = UnityEditor.EditorUtility;

namespace UniSharperEditor.Rendering
{
    /// <summary>
    /// Displays the window for generating lightmap for adding scenes.
    /// </summary>
    /// <seealso cref="EditorWindow"/>
    internal class LightingGenerationWindow : EditorWindow
    {
        #region Fields

        private List<SceneAsset> scenes = new List<SceneAsset>();
        private Vector2 scrollPosition;

        #endregion Fields

        #region Methods

        [MenuItem("UniSharper/Rendering/Generate Lighting for Scenes", false, 1)]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            LightingGenerationWindow window = GetWindow<LightingGenerationWindow>(true, "Lighting Generation", true);
            window.position = new Rect(200, 200, 500, 500);
        }

        private void GenerateLighting()
        {
            List<string> scenePaths = new List<string>();

            foreach (var sceneAsset in scenes)
            {
                string scenePath = AssetDatabase.GetAssetPath(sceneAsset);

                if (!string.IsNullOrEmpty(scenePath))
                {
                    scenePaths.Add(scenePath);
                }
            }

            for (int i = 0, length = scenePaths.Count; i < length; i++)
            {
                string scenePath = scenePaths[i];
                Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                UnityEditorUtility.DisplayProgressBar("Baking...", string.Format("Baking the scene {0}... {1}/{2}", scene.name, i + 1, length), (float)(i + 1) / length);
                UnityEditor.Lightmapping.Bake();
                EditorSceneManager.SaveScene(scene);
            }

            UnityEditorUtility.ClearProgressBar();
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label("Scenes to generate lighting:", EditorStyles.boldLabel);
            for (int i = 0; i < scenes.Count; ++i)
            {
                scenes[i] = (SceneAsset)EditorGUILayout.ObjectField(scenes[i], typeof(SceneAsset), false);
            }
            if (GUILayout.Button("Add"))
            {
                scenes.Add(null);
            }

            GUILayout.Space(8);

            if (GUILayout.Button("Generate Lighting for Scenes"))
            {
                GenerateLighting();
            }

            GUILayout.EndScrollView();
        }

        #endregion Methods
    }
}