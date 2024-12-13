namespace ValeryPopov.Common.Items
{
    public interface IInventory
    {
        /// <summary>
        /// Double-sided action to drop item from inventory. Also can use <see cref="IItem.GrabByInventory(IInventory)"/>.
        /// In theory, all logic is in the inventory, and the item only refers to the method in the inventory.
        /// Dont forget set <see cref="IItem.AttachedInventory"/>
        /// </summary>
        void Grab(IItem item);

        /// <summary>
        /// Double-sided action to drop item from inventory. Also can use <see cref="IItem.DropFromInventory"/>.
        /// In theory, all logic is in the inventory, and the item only refers to the method in the inventory.
        /// Dont forget set <see cref="IItem.AttachedInventory"/>
        /// </summary>
        void Drop(IItem item);
    }
}
