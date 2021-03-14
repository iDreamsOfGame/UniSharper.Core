// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;

namespace UniSharper.Rendering.DataParsers
{
    internal abstract class TilingSheetDataParser : ITilingSheetDataParser
    {
        #region Fields

        private static readonly Dictionary<TilingSheetDataFormat, ITilingSheetDataParser> parsers = new Dictionary<TilingSheetDataFormat, ITilingSheetDataParser>()
        {
            { TilingSheetDataFormat.UnityJson, new TPUnityJsonDataParser() }
        };

        #endregion Fields

        #region Properties

        protected static Dictionary<string, Dictionary<string, Rect>> DataMap { get; } = new Dictionary<string, Dictionary<string, Rect>>();

        #endregion Properties

        #region Methods

        public static ITilingSheetDataParser CreateParser(TilingSheetDataFormat dataFormat) => parsers.ContainsKey(dataFormat) ? parsers[dataFormat] : null;

        public abstract Dictionary<string, Rect> ParseData(string name, string data);

        #endregion Methods
    }
}