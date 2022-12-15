// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using UniSharper.Extensions;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Extensions
{
    internal class GameObjectMenuItemsExtensions
    {
        [MenuItem("GameObject/Copy Path in Hierarchy",false,-1)]
        private static void CopyTransformPath()
        {
            var path = Selection.activeTransform.GetPath();
            GUIUtility.systemCopyBuffer = path;
        }

        [MenuItem("GameObject/Copy Path in Hierarchy",true,-1)]
        private static bool CopyTransformPathValidator() => Selection.activeTransform;
    }
}