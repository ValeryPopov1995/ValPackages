using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using ValPackage.Common.Extantions;

namespace ValPackage.Common.StateMachineSystem
{
    public class StateMachine : MonoBehaviour
    {
        [field: SerializeField] public State CurrentState { get; private set; }
        [SerializeField] private bool _startMachineAtStart = true;
        private State[] _states;

        protected virtual void Awake()
        {
            _states = GetComponentsInChildren<State>();
        }

        private void Start()
        {
            if (_startMachineAtStart)
                StartMachine();
        }

        public void StartMachine()
        {
            if (CurrentState == null)
            {
                CurrentState = GetComponentInChildren<State>();
                this.Log("no current state used like first state, getted any in children");

                if (CurrentState == null)
                {
                    this.LogError("no current state used like first state");
                    return;
                }
            }

            CurrentState.StartState();
        }

        public void StopMachine()
        {
            CurrentState?.EndState();
        }

        public void SwitchState<T>() where T : State
        {
            SwitchState(_states.FirstOrDefault(x => x is T));
        }

        public async void SwitchState(State state)
        {
            if (!_states.Contains(state))
            {
                this.LogError("not my state to switch");
                return;
            }

            if (state == null)
            {
                this.LogError("no state to switch");
                return;
            }

            CurrentState?.EndState();
            CurrentState = state;
            await Task.Yield(); // some magic with CurrentState?.UpdateState() need this line
            CurrentState.StartState();
            this.Log($"{name} switched to state {CurrentState.name}");
        }
    }
}