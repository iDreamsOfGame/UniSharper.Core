using UniSharper.Effect;
using UnityEngine;

namespace UniSharper.Tests.Effect
{
    internal class ParticleEffectControllerTest : MonoBehaviour
    {
        [SerializeField]
        private ParticleEffectController effectController = null;

        private void Start()
        {
            effectController.Started.AddListener(OnEffectPlayStarted);
            effectController.Paused.AddListener(OnEffectPaused);
            effectController.Resumed.AddListener(OnEffectResumed);
            effectController.LoopPointReached.AddListener(OnEffectPlayCompleted);
            effectController.Play();
            Invoke(nameof(Pause), 0.5f);
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

        private void OnEffectPlayCompleted(IParticleEffectController particleEffectController)
        {
            Debug.Log("effect stopped!");
            
            if(!effectController.IsLoop)
                Destroy(effectController.gameObject);
        }
    }
}