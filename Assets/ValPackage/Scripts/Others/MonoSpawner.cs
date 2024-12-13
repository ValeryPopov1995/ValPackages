using UnityEngine;

namespace ValeryPopov.Common
{
    /// <summary>
    /// Spawn prefabs on start, enable, disable, destroy
    /// </summary>
    public class MonoSpawner : MonoBehaviour
    {
        [field: SerializeField] public GameObject SpawnAtStart { get; set; }
        [field: SerializeField] public GameObject SpawnOnEnable { get; set; }
        [field: SerializeField] public GameObject SpawnOnDisable { get; set; }
        [field: SerializeField] public GameObject SpawnOnDestroy { get; set; }
        private bool _isQuting;

        private void Start()
        {
            if (SpawnAtStart)
                Instantiate(SpawnAtStart, transform.position, Quaternion.identity);
        }

        private void OnEnable()
        {
            if (SpawnOnEnable)
                Instantiate(SpawnOnEnable, transform.position, Quaternion.identity);
        }

        private void OnDisable()
        {
            if (SpawnOnDisable)
                Instantiate(SpawnOnDisable, transform.position, Quaternion.identity);
        }

        private void OnApplicationQuit()
        {
            _isQuting = true;
        }

        private void OnDestroy()
        {
            if (!_isQuting && SpawnOnDestroy)
                Instantiate(SpawnOnDestroy, transform.position, Quaternion.identity);
        }
    }
}