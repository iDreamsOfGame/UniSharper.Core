using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Examples.Extensions.Texture2D
{
    internal class BlendTextureOverlayExample : MonoBehaviour
    {
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
        
        private void Start()
        {
            rawImage1.texture = mainCamera.CaptureScreenshotTexture();
            rawImage2.texture = mainCamera.CaptureScreenshotTexture(new Rect(Screen.width / 4f, Screen.height / 4f, Screen.width / 2f, Screen.height / 2f));
            rawImage4.texture = mainCamera.CaptureScreenshotTexture(new Rect(0, Screen.height / 2f, Screen.width, Screen.height / 2f));
            rawImage4.rectTransform.sizeDelta = new Vector2(rawImage4.rectTransform.sizeDelta.x, rawImage4.rectTransform.sizeDelta.x / (rawImage4.mainTexture.width / rawImage4.mainTexture.height));
            BlendTexture();
        }

        private void BlendTexture()
        {
            var texture1 = rawImage1.texture as UnityEngine.Texture2D;
            var texture2 = rawImage4.texture as UnityEngine.Texture2D;

            if (!texture1 || !texture2)
                return;

            rawImage3.texture = texture1.BlendTextureOverlay(texture2, new Vector2Int(100, 150));
        }
    }
}