using UnityEngine.Rendering.Universal;
using Zenject;

namespace ValPackage.Common.Settings.GraphicSettings
{
    public class ShadowCascadeCount_GameSetting : Int_GameSetting
    {
        [Inject] private UniversalRenderPipelineAsset _urpAsset;

        public override void Apply()
        {
            base.Apply();
            _urpAsset.shadowCascadeCount = _value;
        }
    }
}