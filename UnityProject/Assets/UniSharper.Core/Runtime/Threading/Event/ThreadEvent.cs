// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharper.Threading.Event
{
    /// <summary>
    /// The ThreadEvent class is used as the base class for the creation of Event objects, which are
    /// passed as parameters to event listeners when an event occurs.
    /// </summary>
    public class ThreadEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadEvent"/> class.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="context">The context object.</param>
        public ThreadEvent(Enum eventType, object context = null)
        {
            EventType = eventType;
            Context = context;
        }

        /// <summary>
        /// Gets or sets the context object.
        /// </summary>
        /// <value>The context object.</value>
        public object Context { get; }

        /// <summary>
        /// The type of event.
        /// </summary>
        /// <value>The type of event.</value>
        public Enum EventType { get; }
    }
}