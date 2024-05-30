// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using UnityEngine.Events;

namespace UniSharper.Effect
{
    /// <summary>
    /// Implementation for dispatching event of particle effect.
    /// </summary>
    [Serializable]
    public class ParticleEffectEvent : UnityEvent<IParticleEffectController>
    {
    }
}