// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See file LICENSE in
// the project root for license information.

using UnityEngine;

namespace UniSharper.Miscs
{
    /// <summary>
    /// Makes the <see cref="GameObject"/> not be destroyed automatically when loading a new scene.
    /// </summary>
    /// <seealso cref="MonoBehaviour"/>
    public sealed class DontDestroyOnLoad : MonoBehaviour
    {
        #region Fields

        private bool initialized;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (!initialized)
            {
                DontDestroyOnLoad(gameObject);
                initialized = true;
            }
        }

        #endregion Methods
    }
}