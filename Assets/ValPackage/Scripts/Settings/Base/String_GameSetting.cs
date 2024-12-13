namespace ValeryPopov.Common.Settings
{
    public class String_GameSetting : GameSetting<string>
    {
        protected override void Load()
        {
            _value = _saveSystem.LoadString(_saveFileType, SaveKey, _defaultValue);
        }

        public override void Save()
        {
            _saveSystem.Save(_saveFileType, SaveKey, _value);
        }
    }
}
