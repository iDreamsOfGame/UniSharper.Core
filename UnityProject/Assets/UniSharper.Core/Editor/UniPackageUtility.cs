// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Linq;
using UnityEditor.PackageManager;

namespace UniSharperEditor
{
    /// <summary>
    /// Package utility functions.
    /// </summary>
    public static class UniPackageUtility
    {
        /// <summary>
        /// Gets the registered package by name.
        /// </summary>
        /// <param name="name">The package name. </param>
        /// <returns>The <see cref="UnityEditor.PackageManager.PackageInfo"/> instance describing found package. </returns>
        public static PackageInfo GetRegisteredPackage(string name)
        {
            var packages = PackageInfo.GetAllRegisteredPackages();
            return packages.FirstOrDefault(packageInfo => packageInfo.name.Equals(name));
        }
    }
}