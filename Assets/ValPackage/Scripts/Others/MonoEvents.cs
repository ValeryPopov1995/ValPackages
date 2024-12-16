using UnityEngine;
using UnityEngine.Events;

namespace ValPackage.Common
{
    public class MonoEvents : MonoBehaviour
    {
        [SerializeField] public UnityEvent OnStart, OnEnabled, OnDisabled, OnDestroyed;

        private void Start()
        {
            OnStart?.Invoke();
        }

        private void OnEnable()
        {
            OnEnabled?.Invoke();
        }

        private void OnDisable()
        {
            OnDisabled?.Invoke();
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}