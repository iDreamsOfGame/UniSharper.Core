// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.IO;
using System.Linq;
using UnityEditor;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace UniSharperEditor
{
    /// <summary>
    /// Performs operations on <see cref="System.String"/> instances that contain file or directory
    /// path information in Unity editor.
    /// </summary>
    public static class EditorPath
    {
        /// <summary>
        /// Converts absolute path to the path relative to the project <c>Assets</c> folder or any <c>Package</c> folder.
        /// </summary>
        /// <param name="path">The absolute path need to be converted.</param>
        /// <returns>The path relative to the project <c>Assets</c> folder or any <c>Package</c> folder. </returns>
        public static string ConvertToAssetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            // Already asset path, directly return path.
            if (path.StartsWith(EditorEnvironment.AssetsFolderName + Path.AltDirectorySeparatorChar))
            {
                if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)))
                    return path;
            }

            var currentDirectory = Directory.GetCurrentDirectory();
            var assetsFolderFullPath = Path.GetFullPath(EditorEnvironment.AssetsFolderName);
            
            // Check whether the absolute path is under the assets folder.
            if (path.StartsWith(assetsFolderFullPath))
            {
                var result = path[(currentDirectory.Length + 1)..];
                if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(result)))
                    return result;
            }

            // Check whether the absolute path is under any package folder.
            var packageInfoCollection = PackageInfo.GetAllRegisteredPackages();
            if (path.StartsWith(EditorEnvironment.PackagesFolderName + Path.AltDirectorySeparatorChar))
            {
                var paths = path.Split(Path.AltDirectorySeparatorChar);
                if (paths.Length > 1)
                {
                    var packagePath = Path.Combine(EditorEnvironment.PackagesFolderName, paths[1]);
                    if (packageInfoCollection.Any(packageInfo => packageInfo.assetPath.Equals(packagePath)))
                        return path;
                }
            }
            
            if (packageInfoCollection is { Length: > 0 })
                return (from packageInfo in packageInfoCollection where path.StartsWith(packageInfo.resolvedPath) 
                    select Path.Combine(packageInfo.assetPath, path[(packageInfo.resolvedPath.Length + 1)..])).FirstOrDefault();

            return null;
        }

        /// <summary>
        /// Determines whether the specified path is under project <c>Assets</c> folder or any <c>Package</c> folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if the specified path is under project <c>Assets</c> folder or any <c>Package</c> folder; otherwise, <c>false</c>.</returns>
        public static bool IsAssetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var assetPath = ConvertToAssetPath(path);
            return !string.IsNullOrEmpty(assetPath);
        }

        /// <summary>
        /// Gets the asset path of package.
        /// </summary>
        /// <param name="packageName">The name of package. </param>
        /// <returns>The asset path of the package. </returns>
        public static string GetPackageAssetPath(string packageName)
        {
            if (string.IsNullOrEmpty(packageName))
                return null;
            
            var packageInfoCollection = PackageInfo.GetAllRegisteredPackages();
            return packageInfoCollection is { Length: > 0 } 
                ? (from packageInfo in packageInfoCollection where packageInfo.name.Equals(packageName) select packageInfo.assetPath).FirstOrDefault() 
                : null;
        }

        /// <summary>
        /// Gets the local path of the package on disk.
        /// </summary>
        /// <param name="packageName">The name of package. </param>
        /// <returns>The local path of the package on disk. </returns>
        public static string GetPackageResolvedPath(string packageName)
        {
            if (string.IsNullOrEmpty(packageName))
                return null;

            var packageInfoCollection = PackageInfo.GetAllRegisteredPackages();
            return packageInfoCollection is { Length: > 0 }
                ? (from packageInfo in packageInfoCollection where packageInfo.name.Equals(packageName) select packageInfo.resolvedPath).FirstOrDefault()
                : null;
        }
    }
}