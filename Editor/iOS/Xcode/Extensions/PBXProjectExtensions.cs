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
        /// Adds a new directory reference to the list of known files as "Create groups" for UnityMain target.
        /// </summary>
        /// <param name="pbxProject">The Xcode project. </param>
        /// <param name="realPath">The physical path to the folder on the filesystem. </param>
        /// <param name="rootProjectPath">The root project path of the group to be created. </param>
        /// <param name="filesExclusive">The exclusive files list. </param>
        /// <param name="dirsExclusive">The exclusive directories list. </param>
        public static void CreateGroup(this PBXProject pbxProject,
            string realPath,
            string rootProjectPath,
            string[] filesExclusive = null,
            string[] dirsExclusive = null)
        {
            var targetGuid = pbxProject.GetUnityMainTargetGuid();
            pbxProject.CreateGroup(targetGuid, realPath, rootProjectPath, filesExclusive, dirsExclusive);
        }
        
        /// <summary>
        /// Adds a new directory reference to the list of known files as "Create groups" for the specified target.
        /// </summary>
        /// <param name="pbxProject">The Xcode project. </param>
        /// <param name="targetGuid">The GUID of the target as returned by <see cref="PBXProject.TargetGuidByName"/>. </param>
        /// <param name="realPath">The physical path to the folder on the filesystem. </param>
        /// <param name="rootProjectPath">The root project path of the group to be created. </param>
        /// <param name="filesExclusive">The exclusive files list. </param>
        /// <param name="dirsExclusive">The exclusive directories list. </param>
        public static void CreateGroup(this PBXProject pbxProject,
            string targetGuid,
            string realPath,
            string rootProjectPath,
            string[] filesExclusive = null,
            string[] dirsExclusive = null)
        {
            // Adds file references. 
            var files = Directory.GetFiles(realPath);
            foreach (var file in files)
            {
                if (file.EndsWith(FileExtensions.MacOSXDesktopServicesStoreFile))
                    continue;

                var fileName = Path.GetFileName(file);
                if (filesExclusive != null && filesExclusive.Contains(fileName))
                    continue;

                var fileProjectPath = Path.Combine(rootProjectPath, fileName);
                var fileGuid = pbxProject.AddFile(fileProjectPath, fileProjectPath);
                pbxProject.AddFileToBuild(targetGuid, fileGuid);
            }

            // Adds subdirectory references.
            var subDirRealPaths = Directory.GetDirectories(realPath);
            foreach (var subDirRealPath in subDirRealPaths)
            {
                var dirName = Path.GetFileName(subDirRealPath);
                if (dirsExclusive != null && dirsExclusive.Contains(dirName))
                    continue;

                var subDirProjectPath = Path.Combine(rootProjectPath, dirName);
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
                    pbxProject.CreateGroup(targetGuid, subDirRealPath, subDirProjectPath, filesExclusive, dirsExclusive);
                }
            }
        }

        /// <summary>
        /// Removes a group reference from the UnityMain target in Xcode project.
        /// </summary>
        /// <param name="pbxProject">The Xcode project. </param>
        /// <param name="realPath">The physical path to the folder on the filesystem of the group. </param>
        public static void RemoveGroupByRealPath(this PBXProject pbxProject, string realPath)
        {
            var targetGuid = pbxProject.GetUnityMainTargetGuid();
            pbxProject.RemoveGroupByRealPath(targetGuid, realPath);
        }
        
        /// <summary>
        /// Removes a group reference from the specified target in Xcode project by real path.
        /// </summary>
        /// <param name="pbxProject">The Xcode project. </param>
        /// <param name="targetGuid">The GUID of the target as returned by <see cref="PBXProject.TargetGuidByName"/>. </param>
        /// <param name="realPath">The physical path to the folder on the filesystem of the group. </param>
        public static void RemoveGroupByRealPath(this PBXProject pbxProject, string targetGuid, string realPath)
        {
            // 移除文件引用
            var files = Directory.GetFiles(realPath);
            foreach (var file in files)
            {
                if (file.EndsWith(FileExtensions.MacOSXDesktopServicesStoreFile))
                    continue;
                
                var fileGuid = pbxProject.FindFileGuidByRealPath(file);
                if (string.IsNullOrEmpty(fileGuid))
                    continue;
                
                pbxProject.RemoveFileFromBuild(targetGuid, fileGuid);
                pbxProject.RemoveFile(fileGuid);
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
                    
                    pbxProject.RemoveFileFromBuild(targetGuid, fileGuid);
                    pbxProject.RemoveFile(fileGuid);
                }
                else if (subDirRealPath.EndsWith(FileExtensions.XcodeUniversalFrameworkFile) || subDirRealPath.EndsWith(FileExtensions.XcodeFrameworkFile))
                {
                    var fileGuid = pbxProject.FindFileGuidByRealPath(subDirRealPath);
                    if (!string.IsNullOrEmpty(fileGuid)) 
                        pbxProject.RemoveFrameworkFromProject(targetGuid, fileGuid);
                }
                else
                {
                    pbxProject.RemoveGroupByRealPath(targetGuid, subDirRealPath);
                }
            }
        }
        
        /// <summary>
        /// Removes a group reference from the UnityMain target in Xcode project by project path.
        /// </summary>
        /// <param name="pbxProject">The Xcode project. </param>
        /// <param name="realPath">The physical path to the folder on the filesystem of the group. </param>
        /// <param name="rootProjectPath">The root project path of group to be removed. </param>
        public static void RemoveGroupByProjectPath(this PBXProject pbxProject, string realPath, string rootProjectPath)
        { 
            var targetGuid = pbxProject.GetUnityMainTargetGuid();
            pbxProject.RemoveGroupByProjectPath(targetGuid, realPath, rootProjectPath);
        }

        /// <summary>
        /// Removes a group reference from the specified target in Xcode project by project path.
        /// </summary>
        /// <param name="pbxProject">The Xcode project. </param>
        /// <param name="targetGuid">The GUID of the target as returned by <see cref="PBXProject.TargetGuidByName"/>. </param>
        /// <param name="realPath">The physical path to the folder on the filesystem of the group. </param>
        /// <param name="rootProjectPath">The root project path of group to be removed. </param>
        public static void RemoveGroupByProjectPath(this PBXProject pbxProject,
            string targetGuid,
            string realPath,
            string rootProjectPath)
        {
            // 移除文件引用
            var files = Directory.GetFiles(realPath);
            foreach (var file in files)
            {
                if (file.EndsWith(FileExtensions.MacOSXDesktopServicesStoreFile))
                    continue;
                
                var fileName = Path.GetFileName(file);
                var fileProjectPath = $"{rootProjectPath}/{fileName}";
                var fileGuid = pbxProject.FindFileGuidByProjectPath(fileProjectPath);
                if (string.IsNullOrEmpty(fileGuid))
                    continue;
                
                pbxProject.RemoveFileFromBuild(targetGuid, fileGuid);
                pbxProject.RemoveFile(fileGuid);
            }
            
            // 移除文件夹引用
            var subDirRealPaths = Directory.GetDirectories(realPath);
            foreach (var subDirRealPath in subDirRealPaths)
            {
                var dirName = Path.GetDirectoryName(subDirRealPath);
                var subDirProjectPath = $"{rootProjectPath}/{dirName}";
                
                if (subDirRealPath.EndsWith(FileExtensions.XcodeAssetsFile))
                {
                    var fileGuid = pbxProject.FindFileGuidByProjectPath(subDirProjectPath);
                    if (string.IsNullOrEmpty(fileGuid)) 
                        continue;
                    
                    pbxProject.RemoveFileFromBuild(targetGuid, fileGuid);
                    pbxProject.RemoveFile(fileGuid);
                }
                else if (subDirRealPath.EndsWith(FileExtensions.XcodeUniversalFrameworkFile) || subDirRealPath.EndsWith(FileExtensions.XcodeFrameworkFile))
                {
                    var fileGuid = pbxProject.FindFileGuidByProjectPath(subDirProjectPath);
                    if (!string.IsNullOrEmpty(fileGuid)) 
                        pbxProject.RemoveFrameworkFromProject(targetGuid, fileGuid);
                }
                else
                {
                    pbxProject.RemoveGroupByProjectPath(targetGuid, subDirRealPath, subDirProjectPath);
                }
            }
        }
    }
}
#endif