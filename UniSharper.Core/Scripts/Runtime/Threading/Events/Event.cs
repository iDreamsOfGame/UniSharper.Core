// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;

namespace UniSharper.Threading.Events
{
    /// <summary>
    /// The Event class is used as the base class for the creation of Event objects, which are
    /// passed as parameters to event listeners when an event occurs.
    /// </summary>
    public class Event
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="context">The context object.</param>
        /// <exception cref="ArgumentNullException"><c>eventType</c> is <c>null</c> or <c>empty</c>.</exception>
        public Event(string eventType, object context = null)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            EventType = eventType;
            Context = context;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the context object.
        /// </summary>
        /// <value>The context object.</value>
        public object Context
        {
            get;
            set;
        }

        /// <summary>
        /// The type of event.
        /// </summary>
        /// <value>The type of event.</value>
        public string EventType
        {
            get;
            set;
        }

        #endregion Properties
    }
}