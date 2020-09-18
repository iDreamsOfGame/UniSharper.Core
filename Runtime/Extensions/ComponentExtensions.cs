// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Reflection;

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Component"/>.
    /// </summary>
    public static class ComponentExtensions
    {
        #region Methods

        /// <summary>
        /// Copies values from the <see cref="Component"/> to another <see cref="Component"/> of the
        /// <see cref="GameObject"/>.
        /// </summary>
        /// <param name="component">The <see cref="Component"/>.</param>
        /// <param name="targetGameObject">The target <see cref="GameObject"/>.</param>
        public static void CopyComponentValues(this Component component, GameObject targetGameObject)
        {
            if (targetGameObject)
            {
                Type type = component.GetType();
                Component copyComponent = targetGameObject.GetOrAddComponent(type);
                MemberInfo[] memberCollection = type.GetMembers();

                foreach (MemberInfo item in memberCollection)
                {
                    if (item.MemberType == MemberTypes.Field)
                    {
                        FieldInfo fieldInfo = (FieldInfo)item;

                        if (!fieldInfo.IsLiteral)
                        {
                            object fieldValue = fieldInfo.GetValue(component);

                            if (fieldValue is ICloneable)
                            {
                                fieldInfo.SetValue(copyComponent, (fieldValue as ICloneable).Clone());
                            }
                            else
                            {
                                fieldInfo.SetValue(copyComponent, fieldInfo.GetValue(component));
                            }
                        }
                    }
                    else if (item.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo propertyInfo = (PropertyInfo)item;
                        MethodInfo setMethodInfo = propertyInfo.GetSetMethod(false);

                        if (setMethodInfo != null)
                        {
                            object propertyValue = propertyInfo.GetValue(component, null);

                            if (propertyValue is ICloneable)
                            {
                                propertyInfo.SetValue(copyComponent, (propertyValue as ICloneable).Clone(), null);
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

        #endregion Methods
    }
}