using UnityEngine;

namespace ValPackage.Common.Settings.GraphicSettings
{
    public class Resolution_GameSetting : Int_GameSetting
    {
        [SerializeField] private FullScreen_GameSetting _fullscreen;

        public override void Apply()
        {
            base.Apply();
            var resolution = Screen.resolutions[_value];
            Screen.SetResolution(resolution.width, resolution.height, _fullscreen.FullScreenMode);
        }
    }
}