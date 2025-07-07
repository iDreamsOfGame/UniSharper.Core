// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

#if UNITY_IOS
using System.IO;
using System.Linq;
using UniSharper;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

namespace UniSharperEditor.iOS.Xcode.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEditor.iOS.Xcode.PBXProject"/>.
    /// </summary>
    public static class PBXProjectExtensions
    {
        /// <summary>
        /// Adds a new directory reference to the list of known files as "Create groups".
        /// </summary>
        /// <param name="pbxProject"></param>
        /// <param name="realPath">The physical path to the folder on the filesystem. </param>
        /// <param name="projectPath">The project path to the folder. </param>
        /// <param name="filesExclusive">The exclusive files list. </param>
        /// <param name="dirsExclusive">The exclusive directories list. </param>
        public static void CreateGroup(this PBXProject pbxProject,
            string realPath,
            string projectPath,
            string[] filesExclusive = null,
            string[] dirsExclusive = null)
        {
            var targetGuid = pbxProject.GetUnityMainTargetGuid();

            // Adds file references. 
            var files = Directory.GetFiles(realPath);
            foreach (var file in files)
            {
                if (file.EndsWith(FileExtensions.MacOSXDesktopServicesStoreFile))
                    continue;

                var fileName = Path.GetFileName(file);
                if (filesExclusive != null && filesExclusive.Contains(fileName))
                    continue;

                var projectFilePath = Path.Combine(projectPath, fileName);
                var fileGuid = pbxProject.AddFile(projectFilePath, projectFilePath);
                pbxProject.AddFileToBuild(targetGuid, fileGuid);
            }

            // Adds subdirectory references.
            var subDirRealPaths = Directory.GetDirectories(realPath);
            foreach (var subDirRealPath in subDirRealPaths)
            {
                var dirName = Path.GetFileName(subDirRealPath);
                if (dirsExclusive != null && dirsExclusive.Contains(dirName))
                    continue;

                var subDirProjectPath = Path.Combine(projectPath, dirName);
                if (subDirRealPath.EndsWith(FileExtensions.XcodeAssetsFile))
                {
                    var fileGuid = pbxProject.AddFile(subDirProjectPath, subDirProjectPath);
                    pbxProject.AddFileToBuild(targetGuid, fileGuid);
                }
                else if (subDirRealPath.EndsWith(FileExtensions.XcodeUniversalFrameworkFile) || subDirRealPath.EndsWith(FileExtensions.XcodeFrameworkFile))
                {
                    var fileGuid = pbxProject.AddFile(subDirRealPath, subDirProjectPath);
                    pbxProject.AddFileToEmbedFrameworks(targetGuid, fileGuid);
                }
                else
                {
                    pbxProject.CreateGroup(subDirRealPath, subDirProjectPath, filesExclusive, dirsExclusive);
                }
            }
        }
        
        /// <summary>
        /// Removes a group reference from the Xcode project.
        /// </summary>
        /// <param name="pbxProject"></param>
        /// <param name="realPath">The physical path to the folder on the filesystem of the group. </param>
        public static void RemoveGroup(this PBXProject pbxProject, string realPath)
        {
            var targetGuid = pbxProject.GetUnityMainTargetGuid();
            
            // 移除文件引用
            var files = Directory.GetFiles(realPath);
            foreach (var file in files)
            {
                if (file.EndsWith(FileExtensions.MacOSXDesktopServicesStoreFile))
                    continue;
                
                var fileGuid = pbxProject.FindFileGuidByRealPath(file);
                if (string.IsNullOrEmpty(fileGuid))
                    continue;
                
                pbxProject.RemoveFile(fileGuid);
                pbxProject.RemoveFileFromBuild(targetGuid, fileGuid);
            }
            
            // 移除文件夹引用
            var subDirRealPaths = Directory.GetDirectories(realPath);
            foreach (var subDirRealPath in subDirRealPaths)
            {
                if (subDirRealPath.EndsWith(FileExtensions.XcodeAssetsFile))
                {
                    var fileGuid = pbxProject.FindFileGuidByRealPath(subDirRealPath);
                    if (string.IsNullOrEmpty(fileGuid)) 
                        continue;
                    
                    pbxProject.RemoveFile(fileGuid);
                    pbxProject.RemoveFileFromBuild(targetGuid, fileGuid);
                }
                else if (subDirRealPath.EndsWith(FileExtensions.XcodeUniversalFrameworkFile) || subDirRealPath.EndsWith(FileExtensions.XcodeFrameworkFile))
                {
                    var fileGuid = pbxProject.FindFileGuidByRealPath(subDirRealPath);
                    if (!string.IsNullOrEmpty(fileGuid)) 
                        pbxProject.RemoveFrameworkFromProject(targetGuid, fileGuid);
                }
                else
                {
                    pbxProject.RemoveGroup(subDirRealPath);
                }
            }
        }
    }
}
#endif