using UnityEngine;
using UnityEngine.UI;

namespace ValPackage.Common.Ui
{
    /// <summary>
    /// Save bool by <see cref="SaveLoad.SaveSystem"/>
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class SavedToggle : SavedUiComponent
    {
        [SerializeField] private bool _defaultValue = true;
        protected Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(UpdateOnChanged);
        }

        private void OnDestroy()
        {
            if (_toggle != null)
                _toggle.onValueChanged.RemoveListener(UpdateOnChanged);
        }

        private void UpdateOnChanged(bool arg0)
        {
            if (_saveOnChanged) Save();
            OnUiValueChanged?.Invoke();
        }

        public override void Load()
        {
            _toggle.isOn = _saveSystem.LoadBool(_saveFileType, _saveKey, _defaultValue);
        }

        public override void Save()
        {
            _saveSystem.Save(_saveFileType, _saveKey, _toggle.isOn);
        }
    }
}