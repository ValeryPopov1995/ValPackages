using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common.Physic
{

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class TriggerZone : MonoCollection<Collider>
    {
        public event Action<Collider> OnEnter;
        public event Action<Collider> OnExit;
        public static event Action<TriggerZone, Collider> OnEnterStatic;
        public static event Action<TriggerZone, Collider> OnExitStatic;

        [InfoBox("Важно. При переключении 'Rigidbody.isKenematic' триггер срабатывает снова")]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private List<Collider> _collidersFilter = new();
        [SerializeField] private bool _oneUse = false;
        [SerializeField] private bool _debug;

        private bool _usedEnter = false;
        private bool _usedExit = false;

        [SerializeField] private UnityEvent<Collider> OnEnterEvent;
        [SerializeField] private UnityEvent<Collider> OnExitEvent;



        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;

            if (_layerMask == 0 && _collidersFilter.Count == 0)
                this.LogWarning("no conditions to invoke events");
        }

        public bool AddCollider(Collider other)
        {
            if (_collidersFilter.Contains(other)) return false;

            _collidersFilter.Add(other);
            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ColliderNotIncluded(other) || (_oneUse && _usedEnter))
                return;

            if (!TryAddItem(other)) return;

            if (!_usedEnter) _usedEnter = true;
            if (_debug) this.Log($"trigger enter {other.name}");
            OnEnter?.Invoke(other);
            OnEnterStatic?.Invoke(this, other);
            OnEnterEvent?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (ColliderNotIncluded(other) || (_oneUse && _usedExit))
                return;

            if (!TryRemoveItem(other)) return;

            if (!_usedExit) _usedExit = true;
            if (_debug) this.Log($"trigger exit {other.name}");
            OnExit?.Invoke(other);
            OnExitStatic?.Invoke(this, other);
            OnExitEvent?.Invoke(other);
        }

        private bool ColliderNotIncluded(Collider other)
        {
            return (1 << other.gameObject.layer & _layerMask) == 0
                && !_collidersFilter.Contains(other);
        }
    }
}