using System;
using System.Collections.Generic;
using UnityEngine;

namespace ValeryPopov.Common.Physic
{
    public class TriggerZoneByComponent : MonoCollection<Collider>
    {
        public event Action<Collider> OnEnter, OnExit;
        private HashSet<Type> _componentFilter = new();

        private void Awake()
        {
            gameObject.AddComponent<Rigidbody>().isKinematic = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ColliderContainsFilter(other) && TryAddItem(other))
                OnEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (ColliderContainsFilter(other) && TryRemoveItem(other))
                OnExit?.Invoke(other);
        }



        public void AddFilter(Type componentType)
        {
            _componentFilter.Add(componentType);
        }

        private bool ColliderContainsFilter(Collider other)
        {
            if (_componentFilter.Count == 0) return true;

            foreach (var type in _componentFilter)
                if (other.GetComponent(type)) return true;

            return false;
        }

        public static TriggerZoneByComponent CreateTriggerZone(int radius, Transform parent = default)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            var collider = go.GetComponent<SphereCollider>();
            collider.radius = radius;
            collider.isTrigger = true;

            go.GetComponent<MeshRenderer>().enabled = false;

            var trigger = go.AddComponent<TriggerZoneByComponent>();

            if (parent) go.transform.SetParent(parent, false);

            return trigger;
        }
    }
}
