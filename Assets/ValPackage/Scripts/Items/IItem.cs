using UnityEngine;

namespace ValPackage.Common.Items
{
    public interface IItem : IInteractable
    {
        Sprite Icon { get; }
        string Name { get; }
        string Discription { get; }

        bool IsInInventory { get; }
        bool IsOnFloor => !IsInInventory;
        bool IsPickable { get; set; }
        bool Physics { get; set; }

        IInventory AttachedInventory { get; set; }
        GameObject gameObject { get; }
        Transform transform => gameObject.transform;
        string name => gameObject.name;
        bool activeSelf
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        /// <summary>
        /// Double-sided action to pick up item by inventory. Also can use <see cref="IInventory.Grab(IItem)"/>.
        /// In theory, all logic is in the inventory, and the item only refers to the method in the inventory.
        /// Dont forget set <see cref="IItem.AttachedInventory"/>
        /// </summary>
        /// <param name="inventory">attachable inventory/></param>
        void GrabByInventory(IInventory inventory);
        /// <summary>
        /// Double-sided action to drop item from inventory. Also can use <see cref="IInventory.Drop(IItem)"/>.
        /// In theory, all logic is in the inventory, and the item only refers to the method in the inventory.
        /// Dont forget set <see cref="IItem.AttachedInventory"/>
        /// </summary>
        void DropFromInventory();
    }
}