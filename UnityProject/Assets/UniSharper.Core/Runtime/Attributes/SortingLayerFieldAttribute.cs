// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Represents the sorting layer field that you select sorting layer name from Tags and Layers settings of Project in the Inspector window.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SortingLayerFieldAttribute : PropertyAttribute
    {
    }
}