// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace UniSharper.Timers
{
    /// <summary>
    /// Class used internally to store a group of <see cref="ITimer"/>.
    /// </summary>
    /// <seealso cref="ITimerList"/>
    public class TimerGroup : ITimerList
    {
        #region Fields

        private Queue<ITimer> addedTimers;
        private Queue<ITimer> removedTimers;
        private List<ITimer> timers;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerGroup"/> class.
        /// </summary>
        public TimerGroup()
        {
            if (timers == null)
            {
                timers = new List<ITimer>();
            }

            addedTimers = new Queue<ITimer>();
            removedTimers = new Queue<ITimer>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerGroup"/> class.
        /// </summary>
        /// <param name="timers">The timers array.</param>
        public TimerGroup(params ITimer[] timers)
            : this()
        {
            this.timers = new List<ITimer>(timers);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of <see cref="ITimer"/> contained in this <see cref="TimerGroup"/>.
        /// </summary>
        /// <value>The number of <see cref="ITimer"/> contained in this <see cref="TimerGroup"/>.</value>
        public int Count
        {
            get
            {
                return timers.Count;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an <see cref="ITimer"/> item to this <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to add.</param>
        /// <exception cref="ArgumentNullException"><c>timer</c> is <c>null</c>.</exception>
        public void Add(ITimer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            addedTimers.Enqueue(timer);
        }

        /// <summary>
        /// Removes all <see cref="ITimer"/> contained in this <see cref="TimerGroup"/>.
        /// </summary>
        public void Clear()
        {
            removedTimers = new Queue<ITimer>(timers);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ITimer"/> contained in this <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to locate.</param>
        /// <returns>
        /// <c>true</c> if <see cref="ITimer"/> is found in this <see cref="TimerGroup"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><c>timer</c> is <c>null</c>.</exception>
        public bool Contains(ITimer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            return timers.Contains(timer);
        }

        /// <summary>
        /// Performs the specified action on each <see cref="ITimer"/> of the <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Action{ITimer}"/> delegate to perform on each <see cref="ITimer"/> of the
        /// <see cref="TimerGroup"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><c>action</c> is <c>null</c>.</exception>
        public void ForEach(Action<ITimer> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            while (addedTimers.Count > 0)
            {
                ITimer timer = addedTimers.Dequeue();
                timers.Add(timer);
            }

            while (removedTimers.Count > 0)
            {
                ITimer timer = removedTimers.Dequeue();
                timers.Remove(timer);
            }

            foreach (ITimer timer in timers)
            {
                if (timer != null)
                {
                    action.Invoke(timer);
                }
            }
        }

        /// <summary>
        /// Pauses all timers contained in the <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="causedByApplicationPaused">
        /// if set to <c>true</c> invoke this method caused by application paused; otherwise, set <c>false</c>.
        /// </param>
        public void PauseAll(bool causedByApplicationPaused = false)
        {
            ForEach((timer) =>
            {
                timer.Pause(causedByApplicationPaused);
            });
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to be removed.</param>
        /// <returns>
        /// <c>true</c> if item was successfully removed from the <see cref="TimerGroup"/>;
        /// otherwise, <c>false</c>. This method also returns <c>false</c> if item is not found in
        /// the original <see cref="TimerGroup"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">timer</exception>
        /// <exception cref="ArgumentNullException"><c>timer</c> is <c>null</c>.</exception>
        public bool Remove(ITimer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            removedTimers.Enqueue(timer);
            return true;
        }

        /// <summary>
        /// Resets all timers contained in the <see cref="TimerGroup"/>.
        /// </summary>
        public void ResetAll()
        {
            ForEach((timer) =>
            {
                timer.Reset();
            });
        }

        /// <summary>
        /// Resumes all timers contained in <see cref="TimerGroup"/>.
        /// </summary>
        public void ResumeAll()
        {
            ForEach((timer) =>
            {
                timer.Resume();
            });
        }

        /// <summary>
        /// Starts all timers contained in the <see cref="TimerGroup"/>.
        /// </summary>
        public void StartAll()
        {
            ForEach((timer) =>
            {
                timer.Start();
            });
        }

        /// <summary>
        /// Stops all timers contained in the <see cref="TimerGroup"/>.
        /// </summary>
        public void StopAll()
        {
            ForEach((timer) =>
            {
                timer.Stop();
            });
        }

        #endregion Methods
    }
}