using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using ValeryPopov.Common.Addressables;
using ValeryPopov.Common.SaveLoad;
using Zenject;

namespace ValeryPopov.Common.Settings
{
    public abstract class GameSetting : MonoBehaviour
    {
        [field: SerializeField] public string SaveKey { get; private set; }
        [Inject] protected SaveSystem _saveSystem;
        [Inject] protected SceneAddressableLoader _sceneAdrassableLoader;
        protected SaveFileType _saveFileType => SaveFileType.SettingsPreferences;



        private void Start()
        {
            LoadAndApply();
            Save(); // save defailt values
        }

        private void LoadAndApply()
        {
            Load();
            Apply();
        }

        /// <summary>
        /// Load value from SaveSystem
        /// </summary>
        protected abstract void Load();
        /// <summary>
        /// Save value to SaveSystem
        /// </summary>
        public abstract void Save();
        /// <summary>
        /// Apply loaded value
        /// </summary>
        public virtual void Apply() { }



#if UNITY_EDITOR
        // all settings you can change
        private void AllUrpSettings()
        {
            #region urp asset

            //GraphicsSettings.currentRenderPipeline;
            var asset = QualitySettings.renderPipeline as UniversalRenderPipelineAsset;

            //no asset.cascadeBorder = 1f; // need?
            //no asset.colorGradingLutSize = 1; // need?
            //no asset.colorGradingMode = ColorGradingMode.HighDynamicRange; // 0 1
            //no asset.conservativeEnclosingSphere = true; // need?
            asset.fsrOverrideSharpness = true; // need?
            asset.fsrSharpness = 1f; // need?
            //done asset.maxAdditionalLightsCount = 1;
            asset.msaaSampleCount = 1; // need?
            asset.numIterationsEnclosingSphere = 1; // need?
            //done asset.renderScale = 1f;
            //done asset.shadowCascadeCount = 1;
            asset.shadowDepthBias = 1f;
            //done asset.shadowDistance = 1f;
            asset.shadowNormalBias = 1f; // need?
            asset.storeActionsOptimization = StoreActionsOptimization.Store; // 0 1 2 // need?
            asset.supportsCameraDepthTexture = true; // need?
            asset.supportsCameraOpaqueTexture = true; // need?
            asset.supportsDynamicBatching = true; // need?
            asset.supportsHDR = true;
            asset.upscalingFilter = UpscalingFilterSelection.FSR; // 0 1 2 3 // need?
            asset.useAdaptivePerformance = true; // need?
            asset.useSRPBatcher = true; // need?

            #endregion

            #region volume

            VolumeProfile volume = new(); // SerializeField

            //done volume.TryGet(out Bloom bloom);
            //done volume.TryGet(out ChromaticAberration chromaticAberration);
            volume.TryGet(out ColorAdjustments colorAdjustments);
            colorAdjustments.colorFilter.value = Color.white;
            volume.TryGet(out ColorCurves colorCurves);
            //done volume.TryGet(out DepthOfField depthOfField);
            volume.TryGet(out FilmGrain filmGrain);
            volume.TryGet(out LensDistortion lensDistortion);
            volume.TryGet(out LiftGammaGain liftGammaGain);
            //done volume.TryGet(out MotionBlur motionBlur);
            volume.TryGet(out PaniniProjection paniniProjection);
            //done volume.TryGet(out Tonemapping tonemapping);
            //done volume.TryGet(out Vignette vignette);
            volume.TryGet(out WhiteBalance whiteBalance);

            //bloom.active = true;
            //bloom.intensity.value = 1;

            #endregion

            #region camera data

            var data = Camera.main.GetComponent<UniversalAdditionalCameraData>();

            //done data.antialiasing = AntialiasingMode.FastApproximateAntialiasing; // 0 1 2
            //done data.antialiasingQuality = AntialiasingQuality.High; // 0 1 2
            //done data.renderPostProcessing = true;
            //done data.renderShadows = true;

            #endregion

            //done Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
            //done Application.targetFrameRate = 30;
        }
#endif
    }

    public abstract class GameSetting<TValueType> : GameSetting
    {
        [SerializeField] protected TValueType _defaultValue;
        protected TValueType _value;
    }
}
