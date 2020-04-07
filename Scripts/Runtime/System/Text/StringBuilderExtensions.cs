// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace System.Text
{
    /// <summary>
    /// Extension methods collection of <see cref="System.Text.StringBuilder"/>.
    /// </summary>
    public static class StringBuilderExtensions
    {
        #region Methods

        /// <summary>
        /// Appends the line terminator by windows style.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public static StringBuilder AppendWindowsNewLine(this StringBuilder stringBuilder)
        {
            return stringBuilder.Append("\r\n");
        }

        #endregion Methods
    }
}