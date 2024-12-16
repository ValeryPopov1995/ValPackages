using UnityEngine.Rendering.Universal;
using Zenject;

namespace ValPackage.Common.Settings.GraphicSettings
{
    public class RenderScale_GameSetting : Float_GameSetting
    {
        [Inject] private UniversalRenderPipelineAsset _urpAsset;

        public override void Apply()
        {
            base.Apply();
            _urpAsset.renderScale = _value;
        }
    }
}