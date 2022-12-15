// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniSharper;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Property drawer for <see cref="UniSharper.ImplementedTypesFiledAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(ImplementedTypesFiledAttribute))]
    internal class ImplementedTypesFiledDrawer : PropertyDrawer
    {
        private static Dictionary<Type, string[]> interfaceTypeImplementedTypeNamesMap;
        
        private static string[] GetImplementedTypeNames(Type interfaceType, bool searchAllAssemblies = false)
        {
            if (interfaceType == null)
                return Array.Empty<string>();
            
            interfaceTypeImplementedTypeNamesMap ??= new Dictionary<Type, string[]>();

            if (interfaceTypeImplementedTypeNamesMap.TryGetValue(interfaceType, out var list))
                return list;
            
            var assembly = interfaceType.Assembly;
            var allTypes = searchAllAssemblies ? AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()) 
                : assembly.GetTypes();
            list = allTypes.Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => $"{x.FullName}, {assembly.GetName().Name}").ToArray();
            
            if(!interfaceTypeImplementedTypeNamesMap.ContainsKey(interfaceType))
                interfaceTypeImplementedTypeNamesMap.Add(interfaceType, list);
            
            return list;
        }

        private static string[] GetImplementedTypeDisplayNames(string[] typeNames, bool showTypeFullName = false)
        {
            return typeNames.Select(n =>
            {
                var typeName = n.Split(',')[0];
                if (showTypeFullName)
                    return typeName;

                var segments = typeName.Split('.');
                return segments[^1];
            }).ToArray();
        }

        private static int GetImplementedTypeDisplayItemIndex(string[] typeNames, string value) 
            => string.IsNullOrEmpty(value) ? 0 : Array.FindIndex(typeNames, value.Contains);

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
                
                var fieldAttribute = (ImplementedTypesFiledAttribute)attribute;

                var interfaceType = fieldAttribute.InterfaceType ?? Assembly.Load(fieldAttribute.AssemblyName).GetType(fieldAttribute.InterfaceTypeFullName);
                var typeNames = GetImplementedTypeNames(interfaceType, fieldAttribute.SearchAllAssemblies);

                if (typeNames.Length > 0)
                {
                    var displayItems = GetImplementedTypeDisplayNames(typeNames, fieldAttribute.ShowTypeFullName);
                    var index = GetImplementedTypeDisplayItemIndex(typeNames, property.stringValue);
                    index = Math.Max(index, 0);
                    index = EditorGUI.Popup(position, label.text, index, displayItems);
                    property.stringValue = typeNames[index];
                }
                else
                {
                    EditorGUI.Popup(position, label.text, 0, Array.Empty<string>());
                    property.stringValue = string.Empty;
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