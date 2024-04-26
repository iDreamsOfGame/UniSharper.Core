using UniSharper.Extensions;
using UnityEngine;

namespace UniSharper.Samples
{
    public class SpritesFlashShaderSample : MonoBehaviour
    {
        private const string FlashColorPropertyName = "_FlashColor";
        
        private const string FlashAmountPropertyName = "_FlashAmount";
        
        private static readonly int FlashColorProperty = Shader.PropertyToID(FlashColorPropertyName);

        private static readonly int FlashAmountProperty = Shader.PropertyToID(FlashAmountPropertyName);

        [SerializeField, ReadOnlyField]
        private Color flashColor;

        [SerializeField, ReadOnlyField]
        private float flashAmount;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer1;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer2;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer3;

        private void Start()
        {
            var spriteRenderers = new[]
            {
                spriteRenderer1,
                spriteRenderer2,
                spriteRenderer3
            };

            foreach (var spriteRenderer in spriteRenderers)
            {
                var r = RandomUtility.RangeWithRandomSeed(0f, 1);
                var g = RandomUtility.RangeWithRandomSeed(0f, 1);
                var b = RandomUtility.RangeWithRandomSeed(0f, 1);
                flashColor = new Color(r, g, b, 1);
                flashAmount = RandomUtility.RangeWithRandomSeed(0f, 1);
                SetFlashProperties(spriteRenderer, flashColor, flashAmount);
            }
        }

        private static void SetFlashProperties(SpriteRenderer spriteRenderer, Color flashColor, float flashAmount)
        {
            var block = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(block);
            block.SetColor(FlashColorProperty, flashColor);
            block.SetFloat(FlashAmountProperty, flashAmount);
            spriteRenderer.SetPropertyBlock(block);
        }
    }
}
