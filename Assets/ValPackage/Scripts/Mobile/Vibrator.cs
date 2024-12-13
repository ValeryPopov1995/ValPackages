using System.Threading.Tasks;
using UnityEngine;
using ValeryPopov.Common.SaveLoad;
using Zenject;

namespace ValeryPopov.Common.Mobile
{
    public class Vibrator : Singleton<Vibrator>
    {
        private static bool _vibrateOn;
        [SerializeField] private string _vibrationSaveKey = "sets_vibration";
        [Inject] private SaveSystem _saveSystem;

        private async void Start()
        {
            await Task.Yield(); // await GameSetting.cs init
            UpdateVibrate(SaveFileType.SettingsPreferences);
            _saveSystem.OnSave += UpdateVibrate;
        }

        private void OnDestroy()
        {
            _saveSystem.OnSave -= UpdateVibrate;
        }

        private void UpdateVibrate(SaveFileType fileType)
        {
            if (fileType != SaveFileType.SettingsPreferences) return;
            _vibrateOn = _saveSystem.LoadBool(SaveFileType.SettingsPreferences, _vibrationSaveKey);
        }

        public static void Vibrate()
        {
            if (_vibrateOn)
                Handheld.Vibrate();
        }
    }
}