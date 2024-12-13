namespace ValeryPopov.Common.Settings
{
    public class Float_GameSetting : GameSetting<float>
    {
        protected override void Load()
        {
            _value = _saveSystem.LoadFloat(_saveFileType, SaveKey, _defaultValue);
        }

        public override void Save()
        {
            _saveSystem.Save(_saveFileType, SaveKey, _value);
        }
    }
}