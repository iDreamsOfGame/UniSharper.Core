// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.IO;
using System.Linq;
using UniSharper;
using UnityEditor;
#if UNITY_2021_1_OR_NEWER
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
#endif

namespace UniSharperEditor
{
    /// <summary>
    /// Performs operations on <see cref="System.String"/> instances that contain file or directory path information in Unity editor.
    /// </summary>
    public static class EditorPath
    {
        /// <summary>
        /// Determines whether the specified path is under project <c>Assets</c> folder or any <c>Package</c> folder.
        /// </summary>
        /// <param name="path">The physical path of the asset or asset path to the project. </param>
        /// <returns><c>true</c> if the specified path is under project <c>Assets</c> folder or any <c>Package</c> folder; otherwise, <c>false</c>.</returns>
        public static bool IsAssetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var assetPath = GetAssetPath(path);
            return !string.IsNullOrEmpty(assetPath);
        }
        
        /// <summary>
        /// Gets the physical path of the asset path that combined by an array. 
        /// </summary>
        /// <param name="paths">An array of parts of the path. </param>
        /// <returns>The physical path of the asset path that combined by an array. </returns>
        public static string GetFullPath(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                return null;

            var path = Path.Combine(paths);

            // Check whether the path is asset path under the project. 
            if (path.StartsWith(PlayerEnvironment.AssetsFolderName + Path.AltDirectorySeparatorChar))
                return Path.Combine(Directory.GetCurrentDirectory(), path);
            
#if UNITY_2021_1_OR_NEWER
            // Check whether the path is asset path under any package of the project. 
            if (path.StartsWith(PlayerEnvironment.PackagesFolderName + Path.AltDirectorySeparatorChar))
            {
                var packageInfoCollection = PackageInfo.GetAllRegisteredPackages();
                if (packageInfoCollection != null && packageInfoCollection.Length > 0)
                    return (from packageInfo in packageInfoCollection where path.StartsWith(packageInfo.assetPath) 
                        select Path.Combine(path.Replace(packageInfo.assetPath, packageInfo.resolvedPath))).FirstOrDefault();
            }
#endif

            return null;
        }

        /// <summary>
        /// Gets the physical path of the asset path that combined by an array. The asset must be included in project, otherwise return <c>null</c>.
        /// </summary>
        /// <param name="paths">An array of parts of the path. </param>
        /// <returns>The physical path of the asset path that combined by an array. </returns>
        public static string GetPhysicalPath(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                return null;

            var path = Path.Combine(paths);

            // Check whether the path is asset path under the project. 
            if (path.StartsWith(PlayerEnvironment.AssetsFolderName + Path.AltDirectorySeparatorChar))
            {
                if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)))
                    return Path.Combine(Directory.GetCurrentDirectory(), path);
            }

#if UNITY_2021_1_OR_NEWER
            // Check whether the path is asset path under any package of the project. 
            if (path.StartsWith(PlayerEnvironment.PackagesFolderName + Path.AltDirectorySeparatorChar))
            {
                var packageInfoCollection = PackageInfo.GetAllRegisteredPackages();
                if (packageInfoCollection != null && packageInfoCollection.Length > 0)
                    return (from packageInfo in packageInfoCollection where path.StartsWith(packageInfo.assetPath) 
                        select Path.Combine(path.Replace(packageInfo.assetPath, packageInfo.resolvedPath))).FirstOrDefault();
            }
#endif

            return null;
        }
        
        /// <summary>
        /// Converts absolute path to the path relative to the project <c>Assets</c> folder or any <c>Package</c> folder.
        /// </summary>
        /// <param name="path">The absolute path need to be converted.</param>
        /// <returns>The path relative to the project <c>Assets</c> folder or any <c>Package</c> folder. </returns>
        public static string GetAssetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            // Already asset path, directly return path.
            if (path.StartsWith(PlayerEnvironment.AssetsFolderName + Path.AltDirectorySeparatorChar))
            {
                if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)))
                    return path;
            }

            var currentDirectory = Directory.GetCurrentDirectory();
            var assetsFolderFullPath = Path.GetFullPath(PlayerEnvironment.AssetsFolderName);
            
            // Check whether the absolute path is under the Assets folder.
            if (path.StartsWith(assetsFolderFullPath))
            {
                var result = path.Substring(currentDirectory.Length + 1);
                if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(result)))
                    return result;
            }

#if UNITY_2021_1_OR_NEWER
            // Check whether the absolute path is under any package folder.
            var packageInfoCollection = PackageInfo.GetAllRegisteredPackages();
            if (path.StartsWith(PlayerEnvironment.PackagesFolderName + Path.AltDirectorySeparatorChar))
            {
                var paths = path.Split(Path.AltDirectorySeparatorChar);
                if (paths.Length > 1)
                {
                    var packagePath = Path.Combine(PlayerEnvironment.PackagesFolderName, paths[1]);
                    if (packageInfoCollection.Any(packageInfo => packageInfo.assetPath.Equals(packagePath)))
                        return path;
                }
            }
            
            if (packageInfoCollection != null && packageInfoCollection.Length > 0)
                return (from packageInfo in packageInfoCollection where path.StartsWith(packageInfo.resolvedPath) 
                    select Path.Combine(packageInfo.assetPath, path.Substring(packageInfo.resolvedPath.Length + 1))).FirstOrDefault();
#endif

            return null;
        }

        /// <summary>
        /// Tries to get the path relative to the project <c>Assets</c> folder or any <c>Package</c> folder.
        /// </summary>
        /// <param name="path">The absolute path need to be converted. </param>
        /// <param name="assetPath">The path relative to the project <c>Assets</c> folder or any <c>Package</c> folder. </param>
        /// <returns><c>true</c> if conversion is successful; otherwise, <c>false</c>.</returns>
        public static bool TryGetAssetPath(string path, out string assetPath)
        {
            assetPath = GetAssetPath(path);
            return !string.IsNullOrEmpty(assetPath);
        }
        
        /// <summary>
        /// Gets the asset path of package.
        /// </summary>
        /// <param name="packageName">The name of package. </param>
        /// <returns>The asset path of the package. </returns>
        public static string GetPackageAssetPath(string packageName)
        {
#if UNITY_2021_1_OR_NEWER
            if (string.IsNullOrEmpty(packageName))
                return null;
            
            var packageInfoCollection = PackageInfo.GetAllRegisteredPackages();
            return packageInfoCollection != null && packageInfoCollection.Length > 0
                ? (from packageInfo in packageInfoCollection where packageInfo.name.Equals(packageName) select packageInfo.assetPath).FirstOrDefault() 
                : null;
#else
            return $"Packages://{packageName}";
#endif
        }

        /// <summary>
        /// Gets the local path of the package on disk.
        /// </summary>
        /// <param name="packageName">The name of package. </param>
        /// <returns>The local path of the package on disk. </returns>
        public static string GetPackageResolvedPath(string packageName)
        {
#if UNITY_2021_1_OR_NEWER
            if (string.IsNullOrEmpty(packageName))
                return null;

            var packageInfoCollection = PackageInfo.GetAllRegisteredPackages();
            return packageInfoCollection != null && packageInfoCollection.Length > 0
                ? (from packageInfo in packageInfoCollection where packageInfo.name.Equals(packageName) select packageInfo.resolvedPath).FirstOrDefault()
                : null;
#else
            return string.Empty;
#endif
        }
    }
}