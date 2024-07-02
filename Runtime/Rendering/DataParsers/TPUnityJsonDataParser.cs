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
        public override Dictionary<string, Rect> ParseData(string name, string data)
        {
            if (DataMap.TryGetValue(name, out var result)) 
                return result;
            
            DataMap.Add(name, new Dictionary<string, Rect>());
            var jsonData = JsonConvert.DeserializeObject<TPUnityJsonData>(data);
            if (jsonData == null)
                return DataMap[name];
                
            var textureSize = jsonData.Metadata.ImageSize;

            foreach (var pair in jsonData.Frames)
            {
                // Calculate mapping.
                var frameName = pair.Key;
                var frameData = pair.Value;
                var scale = new Vector2(frameData.Frame.Width / textureSize.Width, frameData.Frame.Height / textureSize.Height);
                var offset = new Vector2(frameData.Frame.X / textureSize.Width, (textureSize.Height - frameData.Frame.Y - frameData.SourceSize.Height) / textureSize.Height);
                var rect = new Rect(offset.x, offset.y, scale.x, scale.y);
                DataMap[name].Add(frameName, rect);
            }

            return DataMap[name];
        }

        private class DualSize
        {
            [JsonProperty("h")]
            public float Height { get; set; }

            [JsonProperty("w")]
            public float Width { get; set; }

            public override string ToString() => $"w={Width}, h={Height}";
        }

        private class FrameData
        {
            [JsonProperty("frame")]
            public QuadSize Frame { get; set; }

            [JsonProperty("rotated")]
            public bool Rotated { get; set; }

            [JsonProperty("sourceSize")]
            public DualSize SourceSize { get; set; }

            [JsonProperty("trimmed")]
            public bool Trimmed { get; set; }

            public override string ToString() => $"frame={Frame}, rotated={Rotated}, trimmed={Trimmed}, sourceSize={5}";
        }

        private class QuadSize : DualSize
        {
            [JsonProperty("x")]
            public float X { get; set; }

            [JsonProperty("y")]
            public float Y { get; set; }

            public override string ToString() => $"x={X}, y={Y}, w={Width}, h={Height}";
        }

        private class TPUnityJsonData
        {
            [JsonProperty("frames")]
            public Dictionary<string, FrameData> Frames { get; set; }

            [JsonProperty("meta")]
            public TPUnityJsonMetadata Metadata { get; set; }
        }

        private class TPUnityJsonMetadata
        {
            [JsonProperty("image")]
            public string ImageName { get; set; }

            [JsonProperty("size")]
            public DualSize ImageSize { get; set; }

            public override string ToString() => $"image={ImageName}, size={ImageSize}";
        }
    }
}