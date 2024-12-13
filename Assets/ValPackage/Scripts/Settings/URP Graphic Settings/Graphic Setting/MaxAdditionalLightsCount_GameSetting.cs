using UnityEngine.Rendering.Universal;
using Zenject;

namespace ValeryPopov.Common.Settings.GraphicSettings
{
    public class MaxAdditionalLightsCount_GameSetting : Int_GameSetting
    {
        [Inject] private UniversalRenderPipelineAsset _urpAsset;

        public override void Apply()
        {
            base.Apply();
            _urpAsset.maxAdditionalLightsCount = _value;
        }
    }
}