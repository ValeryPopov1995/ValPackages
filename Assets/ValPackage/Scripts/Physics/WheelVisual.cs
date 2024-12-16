using UnityEngine;

namespace ValPackage.Common.Physic
{
    [RequireComponent(typeof(WheelCollider))]
    public class WheelVisual : MonoBehaviour
    {
        private WheelCollider _wheelCollider;
        private Transform _visual;

        private void Awake()
        {
            _wheelCollider = GetComponent<WheelCollider>();
            _visual = transform.GetChild(0);
        }

        private void Update()
        {
            _wheelCollider.GetWorldPose(out var pos, out var rot);
            _visual.position = pos;
            _visual.rotation = rot;
        }
    }
}