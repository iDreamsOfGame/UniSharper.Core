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
        private ParticleSystem particleSystemRoot = null;

        public ParticleEffectEvent Started { get; private set; }
        
        public ParticleEffectEvent Paused { get; private set; }
        
        public ParticleEffectEvent Resumed { get; private set; }
        
        public ParticleEffectEvent LoopPointReached { get; private set; }

        public ParticleSystem ParticleSystemRoot
        {
            get
            {
                if (particleSystemRoot) 
                    return particleSystemRoot;

                particleSystemRoot = GetComponent<ParticleSystem>();

                if (!particleSystemRoot)
                {
                    particleSystemRoot = transform.GetComponentInChildren<ParticleSystem>(true);
                }

                return particleSystemRoot;
            }
        }

        public Transform Transform => transform;

        public float Time => ParticleSystemRoot.time;

        public float Duration => ParticleSystemRoot.main.duration;

        public bool IsLoop => ParticleSystemRoot.main.loop;

        public bool RemoveAllEventListenersOnDisable { get; set; } = true;

        public void Play()
        {
            InternalStop();

            if (ParticleSystemRoot.isStopped)
            {
                ParticleSystemRoot.Play();
                FireEventStarted();
            }
            else
            {
                Debug.LogWarning("Can not play particle effect, cause the state of ParticleSystem is not stopped!");
            }
        }

        public void Pause()
        {
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

        protected void OnDisable()
        {
            if (RemoveAllEventListenersOnDisable)
                DestroyEvents();
        }

        protected void OnDestroy()
        {
            DestroyEvents();
        }

        private void Update()
        {
            if (IsLoop)
            {
                var deltaTime = ParticleSystemRoot.main.useUnscaledTime
                    ? UnityEngine.Time.unscaledDeltaTime
                    : UnityEngine.Time.deltaTime;

                if (Duration - Time <= deltaTime)
                    FireEventLoopPointReached();
            }
            else
            {
                if (Time >= Duration)
                    FireEventLoopPointReached();
            }
        }

        private void InitializeEvents()
        {
            if (Started == null)
                Started = new ParticleEffectEvent();
            
            if(Paused == null)
                Paused = new ParticleEffectEvent();
            
            if(Resumed == null)
                Resumed = new ParticleEffectEvent();
            
            if(LoopPointReached == null)
                LoopPointReached = new ParticleEffectEvent();
        }

        private void DestroyEvents()
        {
            Started?.RemoveAllListeners();
            Paused?.RemoveAllListeners();
            LoopPointReached?.RemoveAllListeners();
        }

        private void InternalStop()
        {
            if (ParticleSystemRoot.isPlaying)
                ParticleSystemRoot.Stop();
        }

        private void FireEventStarted()
        {
            Started?.Invoke(this);
        }

        private void FireEventPaused()
        {
            Paused?.Invoke(this);
        }

        private void FireEventResumed()
        {
            Resumed?.Invoke(this);
        }

        private void FireEventLoopPointReached()
        {
            LoopPointReached?.Invoke(this);
        }
    }
}
