using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Color32"/> utilities.
    /// </summary>
    public static class Color32Utility
    {
        /// <summary>
        /// Converts the string representation of a <see cref="Color32"/> equivalent.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Color32"/> equivalent. </param>
        /// <returns>A <see cref="Color32"/> equivalent to the <c>s</c>. </returns>
        public static Color32 Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;

            var values = StringUtility.GetColorRgbaValues(s);
            if (values.Length < 4)
                return default;
            
            byte.TryParse(values[0], out var r);
            byte.TryParse(values[1], out var g);
            byte.TryParse(values[2], out var b);
            byte.TryParse(values[3], out var a);
            return new Color32(r, g, b, a);
        }

        /// <summary>
        /// Converts the string representation of a <see cref="Color32"/> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string representation of a <see cref="Color32"/> equivalent. </param>
        /// <param name="result">A <see cref="Color32"/> equivalent to the <c>s</c>. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParse(string s, out Color32 result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = default;
                return false;
            }

            var values = StringUtility.GetColorRgbaValues(s);
            if (values.Length < 4)
            {
                result = default;
                return false;
            }
            
            byte.TryParse(values[0], out var r);
            byte.TryParse(values[1], out var g);
            byte.TryParse(values[2], out var b);
            byte.TryParse(values[3], out var a);
            result = new Color32(r, g, b, a);
            return true;
        }
    }
}