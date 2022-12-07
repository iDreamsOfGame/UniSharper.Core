// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Represents the enumeration property with Flags attribute declaration.
    /// </summary>
    /// <seealso cref="PropertyAttribute"/>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FlagsEnumFieldAttribute : PropertyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagsEnumFieldAttribute"/> class.
        /// </summary>
        public FlagsEnumFieldAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagsEnumFieldAttribute"/> class.
        /// </summary>
        /// <param name="label">The caption/label for the attribute.</param>
        public FlagsEnumFieldAttribute(string label)
            : this()
        {
            Label = label;
        }

        /// <summary>
        /// Gets the caption/label for the attribute.
        /// </summary>
        /// <value>The caption/label for the attribute.</value>
        public string Label { get; }
    }
}