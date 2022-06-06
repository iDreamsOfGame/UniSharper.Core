using UnityEngine;
using UnityEngine.UI;
// ReSharper disable All

namespace UniSharper.Samples
{
    internal class CaptureScreenshotTextureSample : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private RawImage rawImage1;

        [SerializeField]
        private RawImage rawImage2;

        [SerializeField]
        private RawImage rawImage3;

        [SerializeField]
        private RawImage rawImage4;

        private void Start()
        {
            rawImage1.texture = mainCamera.CaptureScreenshotTexture();
            rawImage2.texture = mainCamera.CaptureScreenshotTexture(new Rect(Screen.width / 4f, Screen.height / 4f, Screen.width / 2f, Screen.height / 2f));
            rawImage3.texture = mainCamera.CaptureScreenshotTexture(new Rect(0, 0, Screen.width / 2f, Screen.height / 2f));
            rawImage4.texture = mainCamera.CaptureScreenshotTexture(new Rect(0, Screen.height / 2f, Screen.width, Screen.height / 2f));

            var sizeDelta = rawImage4.rectTransform.sizeDelta;
            rawImage4.rectTransform.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.x / (rawImage4.mainTexture.width / rawImage4.mainTexture.height));
        }
    }
}