using NaughtyAttributes;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ValPackage.Common.Mobile
{
    [RequireComponent(typeof(RectTransform))]
    public class OrientationPositioner : MonoBehaviour
    {
        [Serializable]
        private struct RectTransformAccess
        {
            public Vector3 AnchoredPosition;
            public Quaternion LocalRotation;
            public Vector2 LocalScale;
            public Vector2 Size;

            public RectTransformAccess(RectTransform rect)
            {
                AnchoredPosition = rect.anchoredPosition;
                LocalRotation = rect.localRotation;
                LocalScale = rect.localScale;
                Size = rect.sizeDelta;
            }

            public void Apply(RectTransform rect)
            {
                rect.anchoredPosition = AnchoredPosition;
                rect.localRotation = LocalRotation;
                rect.localScale = LocalScale;
                rect.sizeDelta = Size;
            }
        }

        [SerializeField] private bool _updateEachSecond = false;
        [SerializeField] private bool _updateOnSafeArea = true;
        [SerializeField] private RectTransformAccess _portraitPosition, _landscapePosition;
        private RectTransform _rect;
        private ScreenOrientation _lastOrientation;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private async void Start()
        {
            await Task.Yield(); // await SafeArea
            _lastOrientation = Screen.orientation;
            UpdatePosition();

            if (_updateOnSafeArea)
                SafeArea.OnReorientation += TryUpdatePosition;

            if (!_updateEachSecond) return;
            while (!destroyCancellationToken.IsCancellationRequested) // TODO Optimization - only one instance update orientation
            {
                TryUpdatePosition();
                await Task.Delay(1000);
            }
        }

        private void OnDestroy()
        {
            if (_updateOnSafeArea)
                SafeArea.OnReorientation -= TryUpdatePosition;
        }

        private void TryUpdatePosition()
        {
            if (_lastOrientation == Screen.orientation) return;
            _lastOrientation = Screen.orientation;
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (_lastOrientation == ScreenOrientation.Portrait
                || _lastOrientation == ScreenOrientation.PortraitUpsideDown)
                _portraitPosition.Apply(_rect);
            else
                _landscapePosition.Apply(_rect);
        }

#if UNITY_EDITOR
        [Button]
        private void CopyCurrentPositionToPortrait() => _portraitPosition = new(GetComponent<RectTransform>());
        [Button]
        private void CopyCurrentPositionToLandscape() => _landscapePosition = new(GetComponent<RectTransform>());
#endif
    }
}