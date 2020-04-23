// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Audio
{
    /// <summary>
    /// Implementation for playing audio source.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        #region Fields

        private AudioSource audioSource;

        #endregion Fields

        #region Properties

        public bool Mute
        {
            get => audioSource && audioSource.mute;
            set
            {
                if (!audioSource)
                    return;

                if (audioSource.mute != value)
                    audioSource.mute = value;
            }
        }

        public bool IsLoop
        {
            get => AudioSource && AudioSource.loop;
            set
            {
                if (AudioSource) AudioSource.loop = value;
            }
        }

        public Transform Transform => transform;

        public AudioSource AudioSource
        {
            get
            {
                if (!audioSource) audioSource = GetComponent<AudioSource>();
                return audioSource;
            }
        }

        #endregion Properties

        #region Methods

        public void Pause()
        {
            AudioSource.Pause();
        }

        public void Play(bool muted = false, bool isLoop = false)
        {
            IsLoop = isLoop;
            AudioSource.mute = muted;
            AudioSource.Play();
        }

        public void Resume()
        {
            AudioSource.UnPause();
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        #endregion Methods
    }
}