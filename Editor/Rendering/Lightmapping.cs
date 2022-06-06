// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Collections.Generic;
using UniSharper.Rendering;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Rendering
{
    /// <summary>
    /// The class <see cref="Lightmapping"/> provides menu items to bake lightmaps.
    /// </summary>
    [InitializeOnEditorStartup]
    internal static class Lightmapping
    {
        /// <summary>
        /// Initializes static members of the <see cref="Lightmapping"/> class.
        /// </summary>
        static Lightmapping()
        {
#if UNITY_2019_4_OR_NEWER
            UnityEditor.Lightmapping.bakeCompleted += OnLightmappingCompleted;
#else
            UnityEditor.Lightmapping.completed += OnLightmappingCompleted;
#endif
        }

        /// <summary>
        /// Bakes the prefab lightmaps.
        /// Note: Before building, you need to setup Shader stripping under the menu "Edit -&gt;
        /// Project Settings -&gt; Graphics", Set "Lightmap Modes" to "Manual' and uncheck "Realtime
        /// Non-Directional" and "Realtime Directional".
        /// </summary>
        [MenuItem("UniSharper/Rendering/Bake Prefab Lightmaps", false, 2)]
        private static void BakePrefabLightmaps()
        {
            if (UnityEditor.Lightmapping.giWorkflowMode != UnityEditor.Lightmapping.GIWorkflowMode.OnDemand)
            {
                const string errorMessage = "ExtractLightmapData requires that you have baked you lightmaps and Auto mode is disabled.";
                Debug.LogError(errorMessage);
                return;
            }

            var prefabs = Object.FindObjectsOfType<PrefabLightmapData>();
            MakeSureRendererGameObjectIsLightmapStatic(prefabs);

            // Bake lightmap for scene.
            UnityEditor.Lightmapping.Bake();
        }

        /// <summary>
        /// Bakes the prefab lightmaps.
        /// </summary>
        /// <param name="prefabs">The <see cref="System.Array"/> of <see cref="PrefabLightmapData"/>.</param>
        private static void BakePrefabLightmaps(PrefabLightmapData[] prefabs)
        {
            if (prefabs == null || prefabs.Length == 0) 
                return;
            
            for (int i = 0, length = prefabs.Length; i < length; i++)
            {
                var data = prefabs[i];
                var gameObject = data.gameObject;
                var rendererInfos = new List<LightmapRendererInfo>();
                var lightmapColors = new List<Texture2D>();
                var lightmapDirs = new List<Texture2D>();
                var shadowMasks = new List<Texture2D>();

                GenerateLightmapInfo(gameObject, rendererInfos, lightmapColors, lightmapDirs, shadowMasks);

                data.RendererInfos = rendererInfos.ToArray();
                data.LightmapColors = lightmapColors.ToArray();
                data.LightmapDirections = lightmapDirs.ToArray();
                data.ShadowMasks = shadowMasks.ToArray();

                // Save prefab.
                PrefabUtility.SavePrefabAsset(gameObject);

                // Apply lightmap.
                PrefabLightmapData.ApplyStaticLightmap(data);
            }
        }

        /// <summary>
        /// Generates the lightmap information.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/>.</param>
        /// <param name="rendererInfos">
        /// The <see cref="List{LightmapRendererInfo}"/> to store renderer information.
        /// </param>
        /// <param name="lightmapColors">The <see cref="List{Texture2D}"/> to store <see cref="LightmapData.lightmapColor"/>.</param>
        /// <param name="lightmapDirs">The <see cref="List{Texture2D}"/> to store <see cref="LightmapData.lightmapDir"/>.</param>
        /// <param name="shadowMasks">The <see cref="List{Texture2D}"/> to store <see cref="LightmapData.shadowMask"/>.</param>
        private static void GenerateLightmapInfo(GameObject gameObject, List<LightmapRendererInfo> rendererInfos, List<Texture2D> lightmapColors,
            List<Texture2D> lightmapDirs, List<Texture2D> shadowMasks)
        {
            var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

            foreach (var renderer in renderers)
            {
                var excludedRenderer = renderer.gameObject.GetComponent<PrefabLightmapExcludedRenderer>();

                if (excludedRenderer != null)
                {
                    continue;
                }

                var info = new LightmapRendererInfo
                {
                    Renderer = renderer,
                    LightmapScaleOffset = renderer.lightmapScaleOffset
                };

                var data = LightmapSettings.lightmaps[renderer.lightmapIndex];
                var lightmapColor = data.lightmapColor;
                var lightmapDir = data.lightmapDir;
                var shadowMask = data.shadowMask;

                info.LightmapIndex = lightmapColors.IndexOf(lightmapColor);

                if (info.LightmapIndex == -1)
                {
                    info.LightmapIndex = lightmapColors.Count;
                    lightmapColors.Add(lightmapColor);
                    lightmapDirs.Add(lightmapDir);
                    shadowMasks.Add(shadowMask);
                }

                rendererInfos.Add(info);
            }
        }

        /// <summary>
        /// Make sure the <see cref="GameObject"/> of renderer is lightmap static.
        /// </summary>
        /// <param name="prefabs">The <see cref="System.Array"/> of <see cref="PrefabLightmapData"/>.</param>
        private static void MakeSureRendererGameObjectIsLightmapStatic(PrefabLightmapData[] prefabs)
        {
            if (prefabs == null || prefabs.Length == 0)
                return;
            
            foreach (var lightmap in prefabs)
            {
                var renderers = lightmap.gameObject.GetComponentsInChildren<MeshRenderer>();

                foreach (var renderer in renderers)
                {
                    var gameObject = renderer.gameObject;
                    var excludedRenderer = gameObject.GetComponent<PrefabLightmapExcludedRenderer>();
                    if (excludedRenderer != null) 
                        continue;
                        
                    if (!GameObjectUtility.AreStaticEditorFlagsSet(gameObject, StaticEditorFlags.ContributeGI))
                    {
                        GameObjectUtility.SetStaticEditorFlags(gameObject, StaticEditorFlags.ContributeGI);
                    }
                }
            }
        }

        /// <summary>
        /// Called when lightmap generating completed.
        /// </summary>
        private static void OnLightmappingCompleted()
        {
            var prefabs = Object.FindObjectsOfType<PrefabLightmapData>();
            BakePrefabLightmaps(prefabs);
        }

        /// <summary>
        /// Validates the prefab lightmaps baking.
        /// </summary>
        /// <returns>
        /// <c>true</c> if got <see cref="PrefabLightmapData"/> component in open scenes,
        /// <c>false</c> otherwise.
        /// </returns>
        [MenuItem("UniSharper/Rendering/Bake Prefab Lightmaps", true)]
        private static bool ValidatePrefabLightmapsBaking()
        {
            var prefabs = Object.FindObjectsOfType<PrefabLightmapData>();
            if (prefabs == null || prefabs.Length == 0) 
                return false;
            
            foreach (var item in prefabs)
            {
                var root = PrefabUtility.GetCorrespondingObjectFromSource(item.gameObject);

                if (root != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}