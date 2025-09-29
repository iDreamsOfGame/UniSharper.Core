// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Represents the tag field that you select tag name from Tags and Layers settings of Project in the Inspector window.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class TagFieldAttribute : PropertyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagFieldAttribute"/> class.
        /// </summary>
        /// <param name="useDefaultTagFieldDrawer">Determine whether use the default tag filed drawer.</param>
        public TagFieldAttribute(bool useDefaultTagFieldDrawer = true)
        {
            UseDefaultTagFieldDrawer = useDefaultTagFieldDrawer;
        }
        
        /// <summary>
        /// Determine whether use the default tag filed drawer.
        /// </summary>
        public bool UseDefaultTagFieldDrawer { get; }
    }
}