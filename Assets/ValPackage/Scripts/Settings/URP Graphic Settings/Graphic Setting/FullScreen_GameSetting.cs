using UnityEngine;

namespace ValPackage.Common.Settings.GraphicSettings
{
    public class FullScreen_GameSetting : Int_GameSetting
    {
        public FullScreenMode FullScreenMode => (FullScreenMode)_value;

        public override void Apply()
        {
            base.Apply();
            Screen.fullScreenMode = FullScreenMode;
        }
    }
}