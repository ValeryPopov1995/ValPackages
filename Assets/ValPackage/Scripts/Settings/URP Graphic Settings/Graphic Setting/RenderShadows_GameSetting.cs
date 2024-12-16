using UnityEngine.Rendering.Universal;
using Zenject;

namespace ValPackage.Common.Settings.GraphicSettings
{
    public class RenderShadows_GameSetting : Bool_GameSetting
    {
        [Inject] private UniversalAdditionalCameraData _cameraData;

        public override void Apply()
        {
            base.Apply();
            _cameraData.renderShadows = _value;
        }
    }
}