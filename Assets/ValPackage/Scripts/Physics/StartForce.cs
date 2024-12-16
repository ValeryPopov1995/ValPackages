using UnityEngine;

namespace ValPackage.Common.Physic
{
    [RequireComponent(typeof(Rigidbody))]
    public class StartForce : MonoBehaviour
    {
        [SerializeField] private Vector3Int _startForce = new(0, 0, 10);
        [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;

        private void Start()
        {
            GetComponent<Rigidbody>().AddForce(transform.TransformDirection(_startForce), _forceMode);
        }
    }
}