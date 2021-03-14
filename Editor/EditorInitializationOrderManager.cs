// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Class for managing editor scripts initialization order.
    /// </summary>
    [InitializeOnLoad]
    internal sealed class EditorInitializationOrderManager
    {
        #region Fields

        /// <summary>
        /// The loaded types.
        /// </summary>
        private static Type[] loadedTypes;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="EditorInitializationOrderManager"/> class.
        /// </summary>
        static EditorInitializationOrderManager()
        {
            var typeList = new List<Type>();

            for (int i = 0, length = LoadedTypes.Length; i < length; ++i)
            {
                var type = LoadedTypes[i];

                if (type.IsDefined(typeof(InitializeOnEditorStartupAttribute), false))
                {
                    typeList.Add(type);
                }
            }

            typeList.Sort(new InitializationOrderComparer());
            typeList.ForEach(type =>
            {
                try
                {
                    RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                }
                catch (TypeInitializationException ex)
                {
                    Debug.LogException(ex.InnerException);
                }
            });
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the loaded types.
        /// </summary>
        /// <value>The loaded types.</value>
        internal static Type[] LoadedTypes
        {
            get
            {
                if (loadedTypes == null)
                {
                    try
                    {
                        var assembly = Assembly.GetExecutingAssembly();
                        loadedTypes = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException)
                    {
                        return new Type[0];
                    }
                }

                return loadedTypes;
            }
        }

        #endregion Properties

        #region Classes

        /// <summary>
        /// The comparer of initialization order.
        /// </summary>
        /// <seealso cref="IComparer{Type}"/>
        private class InitializationOrderComparer : IComparer<Type>
        {
            #region Methods

            /// <summary>
            /// Compares the specified x with y.
            /// </summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>
            /// A signed integer that indicates the relative values of x and y, as shown in the
            /// following table.
            /// </returns>
            public int Compare(Type x, Type y)
            {
                var xAttrs = x.GetCustomAttributes(typeof(InitializeOnEditorStartupAttribute), false) as InitializeOnEditorStartupAttribute[];
                var yAttrs = y.GetCustomAttributes(typeof(InitializeOnEditorStartupAttribute), false) as InitializeOnEditorStartupAttribute[];

                if (xAttrs[0].ExecutionOrder > yAttrs[0].ExecutionOrder)
                {
                    return -1;
                }
                else if (xAttrs[0].ExecutionOrder < yAttrs[0].ExecutionOrder)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            #endregion Methods
        }

        #endregion Classes
    }
}