// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace UniSharper.Rendering
{
    /// <summary>
    /// The <see cref="LightmapRendererInfo"/> store data information about lightmap.
    /// </summary>
    [Serializable]
    public class LightmapRendererInfo
    {
        [SerializeField]
        private int lightmapIndex;

        [SerializeField]
        private Vector4 lightmapScaleOffset;

        [SerializeField]
        private Renderer renderer;

        /// <summary>
        /// Gets or sets the index of the lightmap.
        /// </summary>
        /// <value>The index of the lightmap.</value>
        public int LightmapIndex
        {
            get => lightmapIndex;
            set => lightmapIndex = value;
        }

        /// <summary>
        /// Gets or sets the lightmap scale offset.
        /// </summary>
        /// <value>The lightmap scale offset.</value>
        public Vector4 LightmapScaleOffset
        {
            get => lightmapScaleOffset;
            set => lightmapScaleOffset = value;
        }

        /// <summary>
        /// Gets or sets the renderer.
        /// </summary>
        /// <value>The renderer.</value>
        public Renderer Renderer
        {
            get => renderer;
            set => renderer = value;
        }
    }

    /// <summary>
    /// The class <see cref="PrefabLightmapData"/> provides lightmap data of prefab storing and reverting.
    /// </summary>
    /// <seealso cref="MonoBehaviour"/>
    [DisallowMultipleComponent, ExecuteInEditMode]
    public class PrefabLightmapData : MonoBehaviour
    {
        [SerializeField]
        private Texture2D[] lightmapColors;

        [SerializeField]
        private Texture2D[] lightmapDirections;

        [SerializeField]
        private LightmapRendererInfo[] rendererInfos;

        [SerializeField]
        private Texture2D[] shadowMasks;

        /// <summary>
        /// Sets an array of color of incoming light.
        /// </summary>
        /// <value>An array of color of incoming light.</value>
        public Texture2D[] LightmapColors
        {
            set => lightmapColors = value;
        }

        /// <summary>
        /// Sets an array of dominant direction of incoming light.
        /// </summary>
        /// <value>An array of dominant direction of incoming light.</value>
        public Texture2D[] LightmapDirections
        {
            set => lightmapDirections = value;
        }

        /// <summary>
        /// Sets the renderer information collection.
        /// </summary>
        /// <value>The renderer information collection.</value>
        public LightmapRendererInfo[] RendererInfos
        {
            set => rendererInfos = value;
        }

        /// <summary>
        /// Sets an array of occlusion mask per light.
        /// </summary>
        /// <value>An array of occlusion mask per light.</value>
        public Texture2D[] ShadowMasks
        {
            set => shadowMasks = value;
        }

        /// <summary>
        /// Applies the static lightmap.
        /// </summary>
        /// <param name="instance">The instance of <see cref="PrefabLightmapData"/>.</param>
        /// <exception cref="ArgumentNullException"><c>instance</c> is <c>null</c>.</exception>
        public static void ApplyStaticLightmap(PrefabLightmapData instance)
        {
            if (!instance)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var rendererInfos = instance.rendererInfos;

            if (rendererInfos == null || rendererInfos.Length == 0)
            {
                return;
            }

            var lightmapColors = instance.lightmapColors;
            var lightmapDirs = instance.lightmapDirections;
            var shadowMasks = instance.shadowMasks;

            var lightmaps = LightmapSettings.lightmaps;
            var combinedLightmaps = new LightmapData[lightmaps.Length + lightmapColors.Length];
            lightmaps.CopyTo(combinedLightmaps, 0);

            var storedLightmaps = new LightmapData[lightmapColors.Length];

            for (int i = 0, length = lightmapColors.Length; i < length; i++)
            {
                var data = new LightmapData
                {
                    lightmapColor = lightmapColors[i],
                    lightmapDir = lightmapDirs[i],
                    shadowMask = shadowMasks[i]
                };
                storedLightmaps[i] = data;
            }

            storedLightmaps.CopyTo(combinedLightmaps, lightmaps.Length);

            ApplyStaticLightmap(rendererInfos, lightmaps.Length);
            LightmapSettings.lightmaps = combinedLightmaps;
        }

        /// <summary>
        /// Applies the static lightmap for this prefab.
        /// </summary>
        /// <param name="infos">
        /// The <see cref="Array"/> of <see cref="LightmapRendererInfo"/> stored lightmap renderer information collection.
        /// </param>
        /// <param name="lightmapOffsetIndex">Index of the lightmap offset.</param>
        private static void ApplyStaticLightmap(LightmapRendererInfo[] infos, int lightmapOffsetIndex)
        {
            for (int i = 0, length = infos.Length; i < length; i++)
            {
                var info = infos[i];
                info.Renderer.lightmapIndex = info.LightmapIndex + lightmapOffsetIndex;
                info.Renderer.lightmapScaleOffset = info.LightmapScaleOffset;
            }
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            ApplyStaticLightmap(this);
        }
    }
}