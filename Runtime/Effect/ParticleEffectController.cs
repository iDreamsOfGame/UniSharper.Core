// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Effect
{
    /// <summary>
    /// Implementation for controlling particle effect.
    /// </summary>
    [DisallowMultipleComponent]
    public class ParticleEffectController : MonoBehaviour, IParticleEffectController
    {
        private float duration = -1f;

        private ParticleSystem particleSystemRoot;

        private bool started;

        public Transform CachedTransform => transform;

        public float Duration
        {
            get
            {
                if (duration < 0 && ParticleSystemRoot != null)
                {
                    var childParticleSystems = ParticleSystemRoot.GetComponentsInChildren<ParticleSystem>();
                    foreach (var childParticleSystem in childParticleSystems)
                    {
                        var main = childParticleSystem.main;

                        if (main.loop)
                            return -1;

                        if (!childParticleSystem.emission.enabled)
                            continue;

                        var maxDuration = main.startDelayMultiplier + Mathf.Max(main.duration, main.startLifetimeMultiplier);
                        duration = Mathf.Max(duration, maxDuration);
                    }
                }

                return duration;
            }
        }

        public bool IsLoop => ParticleSystemRoot && ParticleSystemRoot.main.loop;

        public ParticleEffectEvent LoopPointReached { get; private set; }

        public ParticleSystem ParticleSystemRoot
        {
            get
            {
                if (particleSystemRoot)
                    return particleSystemRoot;
                
                TryGetComponent(out particleSystemRoot);

                if (!particleSystemRoot)
                    particleSystemRoot = transform.GetComponentInChildren<ParticleSystem>(true);

                return particleSystemRoot;
            }
        }

        public ParticleEffectEvent Paused { get; private set; }

        public float PlaybackTime { get; private set; }

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