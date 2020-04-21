// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharper.Timers
{
    /// <summary>
    /// Base implementation of interface <see cref="ITimer"/>.
    /// </summary>
    /// <seealso cref="ITimer"/>
    public class Timer : ITimer
    {
        #region Fields

        private bool disposed;

        private float time;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class, and sets the Interval
        /// property to the specified number of seconds, the RepeatCount property, the
        /// IgnoreTimeScale property and a <see cref="bool"/> value to determine to invoke the
        /// method <see cref="Start"/> automatically.
        /// </summary>
        /// <param name="interval">The time, in seconds, between <see cref="Ticking"/> events.</param>
        /// <param name="repeatCount">The repeat count.</param>
        /// <param name="ignoreTimeScale">A value indicating whether to ignore time scale of Unity.</param>
        /// <param name="canAcceptApplicationPause">
        /// if set to <c>true</c> can accept timer pause caused by application pause; otherwise, <c>false</c>.
        /// </param>
        public Timer(float interval, uint repeatCount = 0, bool ignoreTimeScale = false, bool canAcceptApplicationPause = true)
        {
            TimerState = TimerState.Stop;
            time = 0f;

            Interval = interval;
            RepeatCount = repeatCount;
            IgnoreTimeScale = ignoreTimeScale;
            CanAcceptApplicationPause = canAcceptApplicationPause;

            Initialize();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when the timer completed, ticking count equals to the <see cref="RepeatCount"/>.
        /// </summary>
        public event TimerCompletedEventHandler Completed;

        /// <summary>
        /// Occurs when call the method <see cref="Pause"/>.
        /// </summary>
        public event TimerPausedEventHandler Paused;

        /// <summary>
        /// Occurs when call the method <see cref="Reset"/>.
        /// </summary>
        public event TimerResetedEventHandler Reseted;

        /// <summary>
        /// Occurs when call the method <see cref="Resume"/>.
        /// </summary>
        public event TimerResumedEventHandler Resumed;

        /// <summary>
        /// Occurs when call the method <see cref="Start"/>.
        /// </summary>
        public event TimerStartedEventHandler Started;

        /// <summary>
        /// Occurs when call the method <see cref="Stop"/>.
        /// </summary>
        public event TimerStoppedEventHandler Stopped;

        /// <summary>
        /// Occurs when the specified timer interval has elapsed.
        /// </summary>
        public event TimerTickingEventHandler Ticking;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether accept application pause.
        /// </summary>
        /// <value><c>true</c> if accept application pause; otherwise, <c>false</c>.</value>
        public bool CanAcceptApplicationPause
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current ticking count of <see cref="Timer"/>.
        /// </summary>
        /// <value>The current ticking count of <see cref="Timer"/>.</value>
        public uint CurrentCount
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Timer"/> ignore time scale of Unity.
        /// </summary>
        /// <value><c>true</c> if ignore time scale of Unity; otherwise, <c>false</c>.</value>
        public bool IgnoreTimeScale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time, in seconds, between <see cref="Ticking"/> events.
        /// </summary>
        /// <value>The time, in seconds, between <see cref="Ticking"/> events.</value>
        public float Interval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the repeat count of <see cref="Timer"/>.
        /// </summary>
        /// <value>The repeat count of <see cref="Timer"/>.</value>
        public uint RepeatCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the state of the <see cref="Timer"/>.
        /// </summary>
        /// <value>The state of the <see cref="Timer"/>.</value>
        public TimerState TimerState
        {
            get;
            protected set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Pauses timing.
        /// </summary>
        /// <param name="causedByApplicationPaused">
        /// if set to <c>true</c> invoke this method caused by application paused; otherwise, set <c>false</c>.
        /// </param>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Pause(bool causedByApplicationPaused = false)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (!CanAcceptApplicationPause && causedByApplicationPaused)
            {
                return;
            }

            if (TimerState == TimerState.Running)
            {
                TimerState = TimerState.Pause;

                if (Paused != null)
                {
                    Paused.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the state of <see cref="ITimer"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Reset()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            Stop();

            CurrentCount = 0;
            time = 0f;

            if (Reseted != null)
            {
                Reseted.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Resumes timing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Resume()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (TimerState == TimerState.Pause)
            {
                TimerState = TimerState.Running;

                if (Resumed != null)
                {
                    Resumed.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Starts timing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Start()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (TimerState != TimerState.Running)
            {
                TimerState = TimerState.Running;

                if (Started != null)
                {
                    Started.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Stops timing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Stop()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (TimerState != TimerState.Stop)
            {
                TimerState = TimerState.Stop;

                if (Stopped != null)
                {
                    Stopped.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Updates the time of timing by delta time.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Tick(float deltaTime)
        {
            if (TimerState != TimerState.Running)
            {
                return;
            }

            time += deltaTime;

            if (time >= Interval)
            {
                CurrentCount++;

                // Raise Ticking event
                if (Ticking != null)
                {
                    Ticking.Invoke(this, new TimerTickingEventArgs(CurrentCount));
                }

                if (RepeatCount != 0 && CurrentCount >= RepeatCount)
                {
                    Reset();

                    if (Completed != null)
                    {
                        Completed.Invoke(this, EventArgs.Empty);
                    }
                }

                time = time - Interval;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                if (TimerManager.Instance != null)
                {
                    TimerManager.Instance.Remove(this);
                }
            }

            disposed = true;
            TimerState = TimerState.Stop;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual void Initialize()
        {
            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.Add(this);
            }
        }

        #endregion Methods
    }
}