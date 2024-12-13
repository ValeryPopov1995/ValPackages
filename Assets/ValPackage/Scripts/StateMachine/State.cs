using System;
using UnityEngine;

namespace ValeryPopov.Common.StateMachineSystem
{
    public abstract class State : MonoBehaviour, IDisposable
    {
        public event Action OnStart, OneEnd;
        /// <summary>
        /// State was started and not ended
        /// </summary>
        public bool IsActive { get; private set; }
        protected StateMachine _stateMachine;

        protected virtual void Awake()
        {
            _stateMachine = GetComponentInParent<StateMachine>();
        }

        protected virtual void Start() { }

        public virtual void StartState()
        {
            IsActive = true;
            OnStart?.Invoke();
        }

        public virtual void EndState()
        {
            IsActive = false;
            OneEnd?.Invoke();
        }

        public virtual void SwitchToState()
        {
            _stateMachine.SwitchState(this);
        }

        public void Dispose()
        {
            IsActive = false;
        }
    }
}