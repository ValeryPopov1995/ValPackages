using NaughtyAttributes;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ValPackage.Common.Physic
{
    public class Ragdoll : MonoBehaviour
    {
        public bool RagdollEnabled { get; private set; } = true; // for Start()
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [SerializeField] private Transform _hips;
        [SerializeField] private Animator _animator;
        private Rigidbody[] _rigidbodies;

        private void Awake()
        {
            // TODO not work ??=
            _hips ??= GetComponent<Transform>();
            _animator ??= GetComponent<Animator>();
            Agent ??= GetComponent<NavMeshAgent>();

            _rigidbodies = _hips.GetComponentsInChildren<Rigidbody>();
        }

        private void Start()
        {
            EnableRagdoll(false);
        }



        public void EnableRagdoll(bool state)
        {
            if (RagdollEnabled == state) return;

            _animator.enabled = !state;
            if (Agent) Agent.enabled = !state;

            foreach (var rb in _rigidbodies)
                rb.isKinematic = !state;

            if (state)
                foreach (var rb in _rigidbodies)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

            RagdollEnabled = state;
        }

        public async Task EnableRagdoll(bool state, float duration)
        {
            if (RagdollEnabled == state) return;

            EnableRagdoll(state);
            await Task.Delay(TimeSpan.FromSeconds(duration));
            EnableRagdoll(!state);
        }

        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Impulse)
        {
            foreach (var rb in _rigidbodies)
                rb.AddForce(force, forceMode);
        }



        [Button] private void EnableRagdoll() => EnableRagdoll(true);
        [Button] private void DisableRagdoll() => EnableRagdoll(false);
    }
}