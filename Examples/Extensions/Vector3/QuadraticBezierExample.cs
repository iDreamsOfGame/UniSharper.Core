using UnityEngine;

namespace UniSharper.Examples.Extensions.Vector3
{
    /// <summary>
    /// Quadratic Bezier examples
    /// </summary>
    internal class QuadraticBezierExample : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer = null;

        private void Update()
        {
            DrawQuadraticBezierCurve();
        }

        private void DrawQuadraticBezierCurve()
        {
            var points = Vector3Utility.GenerateQuadraticBezierPoints(UnityEngine.Vector3.zero, new UnityEngine.Vector3(0.5f, 0.5f), 
                new UnityEngine.Vector3(1, 0, 0), 50);

            for (var i = 1; i < points.Length; i++)
            {
                var point = points[i];
                lineRenderer.positionCount = i;
                lineRenderer.SetPosition(i - 1, point);
            }
        }
    }
}