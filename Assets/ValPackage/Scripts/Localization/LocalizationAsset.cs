using NaughtyAttributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ValeryPopov.Common.Localization
{
    /// <summary>
    /// Набор данных (текст, аудио...) для одного конекретного языка, используемого в игре
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable/ValeryCommon/Localization Asset", order = 1)]
    public class LocalizationAsset : ScriptableObject
    {
        [field: SerializeField] public Language Language { get; private set; }

        [SerializeField] private List<LocalizationTextData> _textDataList;
        [SerializeField] private List<LocalizationAudioData> _audioDataList;
        [SerializeField] private List<LocalizationSpriteData> _spriteDataList;
        [SerializeField] private List<LocalizationMaterialData> _materialsDataList;



        public LocalizationTextData GetTextData(string id) => GetLocalizationData(_textDataList, id);
#if FMOD
        public LocalizationVoiceHelperData GetVoiceHelperData(string id) => GetLocalizationData(_voiceHelperDataList, id);
#endif
        public LocalizationAudioData GetAudioData(string id) => GetLocalizationData(_audioDataList, id);
        public LocalizationSpriteData GetSpriteData(string id) => GetLocalizationData(_spriteDataList, id);
        public LocalizationMaterialData GetMaterialData(string id) => GetLocalizationData(_materialsDataList, id);

        private TLocData GetLocalizationData<TLocData>(IEnumerable<TLocData> dataList, string id) where TLocData : LocalizationAssetData =>
            dataList.FirstOrDefault(data => data.Id == id);



#if UNITY_EDITOR
        private int _columnTargetIndex => (int)Language + 1;
        [Header("Editor Parser")]

        [SerializeField, TextArea(4, 4)]
        private string _importInstruction =
            "1. download .zip (html pages) from google tables\n" +
            "2. unzip and paste files in project\n" +
            "3. insert files in 'GoogleHtmlTexts'\n" +
            "4. click improt";

        [SerializeField] private string _eventReferencePath = "event:/Voice Helper/Rus";
        [SerializeField] private TextAsset[] _googleHtmlTexts;
        [SerializeField] private TextAsset[] _googleHtmlVoiceTexts;
        [SerializeField, TextArea(1, 5)] private string _improtLog;

        [Button("Import Text from Google Htmls async (wait fue seconds)")]
        private async void ImportTextFromGoogleHtmlsAsync()
        {
            _textDataList.Clear();
            _improtLog = "";

            foreach (var html in _googleHtmlTexts)
            {
                _improtLog += "html";
                string path = AssetDatabase.GetAssetPath(html);
                string text = File.ReadAllText(path);
                var lines = await GoogleHtmlTableParser.ParseTable(text);

                for (int i = 1; i < lines.GetLength(0); i++) // lines[0] is useless html data
                {
                    string[] cells = lines[i];
                    if (cells.Length >= _columnTargetIndex + 2 // cells[0] is <div>...</div>
                        && cells[1].ToLower() != "id"
                        && cells[1] != "" // id
                        && cells[_columnTargetIndex + 1] != "") // text
                    {
                        _textDataList.Add(new()
                        {
                            Id = cells[1],
                            Text = cells[_columnTargetIndex + 1]
                        });
                        _improtLog += ".";
                    }

                    await Task.Yield();
                }
            }
            _improtLog += "complete";
        }
#endif
    }
}