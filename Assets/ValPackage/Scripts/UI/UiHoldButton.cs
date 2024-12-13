#if FMOD
using DG.Tweening;
using FMODUnity;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ValeryPopov.Common.Ui
{
    /// <summary>
    /// Holf button for N sec
    /// </summary>
    public class UiHoldButton : MonoBehaviour,
        IPointerUpHandler, IPointerDownHandler,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler, IPointerMoveHandler
    {
        public event Action OnStartPress, OnCancelPress, OnEndPress;

        [SerializeField] protected Image _circleBar;
        [SerializeField] private RectTransform _arrow;
        [SerializeField] private EventReference _startPressReference;
        [SerializeField, Range(.1f, 5)] protected float _fillDuration = 1;
        [SerializeField] private bool _debugMassages;

        public UnityEvent<float> OnProgress;
        public UnityEvent OnStartPressFx, OnCancelPressFx, OnEndPressFx;
        protected bool _locked;
        private Sequence _sequance;

        protected virtual void Awake()
        {
            _circleBar.fillAmount = 0;
        }

        private void OnEnable()
        {
            var forward = _arrow.DOLocalMoveY(-30, 1);
            var backward = _arrow.DOLocalMoveY(-35, 1);
            _sequance = DOTween.Sequence().Append(forward).Append(backward).SetLoops(-1);
            _sequance.Play();
        }
        private void OnDisable()
        {
            _sequance.Kill();
        }

        [Button]
        protected virtual void StartPress()
        {
            if (_locked) return;

            _circleBar.StartCoroutine(FillCircleBarEnumerator());
            OnStartPress?.Invoke();
            OnStartPressFx?.Invoke();
        }

        /// <summary>
        /// Отпускание курка с выделенной точки на карте
        /// </summary>
        [Button]
        protected virtual void EndPress()
        {
            _circleBar.StopAllCoroutines();

            if (_circleBar.fillAmount == 1)
            {
                OnEndPress?.Invoke();
                OnEndPressFx?.Invoke();
            }
            else
            {
                OnCancelPress?.Invoke();
                OnCancelPressFx?.Invoke();
            }

            _circleBar.fillAmount = 0;
            OnProgress?.Invoke(_circleBar.fillAmount);
        }

        protected IEnumerator FillCircleBarEnumerator()
        {
            _circleBar.fillAmount = 0;
            RuntimeManager.PlayOneShot(_startPressReference);

            while (_circleBar.fillAmount < 1)
            {
                _circleBar.fillAmount += Time.deltaTime / _fillDuration;
                OnProgress?.Invoke(_circleBar.fillAmount);
                yield return null;
            }

            _circleBar.fillAmount = 1;
            EndPress();
        }

        #region Interfaces

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            EndPress();
            if (_debugMassages) Debug.Log("[UiHoldButton] Up");
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            StartPress();
            if (_debugMassages) Debug.Log("[UiHoldButton] Down");
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            EndPress();
            if (_debugMassages) Debug.Log("[UiHoldButton] Exit");
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (_debugMassages) Debug.Log("[UiHoldButton] Enter");
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (_debugMassages) Debug.Log("[UiHoldButton] Click");
        }

        public virtual void OnPointerMove(PointerEventData eventData)
        {
            if (_debugMassages) Debug.Log("[UiHoldButton] Move");
        }

        #endregion
    }
}
#endif