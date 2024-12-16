using TMPro;
using UnityEngine;

namespace ValPackage.Common.Ui.ScrollSelect.Example
{
    public class ItemButton : SelectButton
    {
        [SerializeField] private TMP_Text _title;

        protected override void OnSelected()
        {
            _title.text = (_selectedData as ItemData).Title;
        }
    }
}
