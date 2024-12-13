using UnityEngine.Rendering.Universal;
using Zenject;

namespace ValeryPopov.Common.Settings.GraphicSettings
{
    public class RenderPostProcessing_GameSetting : Bool_GameSetting
    {
        [Inject] private UniversalAdditionalCameraData _cameraData;

        public override void Apply()
        {
            base.Apply();
            _cameraData.renderPostProcessing = _value;
        }
    }
}