using UnityEngine;
using UnityEngine.UI;

namespace ValeryPopov.Common.Localization
{
    public class SpriteLocalizationController : LocalizationController
    {
        private Image targetImage;
        private SpriteRenderer spriteRenderer;

        protected override void Awake()
        {
            targetImage = GetComponent<Image>();

            spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer == null && targetImage == null)
            {
                Debug.LogError("I couldn't find any SpriteRenderer or Image components, i think i'm going to die...");
            }

            base.Awake();
        }

        /// <summary>
        /// Extract text data from data Object and pass it into tmpText or ui text
        /// </summary>
        protected override void SetData()
        {
            if (targetImage != null)
            {
                targetImage.sprite = GetData();
            }
            else if (spriteRenderer != null)
            {
                spriteRenderer.sprite = GetData();
            }
        }

        public Sprite GetData()
        {
            return (_localizationManager.GetSpriteData(ID)).Sprite;
        }
    }
}