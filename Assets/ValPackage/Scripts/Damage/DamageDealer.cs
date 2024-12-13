using UnityEngine;

namespace ValeryPopov.Common.Damage
{
    [System.Serializable]
    public class DamageDealer
    {
        public DamageDealer(float value = 1, ElementType element = default, bool spawnBloodPrefab = true, GameObject damagePrefab = default)
        {
            Value = value;
            Element = element;
            SpawnBloodPrefab = spawnBloodPrefab;
            HitPrefab = damagePrefab;
        }

        [field: SerializeField, Min(0)] public float Value { get; protected set; } = 1;
        [field: SerializeField] public ElementType Element { get; protected set; }
        [field: SerializeField] public bool SpawnBloodPrefab { get; protected set; } = true;
        [field: SerializeField] public GameObject HitPrefab { get; protected set; }
    }
}