using System;
using UniSharper.Patterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UniSharper.Rendering.PostProcessing
{
    /// <summary>
    /// This class implements camera shake effect.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraShake : SingletonMonoBehaviour<CameraShake>
    {
        #region Fields

        private Transform cachedTransform;

        private Action completedCallback;

        private float duration;

        private float elapsedTime;

        [SerializeField]
        private float magnitude = 1f;

        [SerializeField]
        private Vector3 originalPosition;

        [SerializeField]
        private bool resetPositionAfterShaking = true;

        [SerializeField]
        private AnimationCurve shakeAmplitude;

        #endregion Fields

        #region Properties

        public Transform CachedTransform
        {
            get
            {
                if (!cachedTransform)
                    cachedTransform = transform;

                return cachedTransform;
            }
        }

        public bool IsShaking { get; private set; }

        /// <summary>
        /// The magnitude of shaking.
        /// </summary>
        public float Magnitude
        {
            get => magnitude;
            set => magnitude = value;
        }

        /// <summary>
        /// The original position.
        /// </summary>
        public Vector3 OriginalPosition
        {
            get => originalPosition;
            set => originalPosition = value;
        }

        /// <summary>
        /// Whether reset position to original position when shaking complete.
        /// </summary>
        public bool ResetPositionAfterShaking
        {
            get => resetPositionAfterShaking;
            set => resetPositionAfterShaking = value;
        }

        /// <summary>
        /// The shake amplitude. Which is a animation curve.
        /// </summary>
        public AnimationCurve ShakeAmplitude
        {
            get => shakeAmplitude;
            set => shakeAmplitude = value;
        }

        #endregion Properties

        #region Methods

        public void Shake(float duration, Action completedCallback = null)
        {
            elapsedTime = 0f;
            this.duration = duration;
            this.completedCallback = completedCallback;
            CachedTransform.localPosition = originalPosition;
            IsShaking = true;
        }

        private bool CheckShakeComplete()
        {
            if (elapsedTime >= duration)
            {
                elapsedTime = 0f;
                IsShaking = false;

                if (resetPositionAfterShaking)
                    CachedTransform.localPosition = originalPosition;

                completedCallback?.Invoke();
                return true;
            }

            return false;
        }

        private void LateUpdate()
        {
            if (!IsShaking)
                return;

            elapsedTime += Time.deltaTime;
            if (CheckShakeComplete())
                return;

            var time = elapsedTime / duration;
            var amplitude = shakeAmplitude.Evaluate(time);
            var power = magnitude * amplitude;
            CachedTransform.localPosition = originalPosition + Random.insideUnitSphere * power;
        }

        #endregion Methods
    }
}