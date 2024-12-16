using System;
using UnityEngine;

namespace ValPackage.Common
{
    /// <summary>
    /// Set transform before target, rotating toward target
    /// </summary>
    public class ForwardOffsetPositioner : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _targetOffset;
        [SerializeField] private float _angleToUpdateTargetPosition = 30;
        [SerializeField] private bool _onlyHorizontalOffset = false;
        [SerializeField, Range(.01f, .99f)] private float _lerpPositon = .1f;
        [SerializeField, Range(.01f, .99f)] private float _lerpRotation = .1f;
        Quaternion _lookRotation => Quaternion.LookRotation(transform.position - _target.position);
        private Vector3 _worldOffset;



        private void OnEnable()
        {
            UpdateImmidiatly();
        }

        private void Update()
        {
            var canvasDirection = transform.position - _target.position;
            if (Vector3.Angle(_target.forward, canvasDirection) > _angleToUpdateTargetPosition)
                _worldOffset = UpdateTargetPosition();

            transform.position = Vector3.Lerp(transform.position, _target.position + _worldOffset, _lerpPositon);
            transform.rotation = Quaternion.Lerp(transform.rotation, _lookRotation, _lerpRotation);
        }

        private Vector3 UpdateTargetPosition()
        {
            Vector3 worldOffset;

            if (_onlyHorizontalOffset)
            {
                worldOffset = _target.TransformPoint(_targetOffset) - _target.position;
                worldOffset.y = _targetOffset.y;
            }
            else
                worldOffset = _target.TransformPoint(_targetOffset) - _target.position;

            return worldOffset;
        }

        private void UpdateImmidiatly()
        {
            _worldOffset = UpdateTargetPosition();
            transform.position = _target.position + _worldOffset;
            transform.rotation = _lookRotation;
        }
    }
}