// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Linq;
using UnityEditor.Compilation;
using GenericAssembly = System.Reflection.Assembly;

namespace UniSharperEditor.Compilation
{
    /// <summary>
    /// Utility methods and properties for script compilation pipeline.
    /// </summary>
    public static class CompilationPipelineUtility
    {
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
        public static Type GetType(string typeFullName) => GetPlayerType(typeFullName) ?? GetEditorType(typeFullName);

        private static Type GetType(AssembliesType assembliesType, string typeFullName)
        {
            var assemblies = CompilationPipeline.GetAssemblies(assembliesType);
            return assemblies.Select(assembly => GenericAssembly.LoadFile(assembly.outputPath))
                .Select(genericAssembly => genericAssembly.GetType(typeFullName))
                .FirstOrDefault(type => type != null);
        }
    }
}