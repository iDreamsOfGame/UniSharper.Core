using UniSharper.Effect;
using UnityEngine;

namespace UniSharper.Samples
{
    internal class ParticleEffectControllerSample : MonoBehaviour
    {
        [SerializeField]
        private ParticleEffectController effectController;

        private void OnEffectPlayStarted(IParticleEffectController particleEffectController)
        {
            Debug.Log("effect start!");
        }
        
        private void OnEffectPaused(IParticleEffectController particleEffectController)
        {
            Debug.Log("effect paused!");
        }

        private void OnEffectResumed(IParticleEffectController particleEffectController)
        {
            Debug.Log("effect resumed!");
        }
        
        private void OnEffectStopped(IParticleEffectController particleEffectController)
        {
            Debug.Log($"effect stopped, duration: {particleEffectController.Duration}.");
        }

        private void OnEffectLoopPointReached(IParticleEffectController particleEffectController)
        {
            Debug.Log($"effect loop reached, duration: {particleEffectController.Duration}.");
        }

        private void Pause()
        {
            effectController.Pause();
            Invoke(nameof(Resume), 1f);
        }

        private void Resume()
        {
            effectController.Resume();
        }

        private void Start()
        {
            effectController.Started.AddListener(OnEffectPlayStarted);
            effectController.Paused.AddListener(OnEffectPaused);
            effectController.Resumed.AddListener(OnEffectResumed);
            effectController.LoopPointReached.AddListener(OnEffectLoopPointReached);
            effectController.Stopped.AddListener(OnEffectStopped);
            effectController.Play();
            
            if (!effectController.isActiveAndEnabled)
                Invoke(nameof(Pause), 0.5f);
        }
    }
}