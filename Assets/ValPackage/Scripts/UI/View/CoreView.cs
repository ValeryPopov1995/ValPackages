using DG.Tweening;
using System;
using UnityEngine;

namespace ValPackage.Common.Ui
{
    /// <summary>
    /// Хранит последние закрытые окна для метод возврата на предыдущее окно
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class CoreView : MonoBehaviour
    {
        public event Action OnShow, OnHide;

        public View LastHidenView { get; private set; }
        public View LastHidenViewWindow { get; private set; }
        public View LastHidenViewPopup { get; private set; }

        private CanvasGroup _canvasGroup;



        protected void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            var views = GetComponentsInChildren<View>();
            View.OnHide += UpdateLastHidenViews;
        }

        protected void OnDestroy()
        {
            var views = GetComponentsInChildren<View>();
            View.OnHide -= UpdateLastHidenViews;
        }



        public async void Show()
        {
            await _canvasGroup.DOFade(1, 1).AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
            OnShow?.Invoke();
        }

        public async void Hide()
        {
            _canvasGroup.interactable = false;
            await _canvasGroup.DOFade(0, 1).AsyncWaitForCompletion();
            OnHide?.Invoke();
        }

        public void HideImmidiatly()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
        }

        public void ShowPreviousWindow()
        {
            LastHidenViewWindow.ShowAsync();
        }

        private void UpdateLastHidenViews(View view)
        {
            LastHidenView = view;

            if (view.Type == View.ViewType.Window)
                LastHidenViewWindow = view;
            else
                LastHidenViewPopup = view;
        }
    }
}