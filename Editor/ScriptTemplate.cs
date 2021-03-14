// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.IO;

using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Represents the template for generating new script.
    /// </summary>
    public static class ScriptTemplate
    {
        #region Methods

        /// <summary>
        /// Load script template file.
        /// </summary>
        /// <param name="fileName">The file name of script template. </param>
        /// <param name="packageName">The package name of script template. </param>
        /// <returns>The text content of script template file. </returns>
        public static string LoadScriptTemplateFile(string fileName, string packageName)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            if (packageName == null)
                throw new ArgumentNullException(nameof(packageName));

            // Search file in project folder.
            var fileNameWithoutExtensions = Path.GetFileNameWithoutExtension(fileName);
            var list = AssetDatabaseUtility.LoadEditorResources<TextAsset>(fileNameWithoutExtensions);

            if (list != null && list.Length > 0)
            {
                return list[0].text;
            }
            else
            {
                // Search file in package directory.
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "Library", "PackageCache");
                var searchDirResults = Directory.GetDirectories(rootPath, $"{packageName}*", SearchOption.TopDirectoryOnly);

                if (searchDirResults.Length > 0)
                {
                    var dirPath = searchDirResults[0];
                    var searchFileResults = Directory.GetFiles(dirPath, fileName, SearchOption.AllDirectories);

                    if (searchFileResults.Length > 0)
                    {
                        return File.ReadAllText(searchFileResults[0]);
                    }
                }
            }

            return string.Empty;
        }

        #endregion Methods

        #region Classes

        /// <summary>
        /// Represents the strings of class member.
        /// </summary>
        public static class ClassMemeberFormatString
        {
            #region Fields

            /// <summary>
            /// The format string for enumeration definition.
            /// </summary>
            public const string EnumDefinition = "\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic enum {1}\r\n\t\t{{\r\n{2}\r\n\t\t}}";

            /// <summary>
            /// The format string for enumeration property.
            /// </summary>
            public const string EnumProperty = "\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic {1} {2}\r\n\t\t{{\r\n\t\t\tget => ({1}){3};\r\n\t\t}}";

            /// <summary>
            /// The format string for property member.
            /// </summary>
            public const string PropertyMember = "\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic {1} {2}\r\n\t\t{{\r\n\t\t\tget;\r\n\t\t\tset;\r\n\t\t}}";

            #endregion Fields
        }

        /// <summary>
        /// Represents the placeholder variable.
        /// </summary>
        public static class Placeholders
        {
            #region Fields

            /// <summary>
            /// The placeholder for the string of constants.
            /// </summary>
            public const string Constants = "#CONSTANTS#";

            /// <summary>
            /// The placeholder for the enumeration inside of class.
            /// </summary>
            public const string EnumInsideOfClass = "#ENUM_INSIDE_OF_CLASS#";

            /// <summary>
            /// The placeholder for the string of fields.
            /// </summary>
            public const string Fields = "#FIELDS#";

            /// <summary>
            /// The placeholder for the string of namespace.
            /// </summary>
            public const string Namespace = "#NAMESPACE#";

            /// <summary>
            /// The placeholder for the string of properties.
            /// </summary>
            public const string Properties = "#PROPERTIES#";

            /// <summary>
            /// The placeholder for the string of script name.
            /// </summary>
            public const string ScriptName = "#SCRIPT_NAME#";

            #endregion Fields
        }

        #endregion Classes
    }
}