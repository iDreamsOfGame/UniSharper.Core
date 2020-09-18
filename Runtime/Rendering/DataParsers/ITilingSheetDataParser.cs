// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;

namespace UniSharper.Rendering.DataParsers
{
    /// <summary>
    /// The <see cref="ITilingSheetDataParser"/> interface defines methods for parsing the tiling
    /// sheet data.
    /// </summary>
    internal interface ITilingSheetDataParser
    {
        #region Methods

        Dictionary<string, Rect> ParseData(string name, string data);

        #endregion Methods
    }
}