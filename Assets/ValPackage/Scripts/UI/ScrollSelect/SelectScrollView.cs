using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ValeryPopov.Common.Ui.ScrollSelect
{
    [RequireComponent(typeof(ScrollRect))]
    [RequireComponent(typeof(CanvasGroup))]
    public class SelectScrollView : MonoBehaviour
    {
        public event Action OnShow, OnHide;

        [SerializeField] private Transform _content;
        [SerializeField, Min(0)] private float _fadeDuration = 1;
        [SerializeField] private bool _hideOnSecondClick = true;
        private bool _isShown => _canvasGroup.alpha > 0;
        private CanvasGroup _canvasGroup;
        private SelectScrollButton[] _buttons;
        private SelectButton _selectButton;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public async void Show(SelectButton selectButton, SelectScrollButton buttonPrefab, SelectableData[] variants)
        {
            if (_selectButton == selectButton && _hideOnSecondClick && _isShown)
            {
                _ = Hide();
                return;
            }
            else if (_isShown) // if click on other button bit it shown
                await Hide();

            _selectButton = selectButton;
            _buttons = new SelectScrollButton[variants.Length];
            for (int i = 0; i < variants.Length; i++)
            {
                _buttons[i] = Instantiate(buttonPrefab, _content);
                _buttons[i].SetData(variants[i]);
            }

            _canvasGroup.blocksRaycasts = true;
            await _canvasGroup.DOFade(1, _fadeDuration).AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
            OnShow?.Invoke();
        }

        public async Task Hide()
        {
            _canvasGroup.interactable = false;
            await _canvasGroup.DOFade(0, _fadeDuration).AsyncWaitForCompletion();
            _canvasGroup.blocksRaycasts = false;

            DestroyButtons();
            OnHide?.Invoke();
        }

        public void Select(SelectableData data)
        {
            _selectButton.Select(data);
            _ = Hide();
        }

        private void DestroyButtons()
        {
            if (_buttons == null) return;
            for (int i = 0; i < _buttons.Length; i++)
                Destroy(_buttons[i].gameObject);
            _buttons = null;
        }
    }
}