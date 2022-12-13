// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="System.String"/> utilities.
    /// </summary>
    public static class StringUtility
    {
        /// <summary>
        /// Gets RGBA string values array in brackets.
        /// </summary>
        /// <param name="value">The string representation of RGBA string values. </param>
        /// <returns>The RGBA string values array. </returns>
        public static string[] GetColorRgbaStringValues(string value)
        {
            value = value.Trim();
            if (value.IndexOf("RGBA(", StringComparison.Ordinal) != 0 || value.LastIndexOf(')') != value.Length - 1)
                return Array.Empty<string>();

            value = value.Replace("RGBA(", "(");
            return GetStringValuesInBrackets(value);
        }
        
        /// <summary>
        /// Gets key/value string pairs in brackets.
        /// </summary>
        /// <param name="value">The string representation of key/value string pairs. </param>
        /// <param name="separator">A string that delimits the substrings in this string. </param>
        /// <param name="keyValueSeparator">A string that delimits the key/value in pair string. </param>
        /// <returns>The key/value string pairs. </returns>
        public static Dictionary<string, string> GetKeyValueStringPairsInBrackets(string value, string separator = ",", string keyValueSeparator = ":")
        {
            var result = new Dictionary<string, string>();
            var pairs = GetStringValuesInBrackets(value, separator);
            
            foreach (var pair in pairs)
            {
                var kvString = pair.Split(keyValueSeparator, StringSplitOptions.RemoveEmptyEntries);
                if (kvString.Length != 2) 
                    continue;
                
                var k = kvString[0].Trim();
                var v = kvString[1].Trim();
                result.Add(k, v);
            }

            return result;
        }

        /// <summary>
        /// Gets string values array in brackets.
        /// </summary>
        /// <param name="value">The string representation of string values array. </param>
        /// <param name="separator">A string that delimits the substrings in this string. </param>
        /// <returns>The string values array. </returns>
        public static string[] GetStringValuesInBrackets(string value, string separator = ",")
        {
            value = value.Trim();
            if (value.IndexOf('(') != 0 || value.LastIndexOf(')') != value.Length - 1)
                return Array.Empty<string>();
            
            value = value.Trim('(', ')');
            return value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}