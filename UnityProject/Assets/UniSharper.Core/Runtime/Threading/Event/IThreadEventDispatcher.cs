// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharper.Threading.Event
{
    /// <summary>
    /// The <see cref="IThreadEventDispatcher"/> interface defines methods for adding or removing
    /// event listeners, checks whether specific types of event listeners are registered, and
    /// dispatches events for child thread.
    /// </summary>
    /// <seealso cref="IThreadSynchronizedObject"/>
    public interface IThreadEventDispatcher : IThreadSynchronizedObject
    {
        /// <summary>
        /// Registers an event listener to receive an event notification.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to handle the event.</param>
        /// <exception cref="ArgumentNullException">
        /// <c>listener</c> is <c>null</c>.
        /// </exception>
        void AddEventListener(Enum eventType, Action<ThreadEvent> listener);

        /// <summary>
        /// Dispatches en <see cref="ThreadEvent"/>.
        /// </summary>
        /// <param name="e">The <see cref="ThreadEvent"/> object.</param>
        /// <exception cref="ArgumentNullException"><c>e</c> is <c>null</c>.</exception>
        void DispatchEvent(ThreadEvent e);

        /// <summary>
        /// Checks whether this <see cref="IThreadEventDispatcher"/> has the delegate listener
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
        bool HasEventListener(Enum eventType, Action<ThreadEvent> listener);

        /// <summary>
        /// Checks whether this <see cref="IThreadEventDispatcher"/> has listeners registered for a
        /// specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <returns>
        /// <c>true</c> if listeners of the specified type of event are registered; <c>false</c> otherwise.
        /// </returns>
        bool HasEventListeners(Enum eventType);

        /// <summary>
        /// Removes all event listeners of this <see cref="IThreadEventDispatcher"/>.
        /// </summary>
        void RemoveAllEventListeners();

        /// <summary>
        /// Removes a listener.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// <c>listener</c> is <c>null</c>.
        /// </exception>
        void RemoveEventListener(Enum eventType, Action<ThreadEvent> listener);

        /// <summary>
        /// Removes the event listeners registered for the specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        void RemoveEventListeners(Enum eventType);
    }
}