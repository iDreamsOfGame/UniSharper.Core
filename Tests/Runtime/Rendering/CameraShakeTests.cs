using UniSharper.Rendering.PostProcessing;
using UnityEngine;

namespace UniSharper.Core.Tests.Runtime.Rendering
{
    internal class CameraShakeTests : MonoBehaviour
    {
        private void Start()
        {
            CameraShake.Instance.Shake(5f, () =>
            {
                Debug.Log("Random shaking completed!");
            });
        }
    }
}