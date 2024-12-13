using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace ValeryPopov.Common
{
    /// <summary>
    /// Update position on <see cref="Cinemachine.CinemachineSmoothPath"/> by <see cref="Target"/>
    /// </summary>
    public class NearestPathPoint : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public Cinemachine.CinemachineSmoothPath Path { get; private set; }
        [SerializeField] private bool _smoothTranslate = true;
        [SerializeField, ShowIf("_smoothTranslate")] private float _smoothDuration = 1;
        [SerializeField] private AsyncCircle _circle;

        private void Awake()
        {
            _circle.Initialize(SetPositionNearTarget, new[] { destroyCancellationToken });
        }

        private void Start()
        {
            _circle.Start();
        }

        public void SetPositionNearTarget()
        {
            if (_smoothTranslate)
                transform.DOMove(GetNearestPointOnPath(), _smoothDuration);
            else
                transform.position = GetNearestPointOnPath();
        }

        public Vector3 GetNearestPointOnPath()
        {
            return Path.EvaluatePosition(Path.FindClosestPoint(Target.position, 0, -1, 1));
        }
    }
}