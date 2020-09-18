// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharper.Timers
{
    /// <summary>
    /// Defines methods to manipulate timer object.
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    public interface ITimer : IDisposable
    {
        #region Events

        /// <summary>
        /// Occurs when the timer completed, ticking count equals to the <see cref="RepeatCount"/>.
        /// </summary>
        event TimerCompletedEventHandler Completed;

        /// <summary>
        /// Occurs when call the method <see cref="Pause"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerPausedEventHandler Paused;

        /// <summary>
        /// Occurs when call the method <see cref="Reset"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerResetedEventHandler Reseted;

        /// <summary>
        /// Occurs when call the method <see cref="Resume"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerResumedEventHandler Resumed;

        /// <summary>
        /// Occurs when call the method <see cref="Start"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerStartedEventHandler Started;

        /// <summary>
        /// Occurs when call the method <see cref="Stop"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerStoppedEventHandler Stopped;

        /// <summary>
        /// Occurs when the specified timer interval has elapsed.
        /// </summary>
        event TimerTickingEventHandler Ticking;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether accept application pause.
        /// </summary>
        /// <value><c>true</c> if accept application pause; otherwise, <c>false</c>.</value>
        bool CanAcceptApplicationPause
        {
            get; set;
        }

        /// <summary>
        /// Gets the current ticking count of <see cref="ITimer"/>.
        /// </summary>
        /// <value>The current ticking count of <see cref="ITimer"/>.</value>
        uint CurrentCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="ITimer"/> ignore time scale of Unity.
        /// </summary>
        /// <value><c>true</c> if ignore time scale of Unity; otherwise, <c>false</c>.</value>
        bool IgnoreTimeScale
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the time, in seconds, between <see cref="Ticking"/> events.
        /// </summary>
        /// <value>The time, in seconds, between <see cref="Ticking"/> events.</value>
        float Interval
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the repeat count of <see cref="ITimer"/>.
        /// </summary>
        /// <value>The repeat count of <see cref="ITimer"/>.</value>
        uint RepeatCount
        {
            get; set;
        }

        /// <summary>
        /// Gets the state of the <see cref="ITimer"/>.
        /// </summary>
        /// <value>The state of the <see cref="ITimer"/>.</value>
        TimerState TimerState
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Pauses timing.
        /// </summary>
        /// <param name="causedByApplicationPaused">
        /// if set to <c>true</c> invoke this method caused by application paused; otherwise, set <c>false</c>.
        /// </param>
        void Pause(bool causedByApplicationPaused = false);

        /// <summary>
        /// Resets the state of <see cref="ITimer"/>.
        /// </summary>
        void Reset();

        /// <summary>
        /// Resumes timing.
        /// </summary>
        void Resume();

        /// <summary>
        /// Starts timing.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops timing.
        /// </summary>
        void Stop();

        /// <summary>
        /// Updates the time of timing by delta time.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        void Tick(float deltaTime);

        #endregion Methods
    }
}