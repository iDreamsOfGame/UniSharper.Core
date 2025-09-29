// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Linq;
using UniSharper;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Property drawer for <see cref="UniSharper.SortingLayerFieldAttribute"/>.
    /// </summary>
    /// <seealso cref="PropertyDrawer"/>
    [CustomPropertyDrawer(typeof(SortingLayerFieldAttribute))]
    internal class SortingLayerFieldDrawer : PropertyDrawer
    {
        /// <summary>
        /// Override this method to make your own GUI for the property.
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                var sortingLayerList = SortingLayer.layers.Select(layer => layer.name).ToList();
                var index = !string.IsNullOrEmpty(property.stringValue) ? sortingLayerList.IndexOf(property.stringValue) : 0;
                index = EditorGUI.Popup(position, label.text, index, sortingLayerList.ToArray());
                property.stringValue = index >= 0 ? sortingLayerList[index] : string.Empty;

                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}