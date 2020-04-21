// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharper.Timers
{
    /// <summary>
    /// Represents the method that will handle the <see cref="ITimer.Completed"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing no event data.</param>
    public delegate void TimerCompletedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="ITimer.Paused"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing no event data.</param>
    public delegate void TimerPausedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="ITimer.Reseted"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing no event data.</param>
    public delegate void TimerResetedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="ITimer.Resumed"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing no event data.</param>
    public delegate void TimerResumedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="ITimer.Started"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing no event data.</param>
    public delegate void TimerStartedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="ITimer.Stopped"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing no event data.</param>
    public delegate void TimerStoppedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="ITimer.Ticking"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing no event data.</param>
    public delegate void TimerTickingEventHandler(object sender, TimerTickingEventArgs e);
}