using TMPro;
using UnityEngine;

namespace ValPackage.Common.Ui.ScrollSelect.Example
{
    public class ItemScrollButton : SelectScrollButton
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _discription;

        protected override void UpdateVisual()
        {
            var item = _selectData as ItemData;
            _title.text = item.Title;
            _discription.text = item.Discription;
        }
    }
}
