// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Represents the readonly field that you can not change the value of field in the Inspector window.
    /// </summary>
    /// <seealso cref="PropertyAttribute"/>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ReadOnlyFieldAttribute : PropertyAttribute
    {
    }
}