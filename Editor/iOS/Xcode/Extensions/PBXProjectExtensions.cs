// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

#if UNITY_IOS
using System.IO;
using System.Linq;
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
        /// Adds a new folder reference to the list of known files as "Create groups".
        /// </summary>
        /// <param name="pbxProject"></param>
        /// <param name="path">The physical path to the folder on the filesystem. </param>
        /// <param name="projectPath">The project path to the folder. </param>
        /// <param name="filesExclusive">The exclusive files list. </param>
        /// <param name="subDirsExclusive">The exclusive sub-directories list. </param>
        public static void CreateGroup(this PBXProject pbxProject, string path, string projectPath, string[] filesExclusive = null, string[] subDirsExclusive = null)
        {
            var targetGuid = pbxProject.GetUnityMainTargetGuid();

            // 添加文件引用
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (file.EndsWith(".DS_Store"))
                    continue;
                
                var fileName = Path.GetFileName(file);
                if (filesExclusive != null && filesExclusive.Contains(fileName))
                    continue;
                
                var projectFilePath = Path.Combine(projectPath, fileName);
                var fileGuid = pbxProject.AddFile(projectFilePath, projectFilePath);
                pbxProject.AddFileToBuild(targetGuid, fileGuid);
            }

            // 添加文件夹引用
            var subDirPaths = Directory.GetDirectories(path);
            foreach (var subDirPath in subDirPaths)
            {
                var dirName = Path.GetFileName(subDirPath);
                if (subDirsExclusive != null && subDirsExclusive.Contains(dirName))
                    continue;
                
                var subDirProjectPath = Path.Combine(projectPath, dirName);
                if (subDirPath.EndsWith(".xcassets"))
                {
                    var fileGuid = pbxProject.AddFile(subDirProjectPath, subDirProjectPath);
                    pbxProject.AddFileToBuild(targetGuid, fileGuid);
                }
                else if (subDirPath.EndsWith(".framework") || subDirPath.EndsWith(".xcframework"))
                {
                    var fileGuid = pbxProject.AddFile(subDirPath, subDirProjectPath);
                    pbxProject.AddFileToEmbedFrameworks(targetGuid, fileGuid);
                }
                else
                {
                    pbxProject.CreateGroup(subDirPath, subDirProjectPath);
                }
            }
        }
    }
}
#endif