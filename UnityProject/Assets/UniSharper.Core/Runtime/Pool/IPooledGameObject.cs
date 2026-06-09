// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace UniSharper.Pool
{
    /// <summary>
    /// If you implement this interface in a component on your pooled prefab,
    /// then the OnSpawn and OnDespawn methods will be automatically called when the associated UniGameObjectPool.Notification = Interface.
    /// </summary>
    public interface IPooledGameObject
    {
        /// <summary>
        /// Called when this poolable object is spawned.
        /// </summary>
        void OnSpawn();
        
        /// <summary>
        /// Called when this poolable object is despawned.
        /// </summary>
        void OnDespawn();
    }
}