// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using UnityEngine;

namespace UniSharper.Rendering
{
    /// <summary>
    /// The <see cref="PrefabLightmapExcludedRenderer"/> to define the excluded GameObject that no
    /// need to store lightmap data.
    /// </summary>
    /// <seealso cref="MonoBehaviour"/>
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class PrefabLightmapExcludedRenderer : MonoBehaviour
    {
    }
}