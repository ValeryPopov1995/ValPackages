using System;
using TMPro;
using UnityEngine;

namespace ValPackage.Common.Localization
{
    [CreateAssetMenu(menuName = "Scriptable/ValPackage/Languages Data")]
    public class LanguagesData : ScriptableObject
    {
        public LanguageData[] LanguageDatas;

        [Serializable]
        public struct LanguageData
        {
            public string EngLanguageName;
            public Language Language;
            public string NotLocalizedLanguageName;
            public TMP_FontAsset DefaultFont;
        }
    }
}