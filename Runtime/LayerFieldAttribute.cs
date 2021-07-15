// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Represents the layer field that you select layer name from Tags and Layers settings of Project in the Inspector window.
    /// </summary>
    /// <seealso cref="PropertyAttribute"/>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LayerFieldAttribute : PropertyAttribute
    {
        /// <summary>
        /// Determine whether use the default layer filed drawer.
        /// </summary>
        public bool UseDefaultLayerFieldDrawer { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="useDefaultTagFieldDrawer">Determine whether use the default layer filed drawer.</param>
        public LayerFieldAttribute(bool useDefaultLayerFieldDrawer = true)
        {
            UseDefaultLayerFieldDrawer = useDefaultLayerFieldDrawer;
        }
    }
}