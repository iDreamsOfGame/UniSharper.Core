﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

#if UNITY_PARTICLE_SYSTEM_MODULE
using UnityEngine;

namespace UniSharper.Effect
{
    /// <summary>
    /// Interface for controlling particle effect.
    /// </summary>
    public interface IParticleEffectController
    {
        /// <summary>
        /// Gets the component <see cref="UnityEngine.Transform"/> attached to this <see cref="IParticleEffectController"/>.
        /// </summary>
        Transform CachedTransform { get; }

        /// <summary>
        /// Gets the <see cref="UnityEngine.ParticleSystem"/> attached to this <see cref="IParticleEffectController"/> or the top level in the hierarchy.
        /// </summary>
        ParticleSystem ParticleSystemRoot { get; }

        /// <summary>
        /// Gets all the component of <see cref="UnityEngine.ParticleSystem"/> attached to this <see cref="IParticleEffectController"/> or children in the hierarchy.
        /// </summary>
        ParticleSystem[] ParticleSystems { get; }

        /// <summary>
        /// Whether check all <see cref="ParticleSystem"/>s stopped in event method <c>Update</c>.
        /// </summary>
        bool CheckParticleSystemsStopped { get; set; }

        /// <summary>
        /// Get the duration of the <see cref="UnityEngine.ParticleSystem"/> in seconds.
        /// </summary>
        float Duration { get; }

        /// <summary>
        /// Whether this controller has initialized or not.
        /// </summary>
        bool HasInitialized { get; }

        /// <summary>
        /// Is the particle effect looping.
        /// </summary>
        bool IsLoop { get; }
        
        /// <summary>
        /// Get the playback position of the <see cref="UnityEngine.ParticleSystem"/> in seconds.
        /// </summary>
        float PlaybackTime { get; }
        
        /// <summary>
        /// Invoked immediately after Play is called.
        /// </summary>
        ParticleEffectEvent Started { get; }

        /// <summary>
        /// Invoked immediately after Pause is called.
        /// </summary>
        ParticleEffectEvent Paused { get; }

        /// <summary>
        /// Invoked immediately after Resume is called.
        /// </summary>
        ParticleEffectEvent Resumed { get; }

        /// <summary>
        /// Invoked when the time of <see cref="UnityEngine.ParticleSystem"/> stopped.
        /// </summary>
        ParticleEffectEvent Stopped { get; }
        
        /// <summary>
        /// Invoked when the time of <see cref="UnityEngine.ParticleSystem"/> reaches the playback time.
        /// </summary>
        ParticleEffectEvent LoopPointReached { get; }

        /// <summary>
        /// Starts the particle effect.
        /// </summary>
        void Play();
        
        /// <summary>
        /// Pauses the particle effect.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes playing the particle effect.
        /// </summary>
        void Resume();

        /// <summary>
        /// Stops playing the particle effect.
        /// </summary>
        void Stop();

        /// <summary>
        /// Removes all listeners on events of this controller.
        /// </summary>
        void RemoveAllListeners();
    }
}
#endif