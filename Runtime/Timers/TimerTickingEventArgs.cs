// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharper.Timers
{
    /// <summary>
    /// Provides data for the <see cref="ITimer.Ticking"/> event.
    /// </summary>
    /// <seealso cref="System.EventArgs"/>
    public class TimerTickingEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerTickingEventArgs"/> class with the
        /// ticking count of the <see cref="ITimer"/>.
        /// </summary>
        /// <param name="currentCount">The current ticking count of the <see cref="ITimer"/>.</param>
        public TimerTickingEventArgs(uint currentCount)
            : base()
        {
            CurrentCount = currentCount;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the current ticking count of the <see cref="ITimer"/>.
        /// </summary>
        /// <value>The current ticking count of the <see cref="ITimer"/>.</value>
        public uint CurrentCount
        {
            get;
            set;
        }

        #endregion Properties
    }
}