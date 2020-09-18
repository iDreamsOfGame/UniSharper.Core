// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using UniSharper.Patterns;

namespace UniSharper.Threading
{
    /// <summary>
    /// A <see cref="UnityThreadSynchronizer"/> representing a <see cref="UnityEngine.MonoBehaviour"/> to
    /// synchronize data between child threads and main threads. Implements the <see
    /// cref="UniSharper.Patterns.SingletonMonoBehaviour{UniSharper.Threading.Synchronizer}"/>
    /// Implements the <see cref="System.Collections.Generic.ICollection{UniSharper.Threading.ISynchronizedObject}"/>
    /// </summary>
    /// <seealso cref="UniSharper.Patterns.SingletonMonoBehaviour{UniSharper.Threading.Synchronizer}"/>
    /// <seealso cref="System.Collections.Generic.ICollection{UniSharper.Threading.ISynchronizedObject}"/>
    public class UnityThreadSynchronizer : SingletonMonoBehaviour<UnityThreadSynchronizer>, ICollection<IThreadSynchronizedObject>
    {
        #region Fields

        private Queue<IThreadSynchronizedObject> addedObjects;
        private Queue<IThreadSynchronizedObject> removedObjects;
        private List<IThreadSynchronizedObject> synchronizedObjects;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the number of objects contained in the <see cref="UnityThreadSynchronizer"/>.
        /// </summary>
        /// <value>The number of objects contained in the <see cref="UnityThreadSynchronizer"/>.</value>
        public int Count
        {
            get
            {
                if (synchronizedObjects != null)
                {
                    return synchronizedObjects.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="UnityThreadSynchronizer"/> is read-only.
        /// </summary>
        /// <value><c>true</c> if the <see cref="UnityThreadSynchronizer"/> is read-only; otherwise, <c>false</c>.</value>
        bool ICollection<IThreadSynchronizedObject>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an object of <see cref="IThreadSynchronizedObject"/> to the <see cref="UnityThreadSynchronizer"/>.
        /// </summary>
        /// <param name="item">
        /// The object of <see cref="IThreadSynchronizedObject"/> to add to the <see cref="UnityThreadSynchronizer"/>.
        /// </param>
        public void Add(IThreadSynchronizedObject item)
        {
            if (addedObjects != null)
            {
                addedObjects.Enqueue(item);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="UnityThreadSynchronizer"/>.
        /// </summary>
        public void Clear()
        {
            if (synchronizedObjects != null)
            {
                removedObjects = new Queue<IThreadSynchronizedObject>(synchronizedObjects);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="UnityThreadSynchronizer"/> contains a specific object of <see cref="IThreadSynchronizedObject"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="UnityThreadSynchronizer"/>.</param>
        /// <returns>
        /// <c>true</c> if <c>item</c> is found in the <see cref="UnityThreadSynchronizer"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(IThreadSynchronizedObject item)
        {
            if (synchronizedObjects != null)
            {
                return synchronizedObjects.Contains(item);
            }

            return false;
        }

        /// <summary>
        /// Copies the objects of <see cref="IThreadSynchronizedObject"/> to sychronize in the <see
        /// cref="UnityThreadSynchronizer"/> to an <see cref="System.Array"/>, starting at a particular <see
        /// cref="System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the elements
        /// copied from <see cref="UnityThreadSynchronizer"/>. The <see cref="System.Array"/> must have
        /// zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        public void CopyTo(IThreadSynchronizedObject[] array, int arrayIndex)
        {
            synchronizedObjects?.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<IThreadSynchronizedObject> GetEnumerator()
        {
            return synchronizedObjects?.GetEnumerator();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object of <see cref="IThreadSynchronizedObject"/>
        /// from the <see cref="UnityThreadSynchronizer"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="UnityThreadSynchronizer"/>.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="item"/> was successfully removed from the <see
        /// cref="UnityThreadSynchronizer"/>; otherwise, <c>false</c>. This method also returns <c>false</c> if
        /// <paramref name="item"/> is not found in the original <see cref="UnityThreadSynchronizer"/>.
        /// </returns>
        public bool Remove(IThreadSynchronizedObject item)
        {
            removedObjects.Enqueue(item);
            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Awake()
        {
            synchronizedObjects = new List<IThreadSynchronizedObject>();
            addedObjects = new Queue<IThreadSynchronizedObject>();
            removedObjects = new Queue<IThreadSynchronizedObject>();
        }

        private void Update()
        {
            lock ((synchronizedObjects as ICollection).SyncRoot)
            {
                while (addedObjects.Count > 0)
                {
                    var obj = addedObjects.Dequeue();
                    synchronizedObjects.Add(obj);
                }

                while (removedObjects.Count > 0)
                {
                    IThreadSynchronizedObject obj = removedObjects.Dequeue();
                    synchronizedObjects.Remove(obj);
                }

                if (synchronizedObjects != null)
                {
                    foreach (IThreadSynchronizedObject item in synchronizedObjects)
                    {
                        item.Synchronize();
                    }
                }
            }
        }

        #endregion Methods
    }
}