// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Provides utilities to handle build operation. 
    /// </summary>
    public static class BuildUtility
    {
        /// <summary>
        /// Gets or sets the version code of application.
        /// </summary>
        [NotNull]
        public static string VersionCode
        {
            get
            {
                switch (EditorUserBuildSettings.activeBuildTarget)
                {
                    case BuildTarget.Android:
                        return PlayerSettings.Android.bundleVersionCode.ToString();
                    
                    case BuildTarget.iOS:
                        return PlayerSettings.iOS.buildNumber;
                    
                    default:
                        return "0";
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                
                switch (EditorUserBuildSettings.activeBuildTarget)
                {
                    case BuildTarget.Android:
                        int.TryParse(value, out var versionCode);
                        PlayerSettings.Android.bundleVersionCode = versionCode;
                        break;

                    case BuildTarget.iOS:
                        PlayerSettings.iOS.buildNumber = value;
                        break;
                }

                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>
        /// Switch active build target to Android.
        /// </summary>
        public static void SwitchActiveBuildTargetToAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                return;
            
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        }
        
        /// <summary>
        /// Switch active build target to iOS.
        /// </summary>
        public static void SwitchActiveBuildTargetToIOS()
        {
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
                return;
            
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
        }
        
        /// <summary>
        /// Performs generic build.
        /// </summary>
        /// <param name="buildPath">The build path.</param>
        /// <param name="buildOutputName">Name of the build output.</param>
        /// <param name="buildTarget">The build target.</param>
        /// <param name="isDevelopomentBuild">if set to <c>true</c> [is development build].</param>
        /// <param name="cleanBuild">if set to <c>true</c> clean all build cache.</param>
        /// <exception cref="System.Exception">Build failure .</exception>
        public static void PerformGenericBuild(string buildPath, 
            string buildOutputName, 
            BuildTarget buildTarget, 
            bool isDevelopmentBuild = false, 
            bool cleanBuild = false)
        {
            EditorUserBuildSettings.development = isDevelopmentBuild;
            EditorUserBuildSettings.allowDebugging = isDevelopmentBuild;
            EditorUserBuildSettings.compressFilesInPackage = true;
            
            var buildFolderFullPath = Path.Combine(Directory.GetCurrentDirectory(), buildPath);

            // Create output directory.
            if (!Directory.Exists(buildFolderFullPath))
                Directory.CreateDirectory(buildFolderFullPath);

            var locationPathName = Path.Combine(buildFolderFullPath, buildOutputName);
            var additionalOptions = isDevelopmentBuild ?
                BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.CompressWithLz4
                : BuildOptions.CompressWithLz4HC;

            // Clea build cache.
            if (cleanBuild)
            {
                FileUtil.DeleteFileOrDirectory(buildFolderFullPath);
                if (!Directory.Exists(buildFolderFullPath))
                    Directory.CreateDirectory(buildFolderFullPath);

#if UNITY_2021_2_OR_NEWER
                additionalOptions |= BuildOptions.CleanBuildCache;
#endif
            }

            if (buildTarget == BuildTarget.iOS)
            {
                // Xcode incremental compile.
                var buildFolderDireInfo = new DirectoryInfo(buildFolderFullPath);
                var fileInfos = buildFolderDireInfo.GetFiles();
                var projectExists = fileInfos.Any(fileInfo => fileInfo.Name == "Info.plist");
                if(projectExists)
                    additionalOptions |= BuildOptions.AcceptExternalModificationsToPlayer;

#if UNITY_2021_2_OR_NEWER
                if (isDevelopmentBuild)
                {
                    additionalOptions |= BuildOptions.SymlinkSources;
                    EditorUserBuildSettings.symlinkSources = true;
                }
                else
                {
                    EditorUserBuildSettings.symlinkSources = false;
                }
#endif
            }

            var result = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, locationPathName, buildTarget, additionalOptions);

            if (result.summary.totalErrors > 0)
            {
                throw new Exception($"Build failure: {result}");
            }
            else
            {
                Debug.Log("Build success!");
            }
        }
    }
}