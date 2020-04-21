// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.IO;
using ReSharpPathUtility = System.IO.PathUtility;

namespace UniSharperEditor
{
    /// <summary>
    /// Performs operations on <see cref="System.String"/> instances that contain file or directory
    /// path information in Unity editor.
    /// </summary>
    public static class EditorPath
    {
        #region Methods

        /// <summary>
        /// Converts to absolute path.
        /// </summary>
        /// <param name="paths">An array of parts of the path.</param>
        /// <returns>The absolute path of the asset path.</returns>
        /// <exception cref="ArgumentNullException">path</exception>
        public static string ConvertToAbsolutePath(params string[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            string[] newPaths = new string[paths.Length + 1];
            newPaths[0] = Directory.GetCurrentDirectory();
            paths.CopyTo(newPaths, 1);
            return ReSharpPathUtility.UnifyToAltDirectorySeparatorChar(Path.Combine(newPaths));
        }

        /// <summary>
        /// Converts absolute path to the path relative to the project folder.
        /// </summary>
        /// <param name="path">The path need to be converted.</param>
        /// <returns>The asset path to the project.</returns>
        /// <exception cref="ArgumentNullException">path</exception>
        public static string ConvertToAssetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            string currentDirectory = ReSharpPathUtility.UnifyToAltDirectorySeparatorChar(Directory.GetCurrentDirectory());
            string absolutePath = ReSharpPathUtility.UnifyToAltDirectorySeparatorChar(path);

            if (absolutePath.KmpIndexOf(currentDirectory) != -1)
            {
                return ReSharpPathUtility.UnifyToAltDirectorySeparatorChar(absolutePath.Substring(currentDirectory.Length + 1));
            }

            return path;
        }

        /// <summary>
        /// Determines whether the specified path is a Asset path relative to the project folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if [is asset path] [the specified path]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">path</exception>
        public static bool IsAssetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            string newPath = ReSharpPathUtility.UnifyToAltDirectorySeparatorChar(path);
            return newPath.StartsWith(EditorEnvironment.AssetsFolderName + Path.AltDirectorySeparatorChar) || newPath.StartsWith(ReSharpPathUtility.UnifyToAltDirectorySeparatorChar(Directory.GetCurrentDirectory()));
        }

        #endregion Methods
    }
}