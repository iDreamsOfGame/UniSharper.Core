// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.Events;

namespace UniSharper.Effect
{
    /// <summary>
    /// Implementation for controlling particle effect.
    /// </summary>
    public class ParticleEffectController : MonoBehaviour, IParticleEffectController
    {
        private ParticleSystem particleSystemRoot = null;

        public UnityEvent Started { get; private set; }
        
        public UnityEvent Paused { get; private set; }
        
        public UnityEvent LoopPointReached { get; private set; }

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

        public float Time => particleSystemRoot.time;

        public float Duration => particleSystemRoot.main.duration;

        public bool IsLoop => particleSystemRoot.main.loop;

        public void Play()
        {
            InternalStop();

            if (ParticleSystemRoot.isStopped)
            {
                ParticleSystemRoot.Play();
                Started?.Invoke();
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
                Paused?.Invoke();
            }
            else
            {
                Debug.LogWarning("Can not pause particle effect, cause the state of ParticleSystem is not playing!");
            }
        }

        public void Resume()
        {
            if (ParticleSystemRoot.isPaused)
                ParticleSystemRoot.Play();
        }

        public void Stop()
        {
            InternalStop();
        }
        
        protected virtual void Awake()
        {
            InitializeEvents();
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
                
                if(Duration - Time <= deltaTime)
                    LoopPointReached?.Invoke();
            }
            else
            {
                if (Time >= Duration)
                    LoopPointReached?.Invoke(); 
            }
        }

        private void InitializeEvents()
        {
            if (Started == null)
                Started = new UnityEvent();
            
            if(Paused == null)
                Paused = new UnityEvent();
            
            if(LoopPointReached == null)
                LoopPointReached = new UnityEvent();
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
    }
}
