// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Represents the implemented type property attribute declaration.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ImplementedTypesFiledAttribute : PropertyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementedTypesFiledAttribute"/> class.
        /// </summary>
        /// <param name="interfaceType">The <see cref="Type"/> of interface.</param>
        /// <param name="searchAllAssemblies">If search <see cref="Type"/> s in  all assemblies.</param>
        /// <param name="showTypeFullName">If show full name of <see cref="Type"/>.</param>
        public ImplementedTypesFiledAttribute(Type interfaceType, bool searchAllAssemblies = false, bool showTypeFullName = false)
        {
            InterfaceType = interfaceType;
            SearchAllAssemblies = searchAllAssemblies;
            ShowTypeFullName = showTypeFullName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementedTypesFiledAttribute"/> class.
        /// </summary>
        /// <param name="assemblyName">The name of assembly.</param>
        /// <param name="interfaceTypeFullName">The full name of the <see cref="Type"/> of interface.</param>
        /// <param name="searchAllAssemblies">If search <see cref="Type"/> s in  all assemblies.</param>
        /// <param name="showTypeFullName">If show full name of <see cref="Type"/>.</param>
        public ImplementedTypesFiledAttribute(string assemblyName, string interfaceTypeFullName, bool searchAllAssemblies = false, bool showTypeFullName = false)
        {
            AssemblyName = assemblyName;
            InterfaceTypeFullName = interfaceTypeFullName;
            SearchAllAssemblies = searchAllAssemblies;
            ShowTypeFullName = showTypeFullName;
        }
        
        /// <summary>
        /// The <see cref="Type"/> of interface.
        /// </summary>
        public Type InterfaceType { get; }

        /// <summary>
        /// If show full name of <see cref="Type"/>.
        /// </summary>
        public bool ShowTypeFullName { get; }

        /// <summary>
        /// If search <see cref="Type"/> s in  all assemblies.
        /// </summary>
        public bool SearchAllAssemblies { get; }

        /// <summary>
        /// The name of assembly.
        /// </summary>
        public string AssemblyName { get; }

        /// <summary>
        /// The full name of the <see cref="Type"/> of interface.
        /// </summary>
        public string InterfaceTypeFullName { get; }
    }
}