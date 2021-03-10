using UnityEngine;

namespace UniSharper.Samples
{
    /// <summary>
    /// Quadratic Bezier examples
    /// </summary>
    internal class DrawQuadraticBezierCurveSample : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private LineRenderer lineRenderer = null;

        #endregion Fields

        #region Methods

        private void Start()
        {
            var points = Vector3Utility.GenerateQuadraticBezierPoints(UnityEngine.Vector3.zero, new Vector3(0.5f, 0.5f),
                new Vector3(1, 0, 0), 50);

            for (var i = 1; i < points.Length; i++)
            {
                var point = points[i];
                lineRenderer.positionCount = i;
                lineRenderer.SetPosition(i - 1, point);
            }
        }

        #endregion Methods
    }
}