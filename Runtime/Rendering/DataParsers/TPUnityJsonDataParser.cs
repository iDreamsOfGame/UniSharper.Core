// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace UniSharper.Rendering.DataParsers
{
    /// <summary>
    /// Data parser to parse Unity JSON data of TexturePacker.
    /// Implements the <see cref="UniSharper.Rendering.DataParsers.TilingSheetDataParser" />
    /// </summary>
    /// <seealso cref="UniSharper.Rendering.DataParsers.TilingSheetDataParser" />
    internal class TPUnityJsonDataParser : TilingSheetDataParser
    {
        #region Methods

        public override Dictionary<string, Rect> ParseData(string name, string data)
        {
            if (!DataMap.ContainsKey(name))
            {
                DataMap.Add(name, new Dictionary<string, Rect>());
                var jsonData = JsonConvert.DeserializeObject<TPUnityJsonData>(data);
                var textureSize = jsonData.Metadata.ImageSize;

                foreach (var kv in jsonData.Frames)
                {
                    var frameName = kv.Key;
                    var frameData = kv.Value;

                    // Calculate mapping.
                    var scale = new Vector2(frameData.Frame.Width / textureSize.Width, frameData.Frame.Height / textureSize.Height);
                    var offset = new Vector2(frameData.Frame.X / textureSize.Width, (textureSize.Height - frameData.Frame.Y - frameData.SourceSize.Height) / textureSize.Height);
                    var rect = new Rect(offset.x, offset.y, scale.x, scale.y);
                    DataMap[name].Add(frameName, rect);
                }
            }

            return DataMap[name];
        }

        #endregion Methods

        #region Classes

        private class DualSize
        {
            #region Properties

            [JsonProperty("h")]
            public float Height { get; set; }

            [JsonProperty("w")]
            public float Width { get; set; }

            #endregion Properties

            #region Methods

            public override string ToString() => $"w={Width}, h={Height}";

            #endregion Methods
        }

        private class FrameData
        {
            #region Properties

            [JsonProperty("frame")]
            public QuatSize Frame { get; set; }

            [JsonProperty("rotated")]
            public bool Rotated { get; set; }

            [JsonProperty("sourceSize")]
            public DualSize SourceSize { get; set; }

            [JsonProperty("trimmed")]
            public bool Trimmed { get; set; }

            #endregion Properties

            #region Methods

            public override string ToString() => $"frame={Frame}, rotated={Rotated}, trimmed={Trimmed}, sourceSize={5}";

            #endregion Methods
        }

        private class QuatSize : DualSize
        {
            #region Properties

            [JsonProperty("x")]
            public float X { get; set; }

            [JsonProperty("y")]
            public float Y { get; set; }

            #endregion Properties

            #region Methods

            public override string ToString() => $"x={X}, y={Y}, w={Width}, h={Height}";

            #endregion Methods
        }

        private class TPUnityJsonData
        {
            #region Properties

            [JsonProperty("frames")]
            public Dictionary<string, FrameData> Frames { get; set; }

            [JsonProperty("meta")]
            public TPUnityJsonMetadata Metadata { get; set; }

            #endregion Properties
        }

        private class TPUnityJsonMetadata
        {
            #region Properties

            [JsonProperty("image")]
            public string ImageName { get; set; }

            [JsonProperty("size")]
            public DualSize ImageSize { get; set; }

            #endregion Properties

            #region Methods

            public override string ToString() => $"image={ImageName}, size={ImageSize},";

            #endregion Methods
        }

        #endregion Classes
    }
}