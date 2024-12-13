using UnityEngine.Rendering.Universal;
using Zenject;

namespace ValeryPopov.Common.Settings.GraphicSettings
{
    public class AntialiasingSmaaQuality_GameSetting : Int_GameSetting
    {
        [Inject] private UniversalAdditionalCameraData _cameraData;

        public override void Apply()
        {
            base.Apply();
            _cameraData.antialiasingQuality = (AntialiasingQuality)_value;
        }
    }
}