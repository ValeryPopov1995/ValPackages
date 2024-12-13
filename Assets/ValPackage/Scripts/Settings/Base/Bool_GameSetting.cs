namespace ValeryPopov.Common.Settings
{
    public class Bool_GameSetting : GameSetting<bool>
    {
        protected override void Load()
        {
            _value = _saveSystem.LoadBool(_saveFileType, SaveKey, _defaultValue);
        }

        public override void Save()
        {
            _saveSystem.Save(_saveFileType, SaveKey, _value);
        }
    }
}