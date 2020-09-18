// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections;
using UnityEngine;

namespace UniSharper.Audio
{
    /// <summary>
    /// Implementation for playing audio source.
    /// </summary>
    [DisallowMultipleComponent]
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
                if (AudioSource) 
                    AudioSource.loop = value;
            }
        }

        public AudioSource AudioSource
        {
            get
            {
                if (!audioSource) 
                    audioSource = GetComponent<AudioSource>();
                
                return audioSource;
            }
        }

        #endregion Properties

        #region Methods

        public void Pause()
        {
            AudioSource.Pause();
        }

        public void Play(float delay = 0f, bool muted = false, bool isLoop = false)
        {
            IsLoop = isLoop;
            AudioSource.mute = muted;
            
            if(!AudioSource || !AudioSource.clip)
                return;
            
            AudioSource.PlayDelayed(delay);
        }

        public void PlayOneShot(float delay = 0f, bool muted = false)
        {
            IsLoop = false;
            AudioSource.mute = muted;
            
            if(!AudioSource || !AudioSource.clip)
                return;
            
            StartCoroutine(PlayOneShotDelayed(delay));
        }

        public void Resume()
        {
            AudioSource.UnPause();
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        private IEnumerator PlayOneShotDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            AudioSource.PlayOneShot(AudioSource.clip);
        }

        #endregion Methods
    }
}