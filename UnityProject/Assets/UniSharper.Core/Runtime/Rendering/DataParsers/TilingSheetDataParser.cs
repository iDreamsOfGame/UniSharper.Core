// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident

namespace UniSharper.Rendering.DataParsers
{
    internal abstract class TilingSheetDataParser : ITilingSheetDataParser
    {
        private static readonly Dictionary<TilingSheetDataFormat, ITilingSheetDataParser> Parsers = new Dictionary<TilingSheetDataFormat, ITilingSheetDataParser>()
        {
            { TilingSheetDataFormat.UnityJson, new TPUnityJsonDataParser() }
        };

        protected static Dictionary<string, Dictionary<string, Rect>> DataMap { get; } = new Dictionary<string, Dictionary<string, Rect>>();

        public static ITilingSheetDataParser CreateParser(TilingSheetDataFormat dataFormat) => Parsers.ContainsKey(dataFormat) ? Parsers[dataFormat] : null;

        public abstract Dictionary<string, Rect> ParseData(string name, string data);
    }
}