// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

#if UNITY_PARTICLE_SYSTEM_MODULE
using System;
using UniSharper.Extensions;
using UnityEngine;

// ReSharper disable ConvertIfStatementToNullCoalescingExpression

namespace UniSharper.Effect
{
    /// <summary>
    /// Implementation for controlling particle effect.
    /// </summary>
    [DisallowMultipleComponent]
    public class ParticleEffectController : MonoBehaviour, IParticleEffectController
    {
        [SerializeField, Tooltip("Whether check all ParticleSystems stopped in event method Update.")]
        private bool checkParticleSystemsStopped = true;
        
        [SerializeField]
        private ParticleEffectEvent startedEvent;

        [SerializeField]
        private ParticleEffectEvent pausedEvent;

        [SerializeField]
        private ParticleEffectEvent resumedEvent;

        [SerializeField]
        private ParticleEffectEvent stoppedEvent;
        
        [SerializeField]
        private ParticleEffectEvent loopPointReached;
        
        private Transform cachedTransform;
        
        private ParticleSystem particleSystemRoot;
        
        private ParticleSystem[] particleSystems;
        
        private float duration = -1f;

        private bool hasStarted;

        public Transform CachedTransform
        {
            get
            {
                try
                {
                    if (cachedTransform)
                        return cachedTransform;

                    cachedTransform = transform;
                    return cachedTransform;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        
        public ParticleSystem ParticleSystemRoot
        {
            get
            {
                if (particleSystemRoot)
                    return particleSystemRoot;
                
                SetParticleSystemRoot();
                return particleSystemRoot;
            }
        }
        
        public ParticleSystem[] ParticleSystems
        {
            get
            {
                if (particleSystems != null)
                    return particleSystems;

                SetParticleSystems();
                return particleSystems;
            }
        }

        public bool CheckParticleSystemsStopped
        {
            get => checkParticleSystemsStopped;
            set => checkParticleSystemsStopped = value;
        }

        public float Duration
        {
            get
            {
                if (duration < 0)
                    SetDuration();

                return duration;
            }
        }
        
        public bool HasInitialized { get; protected set; }

        public bool IsLoop { get; protected set; }

        public float PlaybackTime { get; private set; }
        
        public ParticleEffectEvent Started
        {
            get
            {
                if (startedEvent == null)
                    startedEvent = new ParticleEffectEvent();

                return startedEvent;
            }
        }

        public ParticleEffectEvent Paused
        {
            get
            {
                if (pausedEvent == null)
                    pausedEvent = new ParticleEffectEvent();

                return pausedEvent;
            }
        }

        public ParticleEffectEvent Resumed
        {
            get
            {
                if (resumedEvent == null)
                    resumedEvent = new ParticleEffectEvent();

                return resumedEvent;
            }
        }

        public ParticleEffectEvent Stopped
        {
            get
            {
                if (stoppedEvent == null)
                    stoppedEvent = new ParticleEffectEvent();

                return stoppedEvent;
            }
        }

        public ParticleEffectEvent LoopPointReached
        {
            get
            {
                if (loopPointReached == null)
                    loopPointReached = new ParticleEffectEvent();

                return loopPointReached;
            }
        }

        public virtual void Initialize()
        {
            if (HasInitialized)
                return;

            SetCachedTransform();
            SetParticleSystemRoot();
            SetParticleSystems();
            SetDuration();
            HasInitialized = true;
        }
        
        public void Play()
        {
            if (!ParticleSystemRoot)
                return;

            Initialize();

            if (!ParticleSystemRoot.main.playOnAwake)
            {
                InternalStop(false);
                
                if (!ParticleSystemRoot.isStopped) 
                    return;
                
                ParticleSystemRoot.Play();
            }
            
            PlaybackTime = 0;
            hasStarted = true;
            FireEventStarted();
        }
        
        public void Pause()
        {
            if (!ParticleSystemRoot)
                return;

            if (ParticleSystemRoot.isPlaying)
            {
                ParticleSystemRoot.Pause();
                FireEventPaused();
            }
            else
            {
                Debug.LogWarning("Can not pause particle effect, cause the state of ParticleSystem is not playing!");
            }
        }

        public void Resume()
        {
            if (!ParticleSystemRoot)
                return;

            if (ParticleSystemRoot.isPaused)
            {
                ParticleSystemRoot.Play();
                FireEventResumed();
            }
            else
            {
                Debug.LogWarning("Can not resume particle effect, cause the state of ParticleSystem is not paused!");
            }
        }

        public void Stop()
        {
            InternalStop();
        }
        
        public void RemoveAllListeners()
        {
            Started?.RemoveAllListeners();
            Paused?.RemoveAllListeners();
            Resumed?.RemoveAllListeners();
            Stopped?.RemoveAllListeners();
            LoopPointReached?.RemoveAllListeners();
        }

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
            RemoveAllListeners();
        }

        protected virtual void Update()
        {
            if (!ParticleSystemRoot)
                return;

            if (!hasStarted || ParticleSystemRoot.isPaused)
                return;

            PlaybackTime += Time.deltaTime;

            if (CheckParticleSystemsStopped)
            {
                var allParticleSystemStopped = true;
                foreach (var childParticleSystem in ParticleSystems)
                {
                    if (childParticleSystem.isStopped) 
                        continue;
                
                    allParticleSystemStopped = false;
                    break;
                }

                if (allParticleSystemStopped)
                {
                    OnParticleSystemsStopped();
                    return;
                }
            }
            
            if (PlaybackTime >= Duration)
                OnParticleSystemsStopped();
        }
        
        protected virtual void OnParticleSystemsStopped()
        {
            if (IsLoop)
            {
                FireEventLoopPointReached();
            }
            else
            {
                hasStarted = false;
                FireEventStopped();
            }
            
            PlaybackTime = 0;
        }

        protected virtual void SetCachedTransform()
        {
            if (cachedTransform)
                return;
            
            cachedTransform = transform;
        }
        
        protected virtual void SetParticleSystemRoot()
        {
            if (particleSystemRoot)
                return;
            
            TryGetComponent(out particleSystemRoot);

            if (!particleSystemRoot)
                particleSystemRoot = transform.GetComponentInChildren<ParticleSystem>(true);

            IsLoop = particleSystemRoot?.main.loop ?? false;
        }

        protected virtual void SetParticleSystems()
        {
            if (particleSystems != null)
                return;
            
            particleSystems = GetComponentsInChildren<ParticleSystem>(true);
        }

        protected virtual void SetDuration()
        {
            foreach (var childParticleSystem in ParticleSystems)
            {
                duration = Mathf.Max(duration, childParticleSystem.GetDuration(true));
            }
        }

        private void FireEventLoopPointReached()
        {
            LoopPointReached?.Invoke(this);
        }

        private void FireEventPaused()
        {
            Paused?.Invoke(this);
        }

        private void FireEventResumed()
        {
            Resumed?.Invoke(this);
        }

        private void FireEventStarted()
        {
            Started?.Invoke(this);
        }

        private void FireEventStopped()
        {
            Stopped?.Invoke(this);
        }

        private void InternalStop(bool fireStoppedEvent = true)
        {
            if (!ParticleSystemRoot)
                return;

            if (ParticleSystemRoot.isPlaying)
                ParticleSystemRoot.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            if (fireStoppedEvent)
                FireEventStopped();

            hasStarted = false;
        }
    }
}
#endif