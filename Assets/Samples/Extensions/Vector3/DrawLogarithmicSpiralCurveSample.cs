using UnityEngine;

namespace UniSharper.Samples
{
    internal class DrawLogarithmicSpiralCurveSample : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private LineRenderer lineRenderer = null;

        #endregion Fields

        #region Methods

        private void Start()
        {
            var points = Vector3Utility.GenerateLogarithmicSpiralPoints(Vector3.zero, 5f, 0.5f, 0.15f, 150);

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