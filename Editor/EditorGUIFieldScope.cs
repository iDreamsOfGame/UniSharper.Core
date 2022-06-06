// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using UnityEditor;

namespace UniSharperEditor
{
    /// <summary>
    /// Create a field scope on the editor GUI Layer. Implements the <see cref="System.IDisposable"/>
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    public readonly struct EditorGUIFieldScope : IDisposable
    {
        private readonly float cachedLabelWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorGUIFieldScope"/> class.
        /// </summary>
        /// <param name="labelWidth">Width of the label.</param>
        public EditorGUIFieldScope(float labelWidth = 0)
        {
            cachedLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUILayout.BeginHorizontal();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            CloseScope();
        }

        private void CloseScope()
        {
            EditorGUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = cachedLabelWidth;
        }
    }
}