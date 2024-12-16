using UnityEngine;

namespace ValPackage.Common.Ui.ScrollSelect.Example
{
    [CreateAssetMenu(menuName = "Scriptable/ValPackage/ScrollSelect/Item Data Example")]
    public class ItemData : SelectableData
    {
        [field: SerializeField] public string Title { get; private set; } = "No name";
        [field: SerializeField] public string Discription { get; private set; } = "No discription";
    }
}