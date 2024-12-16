using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ValPackage.Common
{
    /// <summary>
    /// Have static List of every instances of class
    /// </summary>
    public abstract class CollectionMonobehaviour<TMono> : MonoBehaviour where TMono : CollectionMonobehaviour<TMono>
    {
        public static event Action<TMono> OnAdd, OnRemove;

        public static List<TMono> Collection = new();
        public static List<TMono> CollectionEnabled => Collection.Where(t => t.gameObject.activeInHierarchy).ToList();

        protected virtual void Awake()
        {
            Collection.Add(this as TMono);
            OnAdd?.Invoke(this as TMono);
        }

        protected virtual void OnDestroy()
        {
            Collection.Remove(this as TMono);
            OnRemove?.Invoke(this as TMono);
        }
    }
}