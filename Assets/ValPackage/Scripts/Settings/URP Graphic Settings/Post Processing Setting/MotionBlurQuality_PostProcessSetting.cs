﻿using UnityEngine.Rendering.Universal;

namespace ValPackage.Common.Settings.GraphicSettings
{
    public class MotionBlurQuality_PostProcessSetting : PostProcessDrop_GameSetting<MotionBlur>
    {
        protected override void ApplyValue(MotionBlur volumeComponent)
        {
            volumeComponent.quality.Override((MotionBlurQuality)_value);
        }
    }
}