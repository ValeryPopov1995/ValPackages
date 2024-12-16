using UnityEngine;
using UnityEngine.UI;

namespace ValPackage.Common.Ui
{
    [RequireComponent(typeof(Slider))]
    public class SavedSlider : SavedUiComponent
    {
        [SerializeField] private float _defaultValue = 1;

        protected Slider _slider { get; private set; }

        protected virtual void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(UpdateOnChanged);
        }

        private void OnDestroy()
        {
            if (_slider != null)
                _slider.onValueChanged.RemoveListener(UpdateOnChanged);
        }

        private void UpdateOnChanged(float value)
        {
            if (_saveOnChanged) Save();
            OnSliderChanged(value);
            OnUiValueChanged?.Invoke();
        }

        protected virtual void OnSliderChanged(float value) { }

        public override void Load()
        {
            _slider.value = _saveSystem.LoadFloat(_saveFileType, _saveKey, _defaultValue);
        }

        public override void Save()
        {
            _saveSystem.Save(_saveFileType, _saveKey, _slider.value);
        }
    }
}