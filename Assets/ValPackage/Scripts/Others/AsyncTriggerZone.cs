using UnityEngine;

namespace ValPackage.Common
{
    public class AsyncTriggerZone : MonoBehaviour
    {
        public CustomEvent OnEnter, OnExit;

        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField, Min(.1f)] public float Radius { get; private set; } = 1;
        [field: SerializeField, Min(.1f)] public bool InRange { get; private set; }
        [SerializeField] private AsyncCircle _circle;

        private void Awake()
        {
            _circle.Initialize(UpdateTargetPosition, new[] { destroyCancellationToken });
        }

        private void Start()
        {
            _circle.Start();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, Radius);
        }

        public void UpdateTargetPosition()
        {
            bool inRange = Vector3.Distance(transform.position, Target.position) < Radius;
            if (inRange != InRange)
            {
                InRange = inRange;
                if (InRange) OnEnter.TryInvoke();
                else OnExit.TryInvoke();
            }
        }
    }
}