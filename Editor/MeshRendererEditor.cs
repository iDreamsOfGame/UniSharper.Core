// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Custom editor for MeshRenderer component.
    /// </summary>
    [CustomEditor(typeof(MeshRenderer))]
    internal class MeshRendererEditor : Editor
    {
        #region Fields

        private MeshRenderer meshRenderer;

        #endregion Fields

        #region Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            meshRenderer = target as MeshRenderer;

            var layerNames = new string[SortingLayer.layers.Length];
            for (var i = 0; i < SortingLayer.layers.Length; i++)
                layerNames[i] = SortingLayer.layers[i].name;

            var layerValue = SortingLayer.GetLayerValueFromID(meshRenderer.sortingLayerID);
            layerValue = EditorGUILayout.Popup("Sorting Layer", layerValue, layerNames);

            var layer = SortingLayer.layers[layerValue];
            meshRenderer.sortingLayerName = layer.name;
            meshRenderer.sortingLayerID = layer.id;
            meshRenderer.sortingOrder = EditorGUILayout.IntField("Order in Layer", meshRenderer.sortingOrder);
        }

        #endregion Methods
    }
}