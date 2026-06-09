// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using UnityEngine;

namespace UniSharper.Pool
{
    /// <summary>
    /// This component allows you to pool GameObjects, giving you a very fast alternative to Instantiate and Destroy.
    /// Pools also have settings to preload, recycle, and set the spawn capacity, giving you lots of control over your spawning.
    /// </summary>
    [AddComponentMenu("UniSharper/Pool/Prefab Pool")]
    public class UniPrefabPool : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Serializable]
        public class DelayDespawningClone
        {
            [SerializeField]
            private GameObject clone;

            [SerializeField]
            private float life;

            public GameObject Clone
            {
                get => clone;
                set => clone = value;
            }
            
            public float Life
            {
                get => life;
                set => life = value;
            }
        }
        
        public enum ActionNotification
        {
            /// <summary>
            /// If you use this then you must rely on the OnEnable and OnDisable messages.
            /// </summary>
            None,
            
            /// <summary>
            /// The prefab clone is sent the OnSpawn and OnDespawn messages.
            /// </summary>
            SendMessage,
            
            /// <summary>
            /// The prefab clone and all its children are sent the OnSpawn and OnDespawn messages.
            /// </summary>
            BroadcastMessage,
            
            /// <summary>
            /// The prefab clone's components implementing IPooledGameObject are called.
            /// </summary>
            Interface,
            
            /// <summary>
            /// The prefab clone and all its child components implementing IPooledGameObject are called.
            /// </summary>
            BroadcastInterfaces
        }

        public enum PoolingStrategy
        {
            /// <summary>
            /// Despawned clones will be deactivated and placed under this GameObject.
            /// </summary>
            SetActive,
            
            /// <summary>
            /// Despawned clones will be placed under a deactivated GameObject and left alone.
            /// </summary>
            ChangeActiveInHierarchy
        }

        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private ActionNotification notification;

        [SerializeField]
        private PoolingStrategy strategy;

        [SerializeField]
        private int preload;

        [SerializeField]
        private int capacity;

        [SerializeField]
        private bool isCloneRecyclable;

        [SerializeField]
        private bool isPersistentPool;

        [SerializeField]
        private bool shouldNumberClone;

        [SerializeField]
        private bool isDebugMode;
        
        private Transform parentOfDeactivatedClones;

        public Transform ParentOfDeactivatedClones
        {
            get
            {
                if (!parentOfDeactivatedClones)
                {
                    const string gameObjectName = "Despawned Clones";
                    var go = new GameObject(gameObjectName);
                    go.SetActive(false);
                    parentOfDeactivatedClones = go.transform;
                    parentOfDeactivatedClones.SetParent(transform, false);
                }

                return parentOfDeactivatedClones;
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
        }
    }
}