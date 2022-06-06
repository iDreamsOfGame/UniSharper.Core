using UnityEngine;

namespace UniSharper.Samples
{
    internal class DrawAchimedeanSpiralCurveSample : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        private void Start()
        {
            var points = Vector3Utility.GenerateAchimedeanSpiralPoints(UnityEngine.Vector3.zero, 5f, 1, 0.5f, 150);

            for (var i = 1; i < points.Length; i++)
            {
                var point = points[i];
                lineRenderer.positionCount = i;
                lineRenderer.SetPosition(i - 1, point);
            }
        }
    }
}