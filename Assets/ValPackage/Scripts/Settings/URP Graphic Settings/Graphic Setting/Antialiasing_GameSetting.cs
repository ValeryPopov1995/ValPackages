using UnityEngine.Rendering.Universal;
using Zenject;

namespace ValPackage.Common.Settings.GraphicSettings
{
    public class Antialiasing_GameSetting : Int_GameSetting
    {
        [Inject] private UniversalAdditionalCameraData _cameraData;

        public override void Apply()
        {
            base.Apply();
            _cameraData.antialiasing = (AntialiasingMode)_value;
        }
    }
}