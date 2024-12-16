using UnityEngine;
using UnityEngine.UI;

namespace ValPackage.Common.Ui.ScrollSelect
{
    [RequireComponent(typeof(Button))]
    public abstract class SelectScrollButton : MonoBehaviour
    {
        protected Button _button { get; private set; }
        protected SelectableData _selectData { get; private set; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Select);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Select);
        }

        private void Select()
        {
            GetComponentInParent<SelectScrollView>().Select(_selectData);
        }

        public virtual void SetData(SelectableData data)
        {
            _selectData = data;
            UpdateVisual();
        }

        protected abstract void UpdateVisual();
    }
}