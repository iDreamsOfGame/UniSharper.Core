using System;
using System.Collections.Generic;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="System.String"/> utilities.
    /// </summary>
    public static class StringUtility
    {
        internal static string[] GetStringValuesInBrackets(string value)
        {
            value = value.Trim();
            if (value.IndexOf('(') != 0 || value.LastIndexOf(')') != value.Length - 1)
                return Array.Empty<string>();
            
            value = value.Trim('(', ')');
            return value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
        
        internal static Dictionary<string, string> GetKeyValuePairsInBrackets(string value)
        {
            value = value.Trim();
            if (value.IndexOf('(') != 0 || value.LastIndexOf(')') != value.Length - 1)
                return new Dictionary<string, string>();
            
            value = value.Trim('(', ')');
            var pairs = value.Split(',');
            if (pairs.Length == 0)
                return new Dictionary<string, string>();

            var result = new Dictionary<string, string>();
            foreach (var pair in pairs)
            {
                var kv = pair.Split(':');
                if (kv.Length != 2) 
                    continue;
                
                var k = kv[0].Trim();
                var v = kv[1].Trim();
                result.Add(k, v);
            }

            return result;
        }
        
        internal static string[] GetColorRgbaValues(string value)
        {
            value = value.Trim();
            if (value.IndexOf("RGBA(", StringComparison.Ordinal) != 0 || value.LastIndexOf(')') != value.Length - 1)
                return Array.Empty<string>();

            value = value.Replace("RGBA(", "(");
            return GetStringValuesInBrackets(value);
        }
    }
}