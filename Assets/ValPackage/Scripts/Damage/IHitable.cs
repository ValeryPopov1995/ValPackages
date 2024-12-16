using UnityEngine;

namespace ValPackage.Common.Damage
{
    public interface IHitable
    {
        public Collider HitCollider { get; }
        public bool SpawnBloodPrefab { get; }
        public GameObject BloodPrefab { get; }

        public void GetHit(HitData hit);
    }
}