// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace UniSharper.Effect
{
    /// <summary>
    /// Implementation for controlling particle effect.
    /// </summary>
    [DisallowMultipleComponent]
    public class ParticleEffectController : MonoBehaviour, IParticleEffectController
    {
        private Transform cachedTransform;
        
        private ParticleSystem particleSystemRoot;
        
        private ParticleSystem[] particleSystems;
        
        private float duration = -1f;

        private bool started;

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
                if (!IsLoop && duration < 0)
                    SetDuration();

                return duration;
            }
        }

        public bool IsLoop { get; protected set; }
        
        public float PlaybackTime { get; private set; }

        public ParticleEffectEvent LoopPointReached { get; private set; }

        public ParticleEffectEvent Paused { get; private set; }

        public ParticleEffectEvent Resumed { get; private set; }

        public ParticleEffectEvent Started { get; private set; }

        public ParticleEffectEvent Stopped { get; private set; }

        public void Play()
        {
            InternalStop(false);

            if (!ParticleSystemRoot)
                return;

            if (!ParticleSystemRoot.isStopped) 
                return;
            
            PlaybackTime = 0;
            ParticleSystemRoot.Play();
            FireEventStarted();
            started = true;
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
        
        [Preserve]
        public virtual void OnSpawned()
        {
            SetCachedTransform();
            SetParticleSystemRoot();
            SetParticleSystems();
            SetDuration();
        }

        protected virtual void Awake()
        {
            InitializeEvents();
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

            if (ParticleSystemRoot.isPaused || !started)
                return;

            PlaybackTime += Time.deltaTime;

            if (PlaybackTime < Duration)
                return;

            if (IsLoop)
            {
                FireEventLoopPointReached();
            }
            else
            {
                FireEventStopped();
            }

            PlaybackTime = 0f;
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
                var mainModule = childParticleSystem.main;

                if (mainModule.loop)
                {
                    IsLoop = true;
                    return;
                }

                if (!childParticleSystem.emission.enabled)
                    continue;

                var maxDuration = mainModule.startDelayMultiplier + Mathf.Max(mainModule.duration, mainModule.startLifetimeMultiplier);
                duration = Mathf.Max(duration, maxDuration);
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

        private void InitializeEvents()
        {
            Started ??= new ParticleEffectEvent();
            Paused ??= new ParticleEffectEvent();
            Resumed ??= new ParticleEffectEvent();
            Stopped ??= new ParticleEffectEvent();
            LoopPointReached ??= new ParticleEffectEvent();
        }

        private void InternalStop(bool fireStoppedEvent = true)
        {
            if (!ParticleSystemRoot)
                return;

            if (ParticleSystemRoot.isPlaying)
                ParticleSystemRoot.Stop();

            if (fireStoppedEvent)
                FireEventStopped();

            started = false;
        }
    }
}