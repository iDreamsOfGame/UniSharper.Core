// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using UniSharper;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Property drawer for <see cref="LayerFieldAttribute"/>.
    /// </summary>
    /// <seealso cref="PropertyDrawer"/>
    [CustomPropertyDrawer(typeof(LayerFieldAttribute))]
    internal class LayerFieldDrawer : PropertyDrawer
    {
        /// <summary>
        /// Override this method to make your own GUI for the property.
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                var layerFiledAttribute = (LayerFieldAttribute)attribute;
                if (layerFiledAttribute.UseDefaultLayerFieldDrawer)
                {
                    property.intValue = EditorGUI.LayerField(position, label, property.intValue);
                }
                else
                {
                    var layerNameList = new List<string>();
                    layerNameList.AddRange(UnityEditorInternal.InternalEditorUtility.layers);
                    var index = property.intValue >= 0 ? layerNameList.IndexOf(LayerMask.LayerToName(property.intValue)) : -1;
                    index = EditorGUI.Popup(position, label.text, index, layerNameList.ToArray());
                    property.intValue = index >= 0 ? LayerMask.NameToLayer(layerNameList[index]) : -1;
                }
                
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}