using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ValeryPopov.Common.Ui
{
    /// <summary>
    /// UI window, container for ui elements
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class View : MonoBehaviour, IShowable
    {
        private const float _fadeDuration = .5f;

        public static event Action<View> OnShow;
        public static event Action<View> OnHide;
        private static Action<View, bool> onShowViewOnly;

        public enum ViewType { Window, Popup, }
        [field: SerializeField] public ViewType Type { get; private set; } = ViewType.Window;

        [SerializeField] private bool _hideOnStart = true;

        public CustomEvent OnShowEvent, OnHideEvent, OnEscapeEvent;

        private CanvasGroup _canvasGroup;
        private IViewElement[] _iViewElements;
        private bool _isShown = false;



        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            SetStartRect();
            _iViewElements = GetComponentsInChildren<IViewElement>();

            onShowViewOnly += HideOnEvent;
        }

        private void SetStartRect()
        {
            var rect = GetComponent<RectTransform>();
            rect.anchoredPosition = Vector3.zero;
        }

        private void Start()
        {
            if (_hideOnStart)
                gameObject.SetActive(false);
            else
                ShowAsync();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _isShown)
                OnEscapeEvent.TryInvoke();
        }

        private void OnDestroy()
        {
            onShowViewOnly -= HideOnEvent;
        }



        public async Task ShowAsync(bool hideOthers = true)
        {
            if (_isShown) return;

            gameObject.SetActive(true);

            for (int i = 0; i < _iViewElements.Length; i++)
                _iViewElements[i].OnViewShow();

            OnShow?.Invoke(this);
            OnShowEvent.TryInvoke();

            onShowViewOnly?.Invoke(this, hideOthers);
            await _canvasGroup.DOFade(1, _fadeDuration).AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
            _isShown = true;
        }

        private void HideOnEvent(View openedView, bool hideOthers)
        {
            if (this != openedView && Type == openedView.Type && hideOthers)
                _ = HideAsync();
        }

        public Task ShowAsync()
        {
            return ShowAsync(true);
        }

        public async Task HideAsync()
        {
            if (!_isShown) return;

            _isShown = false;
            for (int i = 0; i < _iViewElements.Length; i++)
                _iViewElements[i].OnViewHide();

            OnHide?.Invoke(this);
            OnHideEvent.TryInvoke();

            _canvasGroup.interactable = false;
            await _canvasGroup.DOFade(0, _fadeDuration).AsyncWaitForCompletion();
            gameObject.SetActive(false);
        }




        // for animation and UnityEvents
        [Button] public void Show() => _ = ShowAsync();
        [Button] public void Hide() => _ = HideAsync();

        [Button]
        public void HideImmidiatly()
        {
            if (!_isShown) return;

            _isShown = false;
            for (int i = 0; i < _iViewElements.Length; i++)
                _iViewElements[i].OnViewHide();

            OnHide?.Invoke(this);
            OnHideEvent.TryInvoke();

            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }
        public void DisableGameObject() => gameObject.SetActive(false);
    }
}