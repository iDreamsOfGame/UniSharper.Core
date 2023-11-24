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
        /// The search pattern of binary files.
        /// </summary>
        public const string BinaryFiles = "*.bytes";

        /// <summary>
        /// The search pattern of prefab files.
        /// </summary>
        public const string PrefabFiles = "*.prefab";

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