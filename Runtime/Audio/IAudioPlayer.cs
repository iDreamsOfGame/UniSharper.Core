// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

#if UNITY_AUDIO_MODULE
using UnityEngine;

namespace UniSharper.Audio
{
    /// <summary>
    /// Interface for playing audio source.
    /// </summary>
    public interface IAudioPlayer
    {
        /// <summary>
        /// Get the component of <see cref="UnityEngine.AudioSource"/>.
        /// </summary>
        /// <value>The component of <see cref="UnityEngine.AudioSource"/>. </value>
        AudioSource AudioSource { get; }

        /// <summary>
        /// Is the audio clip looping.
        /// </summary>
        /// <value>The indicator that the <see cref="UnityEngine.AudioSource"/> replays after it finishes or not. </value>
        bool IsLoop { get; set; }

        /// <summary>
        /// Un- / Mutes the <see cref="UnityEngine.AudioSource"/>.
        /// </summary>
        /// <value>The indicator that the <see cref="UnityEngine.AudioSource"/> is muting. </value>
        bool Mute { get; set; }

        /// <summary>
        /// Plays the audio.
        /// </summary>
        /// <param name="delay">Delay time specified in seconds. </param>
        /// <param name="muted">Whether mutes the audio. </param>
        /// <param name="isLoop">Whether replays after the audio finishes. </param>
        void Play(float delay = 0f, bool muted = false, bool isLoop = false);

        /// <summary>
        /// Plays the audio once.
        /// </summary>
        /// <param name="delay">Delay time specified in seconds. </param>
        /// <param name="muted">Whether mutes the audio. </param>
        void PlayOneShot(float delay = 0f, bool muted = false);
        
        /// <summary>
        /// Pauses playing.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes playing.
        /// </summary>
        void Resume();

        /// <summary>
        /// Stops playing audio.
        /// </summary>
        void Stop();
    }
}
#endif