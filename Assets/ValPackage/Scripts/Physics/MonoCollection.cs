using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ValPackage.Common.Physic
{
    public abstract class MonoCollection<TItem> : MonoBehaviour where TItem : Component
    {
        public List<TItem> Items = new();
        public TItem Nearest => Items
            .OrderBy(item => Vector3.Distance(transform.position, item.transform.position))
            .FirstOrDefault();

        public bool Contains(TItem item) => Items.Contains(item);

        protected bool TryAddItem(TItem item)
        {
            ClearNullItems();
            if (Items.Contains(item)) return false;

            Items.Add(item);
            return true;
        }

        protected bool TryRemoveItem(TItem item)
        {
            ClearNullItems();
            if (!Items.Contains(item)) return false;

            Items.Remove(item);
            return true;
        }

        protected void ClearNullItems()
        {
            Items = Items.Where(x => x != null).ToList();
        }
    }
}