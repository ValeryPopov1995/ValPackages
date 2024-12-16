using TMPro;
using UnityEngine;
using ValPackage.Common.Extantions;

namespace ValPackage.Common.Localization
{
    public class TextLocalizationController : LocalizationController
    {
        [Tooltip("Если не назначено - берет с GetComponent()")]
        [SerializeField] private TMP_Text _text;



        protected override void Awake()
        {
            base.Awake();
            _text ??= GetComponent<TMP_Text>();
        }

        protected override void SetData()
        {
            if (_localizationManager == null)
            {
                this.LogWarning("no localization manager");
                _text.text = ID;
                return;
            }

            var textData = _localizationManager.GetTextData(ID);

            if (textData == null || textData.Text == string.Empty)
            {
                this.LogWarning("no text id localization, used id of text");
                _text.text = ID;
            }
            else
            {
                _text.text = textData.Text;

                if (_localizationManager.DefaultFont)
                    _text.font = _localizationManager.DefaultFont;
            }
        }

        public void SetText(string id)
        {
            ID = id;
            SetData();
        }
    }
}