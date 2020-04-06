// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Collections;
using System.Collections.Generic;
using UniSharper.Patterns;

namespace UniSharper.Threading
{
    /// <summary>
    /// A <see cref="Synchronizer"/> representing a <see cref="UnityEngine.MonoBehaviour"/> to
    /// synchronize data between child threads and main threads. Implements the <see
    /// cref="UniSharper.Patterns.SingletonMonoBehaviour{UniSharper.Threading.Synchronizer}"/>
    /// Implements the <see cref="System.Collections.Generic.ICollection{UniSharper.Threading.ISynchronizedObject}"/>
    /// </summary>
    /// <seealso cref="UniSharper.Patterns.SingletonMonoBehaviour{UniSharper.Threading.Synchronizer}"/>
    /// <seealso cref="System.Collections.Generic.ICollection{UniSharper.Threading.ISynchronizedObject}"/>
    public class Synchronizer : SingletonMonoBehaviour<Synchronizer>, ICollection<ISynchronizedObject>
    {
        #region Fields

        private Queue<ISynchronizedObject> addedObjects;
        private Queue<ISynchronizedObject> removedObjects;
        private List<ISynchronizedObject> synchronizedObjects;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the number of objects contained in the <see cref="Synchronizer"/>.
        /// </summary>
        /// <value>The number of objects contained in the <see cref="Synchronizer"/>.</value>
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
        /// Gets a value indicating whether the <see cref="Synchronizer"/> is read-only.
        /// </summary>
        /// <value><c>true</c> if the <see cref="Synchronizer"/> is read-only; otherwise, <c>false</c>.</value>
        bool ICollection<ISynchronizedObject>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an object of <see cref="ISynchronizedObject"/> to the <see cref="Synchronizer"/>.
        /// </summary>
        /// <param name="item">
        /// The object of <see cref="ISynchronizedObject"/> to add to the <see cref="Synchronizer"/>.
        /// </param>
        public void Add(ISynchronizedObject item)
        {
            if (addedObjects != null)
            {
                addedObjects.Enqueue(item);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="Synchronizer"/>.
        /// </summary>
        public void Clear()
        {
            if (synchronizedObjects != null)
            {
                removedObjects = new Queue<ISynchronizedObject>(synchronizedObjects);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="Synchronizer"/> contains a specific object of <see cref="ISynchronizedObject"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="Synchronizer"/>.</param>
        /// <returns>
        /// <c>true</c> if <c>item</c> is found in the <see cref="Synchronizer"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(ISynchronizedObject item)
        {
            if (synchronizedObjects != null)
            {
                return synchronizedObjects.Contains(item);
            }

            return false;
        }

        /// <summary>
        /// Copies the objects of <see cref="ISynchronizedObject"/> to sychronize in the <see
        /// cref="Synchronizer"/> to an <see cref="System.Array"/>, starting at a particular <see
        /// cref="System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the elements
        /// copied from <see cref="Synchronizer"/>. The <see cref="System.Array"/> must have
        /// zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        public void CopyTo(ISynchronizedObject[] array, int arrayIndex)
        {
            if (synchronizedObjects != null)
            {
                synchronizedObjects.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<ISynchronizedObject> GetEnumerator()
        {
            if (synchronizedObjects != null)
            {
                return synchronizedObjects.GetEnumerator();
            }

            return null;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object of <see cref="ISynchronizedObject"/>
        /// from the <see cref="Synchronizer"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="Synchronizer"/>.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="item"/> was successfully removed from the <see
        /// cref="Synchronizer"/>; otherwise, <c>false</c>. This method also returns <c>false</c> if
        /// <paramref name="item"/> is not found in the original <see cref="Synchronizer"/>.
        /// </returns>
        public bool Remove(ISynchronizedObject item)
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
            synchronizedObjects = new List<ISynchronizedObject>();
            addedObjects = new Queue<ISynchronizedObject>();
            removedObjects = new Queue<ISynchronizedObject>();
        }

        private void Update()
        {
            lock ((synchronizedObjects as ICollection).SyncRoot)
            {
                while (addedObjects.Count > 0)
                {
                    ISynchronizedObject obj = addedObjects.Dequeue();
                    synchronizedObjects.Add(obj);
                }

                while (removedObjects.Count > 0)
                {
                    ISynchronizedObject obj = removedObjects.Dequeue();
                    synchronizedObjects.Remove(obj);
                }

                if (synchronizedObjects != null)
                {
                    foreach (ISynchronizedObject item in synchronizedObjects)
                    {
                        item.Synchronize();
                    }
                }
            }
        }

        #endregion Methods
    }
}