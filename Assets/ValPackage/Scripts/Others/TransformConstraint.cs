using NaughtyAttributes;
using UnityEngine;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common
{

    public class TransformConstraint : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private bool _position = true;
        [SerializeField] private bool _rotation = true;
        [Space]
        [SerializeField] private bool _getOffsetsOnAwake = true;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Quaternion _rotate;

        private void Awake()
        {
            if (!_target)
            {
                this.LogError("no target");
                return;
            }

            if (!_getOffsetsOnAwake) return;

            if (_position)
                GetOffset();

            if (_rotation)
                GetRotate();
        }

        private void Update()
        {
            if (_position)
                transform.position = _target.TransformPoint(_offset);

            if (_rotation)
                transform.rotation = _target.rotation * _rotate;
        }



        [Button]
        private void GetOffset()
        {
            _offset = _target.InverseTransformPoint(transform.position);
        }

        [Button]
        private void GetRotate()
        {
            _rotate = Quaternion.Euler(transform.eulerAngles - _target.eulerAngles);
        }
    }
}