// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace UniSharperEditor
{
    /// <summary>
    /// Provides information about, and means to manipulate, the current unity editor environment
    /// and platform. This class cannot be inherited.
    /// </summary>
    public static class EditorEnvironment
    {
        /// <summary>
        /// The folder name of <c>Library</c>.
        /// </summary>
        public const string LibraryFolderName = "Library";

        /// <summary>
        /// The folder name of <c>Logs</c>.
        /// </summary>
        public const string LogsFolderName = "Logs";

        /// <summary>
        /// The folder name of <c>ProjectSettings</c>.
        /// </summary>
        public const string ProjectSettingsFolderName = "ProjectSettings";

        /// <summary>
        /// The folder name of <c>Temp</c>.
        /// </summary>
        public const string TempFolderName = "Temp";

        /// <summary>
        /// The folder name of <c>UserSettings</c>.
        /// </summary>
        public const string UserSettingsFolderName = "UserSettings";

        /// <summary>
        /// The default folder name of <c>Scripts</c>.
        /// </summary>
        public const string DefaultScriptsFolderName = "Scripts";
    }
}