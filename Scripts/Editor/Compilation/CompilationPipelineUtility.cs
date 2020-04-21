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

        public static Type GetEditorType(string typeFullName)
        {
            return GetType(AssembliesType.Editor, typeFullName);
        }

        public static Type GetPlayerType(string typeFullName)
        {
            return GetType(AssembliesType.Player, typeFullName);
        }

        public static Type GetType(string typeFullName)
        {
            Type type = GetPlayerType(typeFullName);

            if (type == null)
            {
                type = GetEditorType(typeFullName);
            }

            return type;
        }

        private static Type GetType(AssembliesType assembliesType, string typeFullName)
        {
            Assembly[] assemblies = CompilationPipeline.GetAssemblies(assembliesType);
            foreach (var assembly in assemblies)
            {
                GenericAssembely genericAssembely = GenericAssembely.LoadFile(assembly.outputPath);
                Type type = genericAssembely.GetType(typeFullName);

                if (type != null)
                    return type;
            }

            return null;
        }

        #endregion Methods
    }
}