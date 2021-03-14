// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using UnityEditor.Compilation;
using GenericAssembely = System.Reflection.Assembly;

namespace UniSharperEditor.Compilation
{
    /// <summary>
    /// Utility methods and properties for script compilation pipeline.
    /// </summary>
    public static class CompilationPipelineUtility
    {
        #region Methods

        /// <summary>
        /// Gets the type of the editor.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <returns>The type with the specified name, if found; otherwise, <c>null</c>.</returns>
        public static Type GetEditorType(string typeFullName) => GetType(AssembliesType.Editor, typeFullName);

        /// <summary>
        /// Gets the type of the player.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <returns>The type with the specified name, if found; otherwise, <c>null</c>.</returns>
        public static Type GetPlayerType(string typeFullName) => GetType(AssembliesType.Player, typeFullName);

        /// <summary>
        /// Gets the type of player or editor.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <returns>The type with the specified name, if found; otherwise, <c>null</c>.</returns>
        public static Type GetType(string typeFullName)
        {
            var type = GetPlayerType(typeFullName);

            if (type == null)
            {
                type = GetEditorType(typeFullName);
            }

            return type;
        }

        private static Type GetType(AssembliesType assembliesType, string typeFullName)
        {
            var assemblies = CompilationPipeline.GetAssemblies(assembliesType);
            foreach (var assembly in assemblies)
            {
                var genericAssembely = GenericAssembely.LoadFile(assembly.outputPath);
                var type = genericAssembely.GetType(typeFullName);

                if (type != null)
                    return type;
            }

            return null;
        }

        #endregion Methods
    }
}