using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace ValeryPopov.Common.Settings.GraphicSettings
{
    public class ShadowDistance_GraphicSetting : Int_GameSetting
    {
        [Tooltip("distance = sliderValue01 * _multiplyDistance")]
        private int _multiplyDistance = 50;
        [Inject] private UniversalRenderPipelineAsset _urpAsset;

        public override void Apply()
        {
            base.Apply();
            _urpAsset.shadowDistance = _value * _multiplyDistance;
        }
    }
}