using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ValeryPopov.Common.Localization
{
    public class LocalizationManager : MonoBehaviour, IInitializable
    {
        private const string _saveKeyLenguageIndex = "LanguageIndex SaveKey";

        public event Action<Language> OnLanguageChanged;
        public TMP_FontAsset DefaultFont { get; private set; }
        public Language CurrentLanguage { get; private set; }

        public bool Initialized { get; private set; }

        [SerializeField] private LanguagesData _languageData;
        [SerializeField] private List<LocalizationAsset> _localizationAssets;

        private LocalizationAsset _currentLocalisationAsset = null;



        public async Task Initialize()
        {
            await Task.Yield();
            CurrentLanguage = LoadLanguage();
            SetLanguage(CurrentLanguage);
            Initialized = true;
        }

        private Language LoadLanguage()
        {
            int target = PlayerPrefs.GetInt(_saveKeyLenguageIndex);
            return _localizationAssets.First(asset => (int)asset.Language == target).Language;
        }

        public void SetLanguage(Language targetLanguage)
        {
            var asset = _localizationAssets.FirstOrDefault(language => language.Language == targetLanguage);
            if (asset == null) return;

            CurrentLanguage = targetLanguage;
            PlayerPrefs.SetInt(_saveKeyLenguageIndex, (int)targetLanguage);
            _currentLocalisationAsset = asset;
            DefaultFont = _languageData.LanguageDatas.First(data => data.Language == CurrentLanguage).DefaultFont;

            OnLanguageChanged?.Invoke(CurrentLanguage);
        }

        public LocalizationTextData GetTextData(string id) => _currentLocalisationAsset.GetTextData(id);
        public LocalizationAudioData GetAudioData(string id) => _currentLocalisationAsset.GetAudioData(id);
        public LocalizationSpriteData GetSpriteData(string id) => _currentLocalisationAsset.GetSpriteData(id);
        public LocalizationMaterialData GetMaterialData(string id) => _currentLocalisationAsset.GetMaterialData(id);

        #region SetLanguage

        [Button]
        public void SetRu()
        {
            if (!Application.isPlaying)
                return;

            SetLanguage(Language.ru);
        }

        [Button]
        public void SetEn()
        {
            if (!Application.isPlaying)
                return;

            SetLanguage(Language.en);
        }

        #endregion
    }
}