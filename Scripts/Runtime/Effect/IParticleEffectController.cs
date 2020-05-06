// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.Events;

namespace UniSharper.Effect
{
    /// <summary>
    /// Interface for controlling particle effect.
    /// </summary>
    public interface IParticleEffectController
    {
        /// <summary>
        /// Invoked immediately after Play is called.
        /// </summary>
        UnityEvent Started { get; }

        /// <summary>
        /// Invoked immediately after Pause is called.
        /// </summary>
        UnityEvent Paused { get; }
        
        /// <summary>
        /// Invoked when the time of <see cref="UnityEngine.ParticleSystem"/> reaches the duration.
        /// </summary>
        UnityEvent LoopPointReached { get; }

        /// <summary>
        /// Get the root component of <see cref="UnityEngine.ParticleSystem"/>.
        /// </summary>
        /// <value>The root component of <see cref="UnityEngine.ParticleSystem"/>. </value>
        ParticleSystem ParticleSystemRoot { get; }

        /// <summary>
        /// Get the playback position of the <see cref="UnityEngine.ParticleSystem"/> in seconds.
        /// </summary>
        /// <value>The playback position of the <see cref="UnityEngine.ParticleSystem"/> in seconds. </value>
        float Time { get; }

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
