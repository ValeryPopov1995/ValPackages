using UnityEngine;

namespace ValeryPopov.Common.Settings.GraphicSettings
{
    public class FrameRate_GameSetting : Int_GameSetting
    {
        public override void Apply()
        {
            base.Apply();
            Application.targetFrameRate = _value * 10;
        }
    }
}