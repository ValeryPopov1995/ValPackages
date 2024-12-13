using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using ValeryPopov.Common.Addressables;
using ValeryPopov.Common.Extantions;
using Zenject;

namespace ValeryPopov.Common.Rendering
{
    public class ScreenFader : MonoBehaviour
    {
        private enum FadeOnStartType
        {
            FadeOnStart,
            UnfadeOnStart,
            DoNothing
        }

        [Header("PostProcessing Volumes")]

        [SerializeField, ColorUsage(showAlpha: true)] private Color _fadeColor = Color.black;
        [SerializeField, ColorUsage(showAlpha: true)] private Color _unfadeColor = Color.white;
        [field: SerializeField, Min(0)] public float Duration { get; private set; } = 2.5f;
        [SerializeField] private FadeOnStartType _fadeOnStartType = FadeOnStartType.UnfadeOnStart;
        [Inject] private SceneAddressableLoader _sceneLoader;
        private VolumeProfile _profile;

        private void Awake()
        {
            StartScene();
        }

        private void Start()
        {
            _sceneLoader.OnSceneLoaded += StartScene;
        }

        private void OnDestroy()
        {
            _sceneLoader.OnSceneLoaded -= StartScene;
        }

        private void StartScene()
        {
            var volume = FindAnyObjectByType<Volume>();
            if (volume == null)
            {
                this.LogWarning("profile not assigmed, no Volume on scene?");
                return;
            }

            _profile = volume.profile;
            if (_profile.TryGet(out ColorAdjustments col))
                col.colorFilter.overrideState = true;

            FadeOnStart();

        }

        private void FadeOnStart()
        {
            switch (_fadeOnStartType)
            {
                case FadeOnStartType.FadeOnStart:
                    _ = Fade();
                    break;
                case FadeOnStartType.UnfadeOnStart:
                    _ = Unfade();
                    break;
            }
        }

        [ContextMenu("Fade"), Button]
        public async Task Fade()
        {
            ColorAdjustments col = null;
            _profile?.TryGet(out col);
            if (_profile == null || col == null)
            {
                this.LogWarning("no fader on scene");
                return;
            }

            col.colorFilter.value = _unfadeColor;
            DOTween.To(() => col.colorFilter.value, x => col.colorFilter.value = x, _fadeColor, Duration);
            await Task.Delay(TimeSpan.FromSeconds(Duration));
        }

        [ContextMenu("Unfade"), Button]
        public async Task Unfade()
        {
            ColorAdjustments col = null;
            _profile?.TryGet(out col);
            if (_profile == null || col == null)
            {
                this.LogWarning("no fader on scene");
                return;
            }

            col.colorFilter.value = _fadeColor;
            DOTween.To(() => col.colorFilter.value, x => col.colorFilter.value = x, _unfadeColor, Duration);
            await Task.Delay(TimeSpan.FromSeconds(Duration));
        }
    }
}