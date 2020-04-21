// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace UniSharperEditor
{
    /// <summary>
    /// Represents the template for generating new script.
    /// </summary>
    public static class ScriptTemplate
    {
        #region Classes

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

        #endregion Classes
    }
}