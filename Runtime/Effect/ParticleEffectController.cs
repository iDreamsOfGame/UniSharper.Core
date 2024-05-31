// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Effect
{
    /// <summary>
    /// Implementation for controlling particle effect.
    /// </summary>
    [DisallowMultipleComponent]
    public class ParticleEffectController : MonoBehaviour, IParticleEffectController
    {
        [SerializeField]
        private ParticleEffectEvent loopPointReached;

        [SerializeField]
        private ParticleEffectEvent pausedEvent;

        [SerializeField]
        private ParticleEffectEvent resumedEvent;

        [SerializeField]
        private ParticleEffectEvent startedEvent;

        [SerializeField]
        private ParticleEffectEvent stoppedEvent;
        
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

        public ParticleEffectEvent LoopPointReached => loopPointReached ??= new ParticleEffectEvent();

        public ParticleEffectEvent Paused => pausedEvent ??= new ParticleEffectEvent();

        public ParticleEffectEvent Resumed => resumedEvent ??= new ParticleEffectEvent();

        public ParticleEffectEvent Started => startedEvent ??= new ParticleEffectEvent();

        public ParticleEffectEvent Stopped => stoppedEvent ??= new ParticleEffectEvent();

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
            PlaybackTime = 0;

            InternalStop(false);

            if (!ParticleSystemRoot.isStopped) 
                return;
                
            ParticleSystemRoot.Play();
            
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

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
            DestroyEvents();
        }

        protected virtual void Update()
        {
            if (!ParticleSystemRoot)
                return;

            if (!hasStarted || ParticleSystemRoot.isPaused || ParticleSystemRoot.isStopped)
                return;

            PlaybackTime += Time.deltaTime;

            if (PlaybackTime < Duration)
                return;
            
            PlaybackTime = 0;
            
            if (IsLoop)
            {
                FireEventLoopPointReached();
            }
            else
            {
                hasStarted = false;
                FireEventStopped();
            }
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

        private void DestroyEvents()
        {
            Started?.RemoveAllListeners();
            Paused?.RemoveAllListeners();
            Resumed?.RemoveAllListeners();
            Stopped?.RemoveAllListeners();
            LoopPointReached?.RemoveAllListeners();
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
                ParticleSystemRoot.Stop();

            if (fireStoppedEvent)
                FireEventStopped();

            hasStarted = false;
        }
    }
}