// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

// ReSharper disable LoopCanBeConvertedToQuery

namespace UniSharper.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.ParticleSystem"/>.
    /// </summary>
    public static class ParticleSystemExtensions
    {
        /// <summary>
        /// Gets the real duration of the <see cref="UnityEngine.ParticleSystem"/> in seconds.
        /// </summary>
        /// <param name="particleSystem">The target <see cref="UnityEngine.ParticleSystem"/>. </param>
        /// <param name="allowLoop">Whether return loop duration. </param>
        /// <returns></returns>
        public static float GetDuration(this ParticleSystem particleSystem, bool allowLoop = false)
        {
            if (!particleSystem.emission.enabled)
                return 0f;
            
            if (particleSystem.main.loop && !allowLoop)
                return -1f;

            if (!particleSystem.main.loop && particleSystem.emission.rateOverTime.GetMinValue() <= 0)
                return particleSystem.main.startDelay.GetMaxValue() + particleSystem.main.startLifetime.GetMaxValue();
            
            return particleSystem.main.startDelay.GetMaxValue() + Mathf.Max(particleSystem.main.duration, particleSystem.main.startLifetime.GetMaxValue());
        }

        public static float GetMaxValue(this ParticleSystem.MinMaxCurve minMaxCurve)
        {
            switch (minMaxCurve.mode)
            {
                case ParticleSystemCurveMode.Constant:
                    return minMaxCurve.constant;

                case ParticleSystemCurveMode.Curve:
                    return minMaxCurve.curve.GetMaxValue();

                case ParticleSystemCurveMode.TwoConstants:
                    return minMaxCurve.constantMax;

                case ParticleSystemCurveMode.TwoCurves:
                    var minValue = minMaxCurve.curveMin.GetMaxValue();
                    var maxValue = minMaxCurve.curveMax.GetMaxValue();
                    return Mathf.Max(minValue, maxValue);
            }

            return -1f;
        }

        public static float GetMinValue(this ParticleSystem.MinMaxCurve minMaxCurve)
        {
            switch (minMaxCurve.mode)
            {
                case ParticleSystemCurveMode.Constant:
                    return minMaxCurve.constant;

                case ParticleSystemCurveMode.Curve:
                    return minMaxCurve.curve.GetMinValue();

                case ParticleSystemCurveMode.TwoConstants:
                    return minMaxCurve.constantMin;

                case ParticleSystemCurveMode.TwoCurves:
                    var ret1 = minMaxCurve.curveMin.GetMinValue();
                    var ret2 = minMaxCurve.curveMax.GetMinValue();
                    return ret1 < ret2 ? ret1 : ret2;
            }

            return -1f;
        }

        public static float GetMinValue(this AnimationCurve curve)
        {
            var minValue = float.MaxValue;
            var frames = curve.keys;
            foreach (var frame in frames)
            {
                var value = frame.value;
                minValue = Mathf.Min(value, minValue);
            }

            return minValue;
        }

        public static float GetMaxValue(this AnimationCurve curve)
        {
            var maxValue = float.MinValue;
            var frames = curve.keys;
            foreach (var frame in frames)
            {
                var value = frame.value;
                maxValue = Mathf.Max(value, maxValue);
            }

            return maxValue;
        }
    }
}