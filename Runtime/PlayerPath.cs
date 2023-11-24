// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.IO;

namespace UniSharper
{
    /// <summary>
    /// Performs operations on <see cref="System.String"/> instances that contain file or directory path information in Unity player.
    /// </summary>
    public static class PlayerPath
    {
        /// <summary>
        /// Gets the asset path of the path that combined by an array. 
        /// </summary>
        /// <param name="paths">An array of parts of the path. </param>
        /// <returns>The asset path of the path that combined by an array. </returns>
        public static string GetAssetPath(params string[] paths)
        {
            if (paths is not { Length: > 0 })
                return null;

            var path = Path.Combine(paths);
            return Path.Combine(PlayerEnvironment.AssetsFolderName, path);
        }
        
        /// <summary>
        /// Gets the path of package.
        /// </summary>
        /// <param name="packageName">The name of package. </param>
        /// <returns>The path of the package. </returns>
        public static string GetPackagePath(string packageName) =>
            !string.IsNullOrEmpty(packageName) ? Path.Combine(PlayerEnvironment.PackagesFolderName, packageName) : null;
    }
}