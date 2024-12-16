using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ValPackage.Common.Ui.ScrollSelect
{
    [RequireComponent(typeof(Button))]
    public abstract class SelectButton : MonoBehaviour
    {
        [SerializeField] private SelectScrollView _scrollView;
        [SerializeField] private SelectScrollButton _scrollButtonPrefab;
        [SerializeField] protected SelectableData[] _datas;
        protected SelectableData _selectedData;
        private Button _button;

        protected virtual void Awake()
        {
            if (!_scrollView)
                _scrollView = FindAnyObjectByType<SelectScrollView>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Press);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(Press);
        }

        internal void Select(SelectableData data)
        {
            _selectedData = data;
            OnSelected();
        }

        protected abstract void OnSelected();

        private void Press()
        {
            _scrollView.Show(this, _scrollButtonPrefab, GetAvailableDatas().ToArray());
        }

        protected virtual IEnumerable<SelectableData> GetAvailableDatas() => _datas;
    }
}