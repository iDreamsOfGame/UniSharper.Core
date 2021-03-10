using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Samples
{
    internal class CaptureScreenshotTextureSample : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private UnityEngine.Camera mainCamera = null;

        [SerializeField]
        private RawImage rawImage1 = null;

        [SerializeField]
        private RawImage rawImage2 = null;

        [SerializeField]
        private RawImage rawImage3 = null;

        [SerializeField]
        private RawImage rawImage4 = null;

        #endregion Fields

        #region Methods

        private void Start()
        {
            rawImage1.texture = mainCamera.CaptureScreenshotTexture();
            rawImage2.texture = mainCamera.CaptureScreenshotTexture(new Rect(Screen.width / 4f, Screen.height / 4f, Screen.width / 2f, Screen.height / 2f));
            rawImage3.texture = mainCamera.CaptureScreenshotTexture(new Rect(0, 0, Screen.width / 2f, Screen.height / 2f));
            rawImage4.texture = mainCamera.CaptureScreenshotTexture(new Rect(0, Screen.height / 2f, Screen.width, Screen.height / 2f));
            rawImage4.rectTransform.sizeDelta = new Vector2(rawImage4.rectTransform.sizeDelta.x, rawImage4.rectTransform.sizeDelta.x / (rawImage4.mainTexture.width / rawImage4.mainTexture.height));
        }

        #endregion Methods
    }
}