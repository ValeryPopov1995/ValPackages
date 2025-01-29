using UnityEngine;
using UnityEngine.UI;
using ValPackage.Common.Extensions;
using ValPackage.Common.Ui;

namespace ValPackage.Common.Settings
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] private Button _apply, _cancel;
        private SavedUiComponent[] _uiSettings;
        private GameSetting[] _settings;

        private void Start()
        {
            _uiSettings = GetComponentsInChildren<SavedUiComponent>();
            _settings = GetComponentsInChildren<GameSetting>();

            if (_apply)
            {
                _apply.onClick.AddListener(Apply);

                foreach (var setting in _uiSettings)
                    setting.OnUiValueChanged += EnableButtons;
            }

            _cancel?.onClick.AddListener(Cancel);
        }

        private void EnableButtons() => SetButtons(true);

        private void SetButtons(bool interactable)
        {
            if (_apply) _apply.interactable = interactable;
            if (_cancel) _cancel.interactable = interactable;
        }

        private void OnEnable()
        {
            SetButtons(false);
        }

        private void Apply()
        {
            _uiSettings.ForEach(setting => setting.Save());
            _settings.ForEach(setting => setting.Apply());
        }

        private void Cancel()
        {
            foreach (var setting in _uiSettings)
                setting.Load();

            SetButtons(false);
        }
    }
}