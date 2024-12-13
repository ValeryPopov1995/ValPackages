using NaughtyAttributes;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ValeryPopov.Common.Mobile
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        public static event Action OnReorientation;

        [SerializeField] private bool _updateEachSecond = true;
        [Tooltip("Use it for coorect scale by CanvasScaler(Scale with screen size)")]
        [SerializeField] private bool _rescaleByParent = true;
        private Rect _lastSafeArea;
        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _rect.pivot = Vector2.zero;
            _rect.anchorMin = Vector2.zero;
            _rect.anchorMax = Vector2.zero;
        }

        private async void Start()
        {
            _lastSafeArea = Screen.safeArea;
            UpdateSafeArea();

            if (!_updateEachSecond) return;
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                TryUpdateSafeArea();
                await Task.Delay(1000);
            }
        }

        private void TryUpdateSafeArea()
        {
            if (_lastSafeArea == Screen.safeArea) return;
            _lastSafeArea = Screen.safeArea;
            UpdateSafeArea();
        }

        [Button]
        private void UpdateSafeArea()
        {
            _rect.position = _lastSafeArea.position;
            _rect.sizeDelta = _lastSafeArea.size;

            if (_rescaleByParent)
            {
                float rescale = 1 / _rect.parent.localScale.x;
                _rect.localScale = Vector3.one * rescale;
            }

            OnReorientation?.Invoke();
        }
    }
}