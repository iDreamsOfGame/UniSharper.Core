// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Collections.Generic;

namespace UniSharper.Threading.Events
{
    /// <summary>
    /// This class provides event dispatcher and listen between Unity main thread and sub-thread.
    /// Since Unity do not allow
    /// </summary>
    /// <seealso cref="IThreadEventDispatcher"/>
    public class ThreadEventDispatcher : IThreadEventDispatcher
    {
        #region Fields

        private readonly object syncRoot = new object();
        private Queue<Event> eventQueue;
        private bool isPending = false;
        private Dictionary<string, List<Action<Event>>> listeners = null;
        private Queue<Event> pendingEventQueue;
        private Dictionary<string, List<Action<Event>>> pendingListeners = null;
        private Dictionary<string, List<Action<Event>>> pendingRemovedListeners = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadEventDispatcher"/> class.
        /// </summary>
        public ThreadEventDispatcher()
        {
            listeners = new Dictionary<string, List<Action<Event>>>();
            pendingListeners = new Dictionary<string, List<Action<Event>>>();
            pendingRemovedListeners = new Dictionary<string, List<Action<Event>>>();

            eventQueue = new Queue<Event>();
            pendingEventQueue = new Queue<Event>();

            if (Synchronizer.Instance != null)
            {
                Synchronizer.Instance.Add(this);
            }
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Finalizes an instance of the <see cref="ThreadEventDispatcher"/> class.
        /// </summary>
        ~ThreadEventDispatcher()
        {
            listeners = null;
            pendingListeners = null;

            eventQueue = null;
            pendingEventQueue = null;

            if (Synchronizer.Instance != null)
            {
                Synchronizer.Instance.Remove(this);
            }
        }

        #endregion Destructors

        #region Methods

        /// <summary>
        /// Registers an event listener to receive an event notification.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to handle the event.</param>
        /// <exception cref="ArgumentNullException">
        /// <para><c>eventType</c> is <c>null</c> or <c>empty</c>.</para>
        /// - or -
        /// <para><c>listener</c> is <c>null</c>.</para>
        /// </exception>
        public void AddEventListener(string eventType, Action<Event> listener)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

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
        /// Dispatches en <see cref="Event"/>.
        /// </summary>
        /// <param name="e">The <see cref="Event"/> object.</param>
        /// <exception cref="ArgumentNullException"><c>e</c> is <c>null</c>.</exception>
        public void DispatchEvent(Event e)
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
        /// <para><c>eventType</c> is <c>null</c> or <c>empty</c>.</para>
        /// - or -
        /// <para><c>listener</c> is <c>null</c>.</para>
        /// </exception>
        public bool HasEventListener(string eventType, Action<Event> listener)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

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
        /// <exception cref="ArgumentNullException"><c>eventType</c> is <c>null</c> or <c>empty</c>.</exception>
        public bool HasEventListeners(string eventType)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            return (listeners.ContainsKey(eventType) && listeners[eventType].Count > 0)
                || (pendingListeners.ContainsKey(eventType) && pendingListeners[eventType].Count > 0);
        }

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
                    pendingListeners = new Dictionary<string, List<Action<Event>>>(listeners);
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
        /// <para><c>eventType</c> is <c>null</c> or <c>empty</c>.</para>
        /// - or -
        /// <para><c>listener</c> is <c>null</c>.</para>
        /// </exception>
        public void RemoveEventListener(string eventType, Action<Event> listener)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

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
                        pendingListeners.AddUnique(eventType, new List<Action<Event>>());
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
        /// <exception cref="ArgumentNullException"><c>eventType</c> is <c>null</c> or <c>empty</c>.</exception>
        public void RemoveEventListeners(string eventType)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            lock (syncRoot)
            {
                if (isPending)
                {
                    if (listeners.ContainsKey(eventType))
                    {
                        if (pendingListeners.ContainsKey(eventType))
                        {
                            pendingListeners.Remove(eventType);
                        }

                        pendingListeners.AddUnique(eventType, listeners[eventType]);
                    }
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
                    Event e = eventQueue.Dequeue();

                    if (listeners.ContainsKey(e.EventType))
                    {
                        List<Action<Event>> handlers = listeners[e.EventType];

                        handlers.ForEach(listener =>
                        {
                            listener.Invoke(e);
                        });
                    }
                }

                isPending = false;
            }
        }

        private void AddNormalEventListener(string eventType, Action<Event> listener)
        {
            if (listeners != null)
            {
                listeners.AddUnique(eventType, new List<Action<Event>>());
                listeners[eventType].AddUnique(listener);
            }
        }

        private void AddPendingEventListener(string eventType, Action<Event> listener)
        {
            if (pendingListeners != null)
            {
                pendingListeners.AddUnique(eventType, new List<Action<Event>>());
                pendingListeners[eventType].AddUnique(listener);
            }
        }

        private void AddPendingEvents()
        {
            while (pendingEventQueue.Count > 0)
            {
                Event e = pendingEventQueue.Dequeue();
                eventQueue.Enqueue(e);
            }
        }

        private void HandlePendingRemovedEventListeners()
        {
            if (pendingRemovedListeners != null && pendingRemovedListeners.Count > 0)
            {
                foreach (KeyValuePair<string, List<Action<Event>>> kvp in pendingRemovedListeners)
                {
                    string eventType = kvp.Key;
                    List<Action<Event>> listeners = kvp.Value;

                    if (listeners != null && listeners.Count > 0)
                    {
                        foreach (Action<Event> listener in listeners)
                        {
                            RemoveNormalEventListener(eventType, listener);
                        }
                    }
                }

                pendingRemovedListeners.Clear();
            }
        }

        private bool HasEventListeners()
        {
            return listeners.Count > 0 || pendingListeners.Count > 0;
        }

        private void MergePendingEventListeners()
        {
            if (pendingListeners != null && pendingListeners.Count > 0)
            {
                foreach (KeyValuePair<string, List<Action<Event>>> kvp in pendingListeners)
                {
                    string eventType = kvp.Key;
                    List<Action<Event>> listeners = kvp.Value;

                    if (listeners != null && listeners.Count > 0)
                    {
                        foreach (Action<Event> listener in listeners)
                        {
                            AddNormalEventListener(eventType, listener);
                        }
                    }
                }

                pendingListeners.Clear();
            }
        }

        private void RemoveNormalEventListener(string eventType, Action<Event> listener)
        {
            if (listeners.ContainsKey(eventType))
            {
                List<Action<Event>> list = listeners[eventType];
                list.Remove(listener);
            }
        }

        #endregion Methods
    }
}