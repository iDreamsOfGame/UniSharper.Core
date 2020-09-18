// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UniSharper.Timers
{
    /// <summary>
    /// Defines the states of <see cref="ITimer"/>.
    /// </summary>
    public enum TimerState
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Represents the timer is in running state.
        /// </summary>
        Running,

        /// <summary>
        /// Represents the timer is in pausing state.
        /// </summary>
        Pause,

        /// <summary>
        /// Represents the timer is in stop state.
        /// </summary>
        Stop
    }
}