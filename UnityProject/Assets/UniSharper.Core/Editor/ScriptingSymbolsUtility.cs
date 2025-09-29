// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ReSharp.Extensions;
using UnityEditor;
using UnityEditor.Build;

namespace UniSharperEditor
{
    /// <summary>
    /// Provides utilities to handle scripting symbols. 
    /// </summary>
    public static class ScriptingSymbolsUtility
    {
        /// <summary>
        /// The separator for separating defines.
        /// </summary>
        public const char DefinesSeparator = ';';
        
        /// <summary>
        /// Gets or sets the user-specified symbols for script compilation for the active build target.
        /// </summary>
        public static string ScriptingSymbolsForActiveBuildTarget
        {
            get => GetScriptingSymbols(EditorUserBuildSettings.activeBuildTarget);
            set => SetScriptingSymbols(EditorUserBuildSettings.activeBuildTarget, value);
        }
        
        /// <summary>
        /// Gets the list of the user-specified define for script compilation for the active build target.
        /// </summary>
        public static List<string> DefinesForActiveBuildTarget => GetDefines(EditorUserBuildSettings.activeBuildTarget);

        /// <summary>
        /// Gets the user-specified symbols for script compilation for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        /// <returns>The user-specified symbols for script compilation for the specific build target. </returns>
        public static string GetScriptingSymbols(BuildTarget buildTarget)
        {
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
#if UNITY_2021_2_OR_NEWER
            var nameBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
            var scriptingDefineSymbols = PlayerSettings.GetScriptingDefineSymbols(nameBuildTarget);
#else
            var scriptingDefineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
#endif
            return string.IsNullOrEmpty(scriptingDefineSymbols) ? string.Empty : scriptingDefineSymbols.TrimAll();
        }
        
        /// <summary>
        /// Sets the user-specified symbols for script compilation for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        /// <param name="value">The symbols for script compilation. </param>
        public static void SetScriptingSymbols(BuildTarget buildTarget, string value)
        {
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
#if UNITY_2021_2_OR_NEWER
            var nameBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
            PlayerSettings.SetScriptingDefineSymbols(nameBuildTarget, value);
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, value);
#endif
            AssetDatabase.SaveAssets();
        }
        
        /// <summary>
        /// Gets the list of the user-specified define for script compilation for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        /// <returns>The list of the user-specified define for script compilation for the specific build target. </returns>
        public static List<string> GetDefines(BuildTarget buildTarget)
        {
            var scriptingDefineSymbols = GetScriptingSymbols(buildTarget);
            var scriptingSymbols = string.IsNullOrEmpty(scriptingDefineSymbols) 
                ? Array.Empty<string>() 
                : scriptingDefineSymbols.Split(new[] { DefinesSeparator }, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(scriptingSymbols);
        }

        /// <summary>
        /// Determines whether the user-specified defines for script compilation contains a specific define for the active build target.
        /// </summary>
        /// <param name="define">The specific define to locate in the user-specified defines for script compilation. </param>
        /// <returns><c>true</c> if the specific define is found in the user-specified defines for script compilation; otherwise, <c>false</c>. </returns>
        public static bool ContainsDefine(string define) => ContainsDefine(EditorUserBuildSettings.activeBuildTarget, define);

        /// <summary>
        /// Determines whether the user-specified defines for script compilation contains a specific define for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        /// <param name="define">The specific define to locate in the user-specified defines for script compilation for the specific build target. </param>
        /// <returns><c>true</c> if the specific define is found in the user-specified defines for script compilation for the specific build target; otherwise, <c>false</c>. </returns>
        public static bool ContainsDefine(BuildTarget buildTarget, string define)
        {
            if (string.IsNullOrEmpty(define))
                return false;

            var targetDefine = define.TrimAll();
            var defines = GetDefines(buildTarget);
            return defines.Count > 0 && defines.Any(element => element.Equals(targetDefine));
        }

        /// <summary>
        /// Adds a specific define to the user-specified defines for script compilation for the active build target.
        /// </summary>
        /// <param name="define">The specific define to add to the user-specified defines for script compilation for the active build target. </param>
        public static void AddDefine(string define)
        {
            AddDefine(EditorUserBuildSettings.activeBuildTarget, define);
        }
        
        /// <summary>
        /// Adds a specific define to the user-specified defines for script compilation for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        /// <param name="define">The specific define to add to the user-specified defines for script compilation for the specific build target. </param>
        public static void AddDefine(BuildTarget buildTarget, string define)
        {
            if (string.IsNullOrEmpty(define))
                return;

            var defines = GetDefines(buildTarget);
            defines.AddUnique(define.TrimAll());
            SetScriptingSymbols(buildTarget, ToScriptingSymbols(defines));
        }

        /// <summary>
        /// Adds specific defines to the user-specified defines for script compilation for the active build target.
        /// </summary>
        /// <param name="define">The specific defines to add to the user-specified defines for script compilation for the active build target. </param>
        public static void AddDefines(IEnumerable<string> defines)
        {
            AddDefines(EditorUserBuildSettings.activeBuildTarget, defines);
        }

        /// <summary>
        /// Adds specific defines to the user-specified defines for script compilation for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        /// <param name="define">The specific defines to add to the user-specified defines for script compilation for the specific build target. </param>
        public static void AddDefines(BuildTarget buildTarget, IEnumerable<string> defines)
        {
            if (defines == null)
                return;

            foreach (var define in defines)
            {
                AddDefine(buildTarget, define);
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific define from the user-specified defines for script compilation for the active build target.
        /// </summary>
        /// <param name="define">The specific define to remove from the user-specified defines for script compilation for the active build target. </param>
        public static void RemoveDefine(string define)
        {
            RemoveDefine(EditorUserBuildSettings.activeBuildTarget, define);
        }

        /// <summary>
        /// Removes the first occurrence of a specific define from the user-specified defines for script compilation for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        /// <param name="define">The specific define to remove from the user-specified defines for script compilation for the specific build target. </param>
        public static void RemoveDefine(BuildTarget buildTarget, string define)
        {
            if (buildTarget == BuildTarget.NoTarget || string.IsNullOrEmpty(define))
                return;
            
            var defines = GetDefines(buildTarget);
            defines.Remove(define.TrimAll());
            SetScriptingSymbols(buildTarget, ToScriptingSymbols(defines));
        }

        /// <summary>
        /// Removes specific defines from the user-specified defines for script compilation for the active build target.
        /// </summary>
        /// <param name="defines">The specific defines to be removed from the user-specified defines for script compilation for the active build target. </param>
        public static void RemoveDefines(IEnumerable<string> defines)
        {
            RemoveDefines(EditorUserBuildSettings.activeBuildTarget, defines);
        }

        /// <summary>
        /// Removes specific defines from the user-specified defines for script compilation for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        /// <param name="defines">The specific defines to be removed from the user-specified defines for script compilation for the specific build target. </param>
        public static void RemoveDefines(BuildTarget buildTarget, IEnumerable<string> defines)
        {
            if (buildTarget == BuildTarget.NoTarget || defines == null)
                return;

            var scriptingDefines = GetDefines(buildTarget);
            foreach (var define in defines)
            {
                scriptingDefines.Remove(define.TrimAll());
            }
            SetScriptingSymbols(buildTarget, ToScriptingSymbols(scriptingDefines));
        }

        /// <summary>
        /// Clears all user-specified defines for script compilation for the active build target.
        /// </summary>
        public static void ClearAllDefines()
        {
            ClearAllDefines(EditorUserBuildSettings.activeBuildTarget);
        }

        /// <summary>
        /// Clears all user-specified defines for script compilation for the specific build target.
        /// </summary>
        /// <param name="buildTarget">The specific build target. </param>
        public static void ClearAllDefines(BuildTarget buildTarget)
        {
            if (buildTarget == BuildTarget.NoTarget)
                return;

            SetScriptingSymbols(buildTarget, string.Empty);
        }

        private static string ToScriptingSymbols(IReadOnlyCollection<string> defines) => 
            defines == null || defines.Count == 0 ? string.Empty : string.Join(DefinesSeparator.ToString(), defines);
    }
}