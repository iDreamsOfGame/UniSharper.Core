// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharper.Timers
{
    /// <summary>
    /// Represents a collection of timers.
    /// </summary>
    public interface ITimerList
    {
        #region Properties

        /// <summary>
        /// Gets the number of <see cref="ITimer"/> contained in this <see cref="ITimerList"/>.
        /// </summary>
        /// <value>The number of <see cref="ITimer"/> contained in this <see cref="ITimerList"/>.</value>
        int Count
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an <see cref="ITimer"/> item to this <see cref="ITimerList"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to add.</param>
        void Add(ITimer timer);

        /// <summary>
        /// Removes all <see cref="ITimer"/> contained in this <see cref="ITimerList"/>.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether the specified <see cref="ITimer"/> contained in this <see cref="ITimerList"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to locate.</param>
        /// <returns>
        /// <c>true</c> if <see cref="ITimer"/> is found in this <see cref="ITimerList"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        bool Contains(ITimer timer);

        /// <summary>
        /// Performs the specified action on each <see cref="ITimer"/> of the <see cref="ITimerList"/>.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Action{ITimer}"/> delegate to perform on each <see cref="ITimer"/> of the
        /// <see cref="ITimerList"/>.
        /// </param>
        void ForEach(Action<ITimer> action);

        /// <summary>
        /// Pauses all timers contained in this <see cref="ITimerList"/>.
        /// </summary>
        /// <param name="causedByApplicationPaused">
        /// if set to <c>true</c> invoke this method caused by application paused; otherwise, set <c>false</c>.
        /// </param>
        void PauseAll(bool causedByApplicationPaused = false);

        /// <summary>
        /// Removes the first occurrence of a specific <see cref="ITimer"/> from this <see cref="ITimerList"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to be removed.</param>
        /// <returns>
        /// <c>true</c> if <see cref="ITimer"/> was successfully removed from this <see
        /// cref="ITimerList"/>; otherwise, <c>false</c>. This method also returns <c>false</c> if
        /// the specified <see cref="ITimer"/> is not found.
        /// </returns>
        bool Remove(ITimer timer);

        /// <summary>
        /// Resets all timers contained in this <see cref="ITimerList"/>.
        /// </summary>
        void ResetAll();

        /// <summary>
        /// Resumes all timers contained in this <see cref="ITimerList"/>.
        /// </summary>
        void ResumeAll();

        /// <summary>
        /// Starts all timers contained in this <see cref="ITimerList"/>.
        /// </summary>
        void StartAll();

        /// <summary>
        /// Stops all timers contained in this <see cref="ITimerList"/>.
        /// </summary>
        void StopAll();

        #endregion Methods
    }
}