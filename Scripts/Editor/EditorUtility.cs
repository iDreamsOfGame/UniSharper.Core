// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Reflection;

namespace UniSharperEditor
{
    /// <summary>
    /// Editor utility functions.
    /// </summary>
    public static class EditorUtility
    {
        #region Methods

        /// <summary>
        /// Clears all the console information.
        /// </summary>
        public static void ClearConsole()
        {
            Assembly.Load("UnityEditor").GetType("UnityEditor.LogEntries").InvokeStaticMethod("Clear");
        }

        #endregion Methods
    }
}