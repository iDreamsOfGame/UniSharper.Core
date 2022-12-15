// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.IO;
using System.Xml;

namespace UniSharperEditor
{
    /// <summary>
    /// Provides utilities to handle file AndroidManifest.xml. 
    /// </summary>
    public static class AndroidManifestUtility
    {
        /// <summary>
        /// Modify the file AndroidManifest.xml.
        /// </summary>
        /// <param name="androidManifestFilePath">The path of file AndroidManifest.xml.</param>
        /// <param name="handler">The function to handle the content of file AndroidManifest.xml.</param>
        public static void ModifyAndroidManifestFile(string androidManifestFilePath, Action<XmlElement> handler)
        {
            if (!File.Exists(androidManifestFilePath))
                return;

            // 加载XML文件
            var manifestXmlFile = new XmlDocument();
            manifestXmlFile.Load(androidManifestFilePath);

            // manifest节点
            var rootNode = manifestXmlFile.DocumentElement;
            if (rootNode == null)
                return;

            handler?.Invoke(rootNode);
            manifestXmlFile.Save(androidManifestFilePath);
        }
    }
}