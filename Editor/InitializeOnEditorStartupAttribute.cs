// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;

namespace UniSharperEditor
{
    /// <summary>
    /// Allow an editor class to be initialized when Unity loads without action from the user.
    /// </summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Class)]
    internal class InitializeOnEditorStartupAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeOnEditorStartupAttribute"/> class.
        /// </summary>
        /// <param name="executionOrder">The execution order.</param>
        public InitializeOnEditorStartupAttribute(int executionOrder = 0)
            : base()
        {
            ExecutionOrder = executionOrder;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the execution order.
        /// </summary>
        /// <value>The execution order.</value>
        public int ExecutionOrder
        {
            get;
            private set;
        }

        #endregion Properties
    }
}