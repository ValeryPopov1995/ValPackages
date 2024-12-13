namespace ValeryPopov.Common.Localization
{
    using UnityEngine;

    [System.Serializable]
    public class LocalizationTextData : LocalizationAssetData
    {
        //public TMP_FontAsset Font;
        [TextArea(3, 6)] public string Text;
    }
}