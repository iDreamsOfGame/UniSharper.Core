using UniSharper.Effect;
using UnityEngine;

namespace UniSharper.Samples
{
    internal class ParticleEffectControllerSample : MonoBehaviour
    {
        [SerializeField]
        private ParticleEffectController nonLoopEffectController;

        [SerializeField]
        private ParticleEffectController loopEffectController;

        private void OnEffectPlayStarted(IParticleEffectController particleEffectController)
        {
            Debug.Log($"effect ({particleEffectController.CachedTransform.name}) start!");
        }
        
        private void OnEffectPaused(IParticleEffectController particleEffectController)
        {
            Debug.Log($"effect({particleEffectController.CachedTransform.name}) paused!");
        }

        private void OnEffectResumed(IParticleEffectController particleEffectController)
        {
            Debug.Log($"effect({particleEffectController.CachedTransform.name}) resumed!");
        }
        
        private void OnEffectStopped(IParticleEffectController particleEffectController)
        {
            Debug.Log($"effect({particleEffectController.CachedTransform.name}) stopped, playback time: {particleEffectController.PlaybackTime}, duration: {particleEffectController.Duration}.");
        }

        private void OnEffectLoopPointReached(IParticleEffectController particleEffectController)
        {
            Debug.Log($"effect({particleEffectController.CachedTransform.name}) loop reached, duration: {particleEffectController.Duration}.");
        }

        private void Pause()
        {
            nonLoopEffectController.Pause();
            loopEffectController.Pause();
            Invoke(nameof(Resume), 1f);
        }

        private void Resume()
        {
            nonLoopEffectController.Resume();
            loopEffectController.Resume();
        }

        private void Start()
        {
            nonLoopEffectController.Started.AddListener(OnEffectPlayStarted);
            nonLoopEffectController.Paused.AddListener(OnEffectPaused);
            nonLoopEffectController.Resumed.AddListener(OnEffectResumed);
            nonLoopEffectController.Stopped.AddListener(OnEffectStopped);
            nonLoopEffectController.Play();
            
            loopEffectController.Started.AddListener(OnEffectPlayStarted);
            loopEffectController.Paused.AddListener(OnEffectPaused);
            loopEffectController.Resumed.AddListener(OnEffectResumed);
            loopEffectController.LoopPointReached.AddListener(OnEffectLoopPointReached);
            loopEffectController.Play();
            
            Invoke(nameof(Pause), 0.2f);
        }
    }
}