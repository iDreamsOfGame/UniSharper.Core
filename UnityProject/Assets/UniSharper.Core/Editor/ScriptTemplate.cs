﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.IO;
using UniSharper;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Represents the template for generating new script.
    /// </summary>
    public static class ScriptTemplate
    {
        /// <summary>
        /// Load script template file.
        /// </summary>
        /// <param name="fileName">The file name of script template. </param>
        /// <param name="packageName">The package name of script template. </param>
        /// <returns>The text content of script template file. </returns>
        public static string LoadScriptTemplateFile(string fileName, string packageName = null)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            // Search file in 'Assets' folder.
            var fileNameWithoutExtensions = Path.GetFileNameWithoutExtension(fileName);
            var assets = UniAssetDatabase.LoadEditorResources<TextAsset>(fileNameWithoutExtensions);
            if (assets != null && assets.Length > 0)
                return assets[0].text;
            
            if (string.IsNullOrEmpty(packageName)) 
                return string.Empty;
            
            // Search file in package directory.
            var packagePath = $"{PlayerEnvironment.PackagesFolderName}/{packageName}";
            assets = UniAssetDatabase.LoadEditorResources<TextAsset>(fileNameWithoutExtensions, packagePath);
            return assets != null && assets.Length > 0 ? assets[0].text : string.Empty;
        }

        /// <summary>
        /// Represents the strings of class member.
        /// </summary>
        public static class ClassMemberFormatString
        {
            /// <summary>
            /// The format string for enumeration definition.
            /// </summary>
            public const string EnumDefinition = "\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic enum {1}\r\n\t\t{{\r\n{2}\r\n\t\t}}";

            /// <summary>
            /// The format string for enumeration property.
            /// </summary>
            public const string EnumProperty = "\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic {1} {2} => ({1}){3};";

            /// <summary>
            /// The format string for private field.
            /// </summary>
            public const string PrivateField = "\t\tprivate {0} {1};";
            
            /// <summary>
            /// The format string for auto-implemented property.
            /// </summary>
            public const string AutoImplementedProperty = "\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic {1} {2} {{ get; set; }}";
            
            /// <summary>
            /// The format string for expression-bodied getter property.
            /// </summary>
            public const string ExpressionBodiedGetterProperty = "\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic {1} {2} => {3};";
            
            /// <summary>
            /// The format string for only getter property.
            /// </summary>
            public const string OnlyGetterProperty = "\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic {1} {2}"
                                                           + "\r\n\t\t{{\r\n\t\t\tget\r\n\t\t\t{{\r\n\t\t\t\t{3}\r\n\t\t\t}}\r\n\t\t}}";
        }

        /// <summary>
        /// Represents the placeholder variable.
        /// </summary>
        public static class Placeholders
        {
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
        }
    }
}