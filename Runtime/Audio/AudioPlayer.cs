// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
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
        private AudioSource audioSource;

        public AudioSource AudioSource
        {
            get
            {
                try
                {
                    if (!audioSource && gameObject)
                        TryGetComponent(out audioSource);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e.ToString());
                }
                
                return audioSource;
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

        public bool Mute
        {
            get => AudioSource && AudioSource.mute;
            set
            {
                if (!AudioSource)
                    return;

                if (AudioSource.mute != value)
                    AudioSource.mute = value;
            }
        }

        public void Play(float delay = 0f, bool muted = false, bool isLoop = false)
        {
            if (!AudioSource)
                return;

            IsLoop = isLoop;
            AudioSource.mute = muted;

            if (!AudioSource.clip)
                return;

            AudioSource.PlayDelayed(delay);
        }

        public void PlayOneShot(float delay = 0f, bool muted = false)
        {
            if (!AudioSource)
                return;

            IsLoop = false;
            AudioSource.mute = muted;

            if (!AudioSource.clip)
                return;

            StartCoroutine(PlayOneShotDelayed(delay));
        }

        public void Pause()
        {
            if (!AudioSource)
                return;

            AudioSource.Pause();
        }

        public void Resume()
        {
            if (!AudioSource)
                return;

            AudioSource.UnPause();
        }

        public void Stop()
        {
            if (!AudioSource)
                return;

            AudioSource.Stop();
        }

        private IEnumerator PlayOneShotDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (AudioSource && AudioSource.clip)
                AudioSource.PlayOneShot(AudioSource.clip);
        }
    }
}