// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections;
using System.Collections.Generic;

namespace UniSharper
{
    /// <summary>
    /// Support a simple chain execution for <see cref="IEnumerator"/>.
    /// </summary>
    /// <seealso cref="IEnumerator"/>
    public class CoroutineEnumerator : IEnumerator
    {
        #region Fields

        private readonly Queue<IEnumerator> coroutineQueue;

        private object current;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CoroutineEnumerator"/> class with some coroutines.
        /// </summary>
        /// <param name="coroutines">The coroutines.</param>
        public CoroutineEnumerator(params IEnumerator[] coroutines) => coroutineQueue = new Queue<IEnumerator>(coroutines);

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The element in the collection at the current position of the enumerator.</value>
        public object Current => current;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Enqueues the specified coroutine.
        /// </summary>
        /// <param name="coroutine">The coroutine.</param>
        public void Enqueue(IEnumerator coroutine) => coroutineQueue.Enqueue(coroutine);

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the enumerator was successfully advanced to the next element;
        /// <c>false</c> if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            while (coroutineQueue.Count > 0)
            {
                current = coroutineQueue.Dequeue();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset() => coroutineQueue.Clear();

        #endregion Methods
    }
}