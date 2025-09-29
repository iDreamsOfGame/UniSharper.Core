﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Miscs
{
    /// <summary>
    /// Makes the <see cref="GameObject"/> not be destroyed automatically when loading a new scene.
    /// </summary>
    /// <seealso cref="MonoBehaviour"/>
    public sealed class DontDestroyOnLoad : MonoBehaviour
    {
        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            var cachedTransform = transform;
            DontDestroyOnLoad(cachedTransform.root == cachedTransform ? gameObject : cachedTransform.root.gameObject);
        }
    }
}