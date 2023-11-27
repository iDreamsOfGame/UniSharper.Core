// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace UniSharper
{
    /// <summary>
    /// The collection of search pattern constant.
    /// </summary>
    public sealed class SearchPattern
    {
        /// <summary>
        /// The search pattern of Unity scene files.
        /// </summary>
        public const string UnitySceneFiles = "*.unity";
        
        /// <summary>
        /// The search pattern of Unity binary files.
        /// </summary>
        public const string UnityBinaryFiles = "*.bytes";

        /// <summary>
        /// The search pattern of Unity prefab files.
        /// </summary>
        public const string UnityPrefabFiles = "*.prefab";

        /// <summary>
        /// The search pattern of PNG image files.
        /// </summary>
        public const string PngImageFiles = "*.png";

        /// <summary>
        /// The search pattern of old format excel files.
        /// </summary>
        public const string ExcelFiles = "*.xls";

        /// <summary>
        /// The search pattern of new format excel files.
        /// </summary>
        public const string ExcelXFiles = "*.xlsx";

        /// <summary>
        /// The search pattern of C Sharp script files.
        /// </summary>
        public const string CSharpScriptFiles = "*.cs";
    }
}