using UniSharper.Effect;
using UnityEngine;

namespace UniSharper.Examples.Effect
{
    public class ParticleEffectControllerExample : MonoBehaviour
    {
        [SerializeField]
        private ParticleEffectController effectController = null;

        private void Start()
        {
            effectController.Started.AddListener(OnEffectPlayStarted);
            effectController.Paused.AddListener(OnEffectPlayPaused);
            effectController.LoopPointReached.AddListener(OnEffectPlayCompleted);
            effectController.Play();
            Invoke("Pause", 0.5f);
        }

        private void Pause()
        {
            effectController.Pause();
            Invoke("Resume", 1f);
        }

        private void Resume()
        {
            Debug.Log("effect resumed!");
            effectController.Resume();
        }

        private void OnEffectPlayStarted()
        {
            Debug.Log("effect start!");
        }

        private void OnEffectPlayPaused()
        {
            Debug.Log("effect paused!");
        }

        private void OnEffectPlayCompleted()
        {
            Debug.Log("effect stopped!");
            
            if(!effectController.IsLoop)
                Destroy(effectController.gameObject);
        }
    }
}