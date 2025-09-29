// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Reflection;
using UnityEngine;

namespace UniSharper.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Component"/>.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Copies values from the <see cref="Component"/> to another <see cref="Component"/> of the
        /// <see cref="GameObject"/>.
        /// </summary>
        /// <param name="component">The <see cref="Component"/>.</param>
        /// <param name="targetGameObject">The target <see cref="GameObject"/>.</param>
        public static void CopyComponentValues(this Component component, GameObject targetGameObject)
        {
            if (!targetGameObject) 
                return;
            
            var type = component.GetType();
            var copyComponent = targetGameObject.GetOrAddComponent(type);
            var memberCollection = type.GetMembers();

            foreach (var item in memberCollection)
            {
                if (item.MemberType == MemberTypes.Field)
                {
                    var fieldInfo = (FieldInfo)item;
                    if (fieldInfo.IsLiteral) 
                        continue;
                    
                    var fieldValue = fieldInfo.GetValue(component);
                    if (fieldValue is ICloneable cloneable)
                    {
                        fieldInfo.SetValue(copyComponent, cloneable.Clone());
                    }
                    else
                    {
                        fieldInfo.SetValue(copyComponent, fieldInfo.GetValue(component));
                    }
                }
                else if (item.MemberType == MemberTypes.Property)
                {
                    var propertyInfo = (PropertyInfo)item;
                    var setMethodInfo = propertyInfo.GetSetMethod(false);
                    if (setMethodInfo == null) 
                        continue;
                    
                    var propertyValue = propertyInfo.GetValue(component, null);
                    if (propertyValue is ICloneable cloneable)
                    {
                        propertyInfo.SetValue(copyComponent, cloneable.Clone(), null);
                    }
                    else
                    {
                        propertyInfo.SetValue(copyComponent, propertyInfo.GetValue(component, null), null);
                    }
                }
            }
        }
    }
}