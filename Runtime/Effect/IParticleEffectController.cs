﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Effect
{
    /// <summary>
    /// Interface for controlling particle effect.
    /// </summary>
    public interface IParticleEffectController
    {
        /// <summary>
        /// Get the the <see cref="UnityEngine.Transform"/> attached to this <see cref="IParticleEffectController"/>.
        /// </summary>
        /// <value>The the <see cref="UnityEngine.Transform"/> attached to this <see cref="IParticleEffectController"/>. </value>
        Transform CachedTransform { get; }

        /// <summary>
        /// Get the duration of the <see cref="UnityEngine.ParticleSystem"/> in seconds.
        /// </summary>
        /// <value>The duration of the <see cref="UnityEngine.ParticleSystem"/> in seconds. </value>
        float Duration { get; }

        /// <summary>
        /// Is the particle effect looping.
        /// </summary>
        /// <value>The indicator that the <see cref="UnityEngine.ParticleSystem"/> replays after it finishes or not. </value>
        bool IsLoop { get; }

        /// <summary>
        /// Invoked when the time of <see cref="UnityEngine.ParticleSystem"/> reaches the playback time.
        /// </summary>
        ParticleEffectEvent LoopPointReached { get; }

        /// <summary>
        /// Get the root component of <see cref="UnityEngine.ParticleSystem"/>.
        /// </summary>
        /// <value>The root component of <see cref="UnityEngine.ParticleSystem"/>. </value>
        ParticleSystem ParticleSystemRoot { get; }

        /// <summary>
        /// Invoked immediately after Pause is called.
        /// </summary>
        ParticleEffectEvent Paused { get; }

        /// <summary>
        /// Get the playback position of the <see cref="UnityEngine.ParticleSystem"/> in seconds.
        /// </summary>
        /// <value>The playback position of the <see cref="UnityEngine.ParticleSystem"/> in seconds. </value>
        float PlaybackTime { get; }

        /// <summary>
        /// Invoked immediately after Resume is called.
        /// </summary>
        ParticleEffectEvent Resumed { get; }

        /// <summary>
        /// Invoked immediately after Play is called.
        /// </summary>
        ParticleEffectEvent Started { get; }

        /// <summary>
        /// Invoked when the time of <see cref="UnityEngine.ParticleSystem"/> stopped.
        /// </summary>
        ParticleEffectEvent Stopped { get; }

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
    }
}