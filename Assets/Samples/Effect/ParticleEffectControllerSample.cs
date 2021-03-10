using UniSharper.Effect;
using UnityEngine;

namespace UniSharper.Samples
{
    internal class ParticleEffectControllerSample : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private ParticleEffectController effectController = null;

        #endregion Fields

        #region Methods

        private void OnEffectPaused(IParticleEffectController particleEffectController)
        {
            Debug.Log("effect paused!");
        }

        private void OnEffectPlayCompleted(IParticleEffectController particleEffectController)
        {
            Debug.Log("effect stopped!");

            if (!effectController.IsLoop)
                Destroy(effectController.gameObject);
        }

        private void OnEffectPlayStarted(IParticleEffectController particleEffectController)
        {
            Debug.Log("effect start!");
        }

        private void OnEffectResumed(IParticleEffectController particleEffectController)
        {
            Debug.Log("effect resumed!");
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
            effectController.LoopPointReached.AddListener(OnEffectPlayCompleted);
            effectController.Play();
            Invoke(nameof(Pause), 0.5f);
        }

        #endregion Methods
    }
}