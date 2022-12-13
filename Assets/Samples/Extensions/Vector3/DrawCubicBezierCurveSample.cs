using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Samples
{
    /// <summary>
    /// Cubic Bezier examples
    /// </summary>
    internal class DrawCubicBezierCurveSample : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        private void Start()
        {
            var points = Vector3Utility.GenerateCubicBezierPoints(Vector3.zero, new Vector3(0f, 0.5f),
                new Vector3(0.75f, 0.5f, 0), new Vector3(1, 0, 0), 50);

            for (var i = 1; i < points.Length; i++)
            {
                var point = points[i];
                lineRenderer.positionCount = i;
                lineRenderer.SetPosition(i - 1, point);
            }
        }
    }
}