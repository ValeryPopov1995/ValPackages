namespace ValeryPopov.Common.Localization
{
    using UnityEngine;

    public class MaterialLocalizationController : LocalizationController
    {
        private Renderer tmpRenderer;

        protected override void Awake()
        {
            tmpRenderer = GetComponent("Renderer") as Renderer;

            if (tmpRenderer == null)
            {
                Debug.LogError("I couldn't find any text components, i think i'm going to die...");
            }

            base.Awake();
        }

        /// <summary>
        /// Extract text data from data Object and pass it into tmpText or ui text
        /// </summary>
        protected override void SetData()
        {
            tmpRenderer.material = (_localizationManager.GetMaterialData(ID)).Material;
        }
    }
}