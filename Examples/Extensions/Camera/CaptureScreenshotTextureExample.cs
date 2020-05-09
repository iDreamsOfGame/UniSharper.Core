using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Examples.Extensions.Camera
{
    internal class CaptureScreenshotTextureExample : MonoBehaviour
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
            rawImage3.texture = mainCamera.CaptureScreenshotTexture(new Rect(0, 0, Screen.width / 2f, Screen.height / 2f));
            rawImage4.texture = mainCamera.CaptureScreenshotTexture(new Rect(0, Screen.height / 2f, Screen.width, Screen.height / 2f));
            rawImage4.rectTransform.sizeDelta = new Vector2(rawImage4.rectTransform.sizeDelta.x, rawImage4.rectTransform.sizeDelta.x / (rawImage4.mainTexture.width / rawImage4.mainTexture.height));
            BlendTextures();
        }

        private void BlendTextures()
        {
            var texture1 = rawImage1.texture as Texture2D;
            var texture2 = rawImage4.texture as Texture2D;

            if (!texture1 || !texture2)
                return;


            var output = new Texture2D(texture1.width, texture1.height, TextureFormat.RGBA32, false)
            {
                alphaIsTransparency = true, 
                wrapMode = TextureWrapMode.Clamp
            };

            var positionX = 100;
            var positionY = 200;
            
            for (int x = 0; x < texture1.width; x++)
            {
                for (int y = 0; y < texture1.height; y++)
                {
                    var color1 = texture1.GetPixel(x, y);
                    var color2 = texture2.GetPixel(x, y);
                    output.SetPixel(x, y, color1 + color2);
                }
            }
            
            output.Apply();
            rawImage3.texture = output;
        }
    }
}


