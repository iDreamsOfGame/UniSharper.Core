// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UniSharper.Threading
{
    /// <summary>
    /// The <see cref="IThreadSynchronizedObject"/> interface defines the object that need to synchronize
    /// data between child threads and main thread.
    /// </summary>
    public interface IThreadSynchronizedObject
    {
        /// <summary>
        /// Synchronizes data between threads.
        /// </summary>
        void Synchronize();
    }
}