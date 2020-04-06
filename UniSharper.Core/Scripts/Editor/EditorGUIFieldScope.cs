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
    public struct EditorGUIFieldScope : IDisposable
    {
        #region Fields

        private float cachedLabelWidth;

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}