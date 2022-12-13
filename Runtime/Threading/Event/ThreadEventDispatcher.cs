// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using ReSharp.Extensions;

namespace UniSharper.Threading.Event
{
    /// <summary>
    /// This class provides event dispatcher and listen between Unity main thread and sub-thread.
    /// Since Unity do not allow
    /// </summary>
    /// <seealso cref="IThreadEventDispatcher"/>
    public class ThreadEventDispatcher : IThreadEventDispatcher
    {
        private readonly Queue<ThreadEvent> eventQueue;

        private readonly Dictionary<Enum, List<Action<ThreadEvent>>> listeners;

        private readonly Queue<ThreadEvent> pendingEventQueue;

        private readonly Dictionary<Enum, List<Action<ThreadEvent>>> pendingRemovedListeners;

        private readonly object syncRoot = new();

        private bool isPending;

        private Dictionary<Enum, List<Action<ThreadEvent>>> pendingListeners;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadEventDispatcher"/> class.
        /// </summary>
        public ThreadEventDispatcher()
        {
            listeners = new Dictionary<Enum, List<Action<ThreadEvent>>>();
            pendingListeners = new Dictionary<Enum, List<Action<ThreadEvent>>>();
            pendingRemovedListeners = new Dictionary<Enum, List<Action<ThreadEvent>>>();

            eventQueue = new Queue<ThreadEvent>();
            pendingEventQueue = new Queue<ThreadEvent>();

            if (UnityThreadSynchronizer.Instance != null)
            {
                UnityThreadSynchronizer.Instance.Add(this);
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ThreadEventDispatcher"/> class.
        /// </summary>
        ~ThreadEventDispatcher()
        {
            if (UnityThreadSynchronizer.Instance != null)
            {
                UnityThreadSynchronizer.Instance.Remove(this);
            }
        }

        /// <summary>
        /// Registers an event listener to receive an event notification.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to handle the event.</param>
        /// <exception cref="ArgumentNullException">
        /// <c>listener</c> is <c>null</c>.
        /// </exception>
        public void AddEventListener(Enum eventType, Action<ThreadEvent> listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            lock (syncRoot)
            {
                if (isPending)
                {
                    AddPendingEventListener(eventType, listener);
                }
                else
                {
                    AddNormalEventListener(eventType, listener);
                }
            }
        }

        /// <summary>
        /// Dispatches en <see cref="ThreadEvent"/>.
        /// </summary>
        /// <param name="e">The <see cref="ThreadEvent"/> object.</param>
        /// <exception cref="ArgumentNullException"><c>e</c> is <c>null</c>.</exception>
        public void DispatchEvent(ThreadEvent e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            lock (syncRoot)
            {
                if (!HasEventListeners(e.EventType))
                {
                    return;
                }

                if (isPending)
                {
                    pendingEventQueue.Enqueue(e);
                }
                else
                {
                    eventQueue.Enqueue(e);
                }
            }
        }

        /// <summary>
        /// Checks whether this <see cref="ThreadEventDispatcher"/> has the delegate listener
        /// registered for a specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to locate.</param>
        /// <returns>
        /// <c>true</c> if a listener of the specified type of event is registered; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <c>listener</c> is <c>null</c>.
        /// </exception>
        public bool HasEventListener(Enum eventType, Action<ThreadEvent> listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            return (listeners.ContainsKey(eventType) && listeners[eventType].Contains(listener))
                || (pendingListeners.ContainsKey(eventType) && pendingListeners[eventType].Contains(listener));
        }

        /// <summary>
        /// Checks whether this <see cref="ThreadEventDispatcher"/> has listeners registered for a
        /// specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <returns>
        /// <c>true</c> if listeners of the specified type of event are registered; <c>false</c> otherwise.
        /// </returns>
        public bool HasEventListeners(Enum eventType) => (listeners.ContainsKey(eventType) && listeners[eventType].Count > 0)
                   || (pendingListeners.ContainsKey(eventType) && pendingListeners[eventType].Count > 0);

        /// <summary>
        /// Removes all event listeners of this <see cref="ThreadEventDispatcher"/>.
        /// </summary>
        public void RemoveAllEventListeners()
        {
            if (!HasEventListeners())
            {
                return;
            }

            lock (syncRoot)
            {
                if (isPending)
                {
                    pendingListeners = new Dictionary<Enum, List<Action<ThreadEvent>>>(listeners);
                }
                else
                {
                    listeners.Clear();
                }
            }
        }

        /// <summary>
        /// Removes a listener.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// <c>listener</c> is <c>null</c>.
        /// </exception>
        public void RemoveEventListener(Enum eventType, Action<ThreadEvent> listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            lock (syncRoot)
            {
                if (isPending)
                {
                    if (!pendingListeners.ContainsKey(eventType) || !pendingListeners[eventType].Contains(listener))
                    {
                        pendingListeners.AddUnique(eventType, new List<Action<ThreadEvent>>());
                        pendingListeners[eventType].AddUnique(listener);
                    }
                }
                else
                {
                    RemoveNormalEventListener(eventType, listener);
                }
            }
        }

        /// <summary>
        /// Removes the event listeners registered for the specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        public void RemoveEventListeners(Enum eventType)
        {
            lock (syncRoot)
            {
                if (isPending)
                {
                    if (!listeners.ContainsKey(eventType))
                        return;

                    if (pendingListeners.ContainsKey(eventType))
                    {
                        pendingListeners.Remove(eventType);
                    }

                    pendingListeners.AddUnique(eventType, listeners[eventType]);
                }
                else
                {
                    listeners.Remove(eventType);
                }
            }
        }

        /// <summary>
        /// Synchronizes data between threads.
        /// </summary>
        public void Synchronize()
        {
            lock (syncRoot)
            {
                HandlePendingRemovedEventListeners();
                MergePendingEventListeners();
                AddPendingEvents();

                isPending = true;

                while (eventQueue.Count > 0)
                {
                    var e = eventQueue.Dequeue();
                    if (!listeners.ContainsKey(e.EventType))
                        continue;

                    var handlers = listeners[e.EventType];

                    handlers.ForEach(listener =>
                    {
                        listener.Invoke(e);
                    });
                }

                isPending = false;
            }
        }

        private void AddNormalEventListener(Enum eventType, Action<ThreadEvent> listener)
        {
            if (listeners == null)
                return;

            listeners.AddUnique(eventType, new List<Action<ThreadEvent>>());
            listeners[eventType].AddUnique(listener);
        }

        private void AddPendingEventListener(Enum eventType, Action<ThreadEvent> listener)
        {
            if (pendingListeners == null)
                return;

            pendingListeners.AddUnique(eventType, new List<Action<ThreadEvent>>());
            pendingListeners[eventType].AddUnique(listener);
        }

        private void AddPendingEvents()
        {
            while (pendingEventQueue.Count > 0)
            {
                var e = pendingEventQueue.Dequeue();
                eventQueue.Enqueue(e);
            }
        }

        private void HandlePendingRemovedEventListeners()
        {
            if (pendingRemovedListeners == null || pendingRemovedListeners.Count == 0)
                return;

            foreach (var (eventType, actions) in pendingRemovedListeners)
            {
                if (actions == null || actions.Count == 0)
                    continue;

                foreach (var listener in actions)
                {
                    RemoveNormalEventListener(eventType, listener);
                }
            }

            pendingRemovedListeners.Clear();
        }

        private bool HasEventListeners() => listeners.Count > 0 || pendingListeners.Count > 0;

        private void MergePendingEventListeners()
        {
            if (pendingListeners == null || pendingListeners.Count == 0)
                return;

            foreach (var (eventType, actions) in pendingListeners)
            {
                if (actions == null || actions.Count == 0)
                    continue;

                foreach (var listener in actions)
                {
                    AddNormalEventListener(eventType, listener);
                }
            }

            pendingListeners.Clear();
        }

        private void RemoveNormalEventListener(Enum eventType, Action<ThreadEvent> listener)
        {
            if (!listeners.ContainsKey(eventType))
                return;

            var list = listeners[eventType];
            list.Remove(listener);
        }
    }
}