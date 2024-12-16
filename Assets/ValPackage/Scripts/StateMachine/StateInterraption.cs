using UnityEngine;

namespace ValPackage.Common.StateMachineSystem
{
    /// <summary>
    /// Base class for state interruptions
    /// </summary>
    public abstract class StateInterraption : MonoBehaviour
    {
        [SerializeField] private State _toState;
        private StateMachine _stateMachine;

        protected virtual void Awake()
        {
            _stateMachine = GetComponentInParent<StateMachine>();
        }

        /// <summary>
        /// <see cref="StateMachine.SwitchState(State)"/> by internal conditions
        /// </summary>
        /// <returns></returns>
        protected virtual bool TryInterrapt()
        {
            _stateMachine.SwitchState(_toState);
            return true;
        }
    }
}