namespace ValPackage.Common.Settings
{
    public class Int_GameSetting : GameSetting<int>
    {
        protected override void Load()
        {
            _value = _saveSystem.LoadInt(_saveFileType, SaveKey, _defaultValue);
        }

        public override void Save()
        {
            _saveSystem.Save(_saveFileType, SaveKey, _value);
        }
    }
}