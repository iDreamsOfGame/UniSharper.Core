// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using UniSharper;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Property drawer for <see cref="FlagsEnumFieldAttribute"/>.
    /// </summary>
    /// <seealso cref="PropertyDrawer"/>
    [CustomPropertyDrawer(typeof(FlagsEnumFieldAttribute))]
    internal class FlagsEnumFieldDrawer : PropertyDrawer
    {
        /// <summary>
        /// Override this method to make your own GUI for the property.
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                var flagsAttribute = (FlagsEnumFieldAttribute)attribute;
                var targetEnum = GetBaseProperty<Enum>(property);

                var propertyDisplayName = flagsAttribute.Label;

                if (string.IsNullOrEmpty(propertyDisplayName))
                {
                    propertyDisplayName = property.name;
                }

                if (targetEnum != null)
                {
                    EditorGUI.BeginProperty(position, label, property);
                    property.intValue = (int)Enum.ToObject(targetEnum.GetType(), EditorGUI.EnumFlagsField(position, propertyDisplayName, targetEnum));
                    EditorGUI.EndProperty();
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        /// <summary>
        /// Gets the base property.
        /// </summary>
        /// <typeparam name="T">The Type definition of property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The property with Type definition.</returns>
        private static T GetBaseProperty<T>(SerializedProperty property)
        {
            // Separate the steps it takes to get to this property.
            var separatedPaths = property.propertyPath.Split('.');

            // Go down to the root of this serialized property.
            var reflectionTarget = property.serializedObject.targetObject as object;

            foreach (var path in separatedPaths)
            {
                reflectionTarget = reflectionTarget.GetFieldValue(path);
            }

            return (T)reflectionTarget;
        }
    }
}