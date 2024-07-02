// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UniSharper.Patterns;
using UnityEngine;

// ReSharper disable ConvertIfStatementToNullCoalescingExpression

namespace UniSharper.Timers
{
    /// <summary>
    /// The <see cref="TimerManager"/> is a convenience class for managing all <see cref="ITimer"/>
    /// s at runtime. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="SingletonMonoBehaviour{TimerManager}"/>
    /// <seealso cref="ITimerList"/>
    public sealed class TimerManager : SingletonMonoBehaviour<TimerManager>
    {
        private ITimerList timerList;
        
        /// <summary>
        /// Gets the number of <see cref="ITimer"/> elements contained in the <see cref="TimerManager"/>.
        /// </summary>
        /// <value>The number of <see cref="ITimer"/> elements contained in the <see cref="TimerManager"/>.</value>
        public int Count => timerList?.Count ?? 0;

        /// <summary>
        /// Gets the timer list.
        /// </summary>
        /// <value>The timer list.</value>
        private ITimerList TimerList
        {
            get
            {
                if (timerList == null)
                    timerList = new TimerGroup();

                return timerList;
            }
        }

        /// <summary>
        /// Adds an <see cref="ITimer"/> item.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to be added.</param>
        public void Add(ITimer timer) => TimerList.Add(timer);

        /// <summary>
        /// Removes all <see cref="ITimer"/> items.
        /// </summary>
        public void Clear() => TimerList.Clear();

        /// <summary>
        /// Determines whether the <see cref="TimerManager"/> contains a specific <see cref="ITimer"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to locate in the <see cref="TimerManager"/>.</param>
        /// <returns>
        /// <c>true</c> if <see cref="ITimer"/> item is found in the <see cref="TimerManager"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(ITimer timer) => TimerList.Contains(timer);

        /// <summary>
        /// Pauses all timers in the <see cref="TimerManager"/>.
        /// </summary>
        public void PauseAll() => TimerList.PauseAll();

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="TimerManager"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to be removed.</param>
        /// <returns>
        /// <c>true</c> if item was successfully removed; otherwise, <c>false</c>. This method also
        /// returns <c>false</c> if item is not found in the <see cref="TimerManager"/>.
        /// </returns>
        public bool Remove(ITimer timer) => TimerList.Remove(timer);

        /// <summary>
        /// Resets all timers in the <see cref="TimerManager"/>.
        /// </summary>
        public void ResetAll() => TimerList.ResetAll();

        /// <summary>
        /// Resumes all timers in <see cref="TimerManager"/>.
        /// </summary>
        public void ResumeAll() => TimerList.ResumeAll();

        /// <summary>
        /// Starts all timers in the <see cref="TimerManager"/>.
        /// </summary>
        public void StartAll() => TimerList.StartAll();

        /// <summary>
        /// Stops all timers in the <see cref="TimerManager"/>.
        /// </summary>
        public void StopAll() => TimerList.StopAll();

        /// <summary>
        /// This function is called when the application pauses.
        /// </summary>
        /// <param name="pauseStatus"><c>true</c> if the application is paused, else <c>false</c>.</param>
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                // Pause all timers.
                TimerList.PauseAll(true);
            }
            else
            {
                // Resume all timers.
                ResumeAll();
            }
        }

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        private void Update()
        {
            timerList?.ForEach(timer =>
            {
                var deltaTime = timer.IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;

                try
                {
                    timer.Tick(deltaTime);
                }
                catch (Exception exception)
                {
                    Debug.LogWarning(exception);
                }
            });
        }
    }
}