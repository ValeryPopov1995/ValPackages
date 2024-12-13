using UnityEngine;

namespace ValeryPopov.Common.Damage
{

    public enum ElementType
    {
        None,
        Fire,
        Ice,
        Heal
    }

    /// <summary>
    /// Damage or heal data
    /// </summary>
    public struct HitData
    {
        /// <summary>Damage/heal etc, minimum 0</summary>
        public float Value;
        public ElementType Element;
        /// <summary>Direction and value of hit force</summary>
        public Vector3 HitForce;
        /// <summary>World point of collision</summary>
        public Vector3 HitPoint;
        /// <summary>World point of hitted bullet, use it if have no <see cref="HitPoint"/></summary>
        public Vector3 BulletPoint;
        /// <summary><see cref="Collider"/> of hitted object</summary>
        public Collider HitCollider;
        public Collider BulletCollider;
        /// <summary>Is need spawn "blood" prefab</summary>
        public bool SpawnBloodPrefab;
        /// <summary>Spawnable object of hitting, not "blood"</summary>
        public GameObject HitPrefab;

        // TODO check optimization
        /// <summary>
        /// Use it for check multimple hitting per frame (for example, <see cref="HealthParent"/> get damage from every <see cref="HealthPart"/> by <see cref="DamageArea"/>)
        /// </summary>
        public int Hash { get; private set; }

        /*public override int GetHashCode()
        {
            return Tuple.Create(Value, Element, HitForce, HitPoint, BulletPoint, SpawnBloodPrefab)
                .GetHashCode();
        }*/

        public HitData(
            float value,
            ElementType element = default,
            Vector3 hitForce = default,
            Vector3 hitPoint = default,
            Vector3 bulletPoint = default,
            Collider hitCollider = default,
            Collider bulletCollider = default,
            bool spawnBloodPrefab = true,
            GameObject damagePrefab = default)
        {
            Hash = CreateHash();

            Value = value;
            Element = element;
            HitForce = hitForce;
            HitPoint = hitPoint;
            BulletPoint = bulletPoint;
            HitCollider = hitCollider;
            BulletCollider = bulletCollider;
            SpawnBloodPrefab = spawnBloodPrefab;
            HitPrefab = damagePrefab;
        }

        public HitData(
            DamageDealer damageDealer,
            Vector3 hitForce = default,
            Vector3 hitPoint = default,
            Vector3 bulletPoint = default,
            Collider hitCollider = default,
            Collider bulletCollider = default)
        {
            Hash = CreateHash();

            Value = damageDealer.Value;
            Element = damageDealer.Element;
            SpawnBloodPrefab = damageDealer.SpawnBloodPrefab;
            HitPrefab = damageDealer.HitPrefab;

            HitForce = hitForce;
            HitPoint = hitPoint;
            BulletPoint = bulletPoint;
            HitCollider = hitCollider;
            BulletCollider = bulletCollider;
        }

        public HitData(
            DamageDealer damageDealer,
            IHitable hitable,
            Vector3 hitForce = default,
            Vector3 hitPoint = default,
            Vector3 bulletPoint = default,
            Collider bulletCollider = default)
        {
            Hash = CreateHash();

            Value = damageDealer.Value;
            Element = damageDealer.Element;
            SpawnBloodPrefab = damageDealer.SpawnBloodPrefab && hitable.SpawnBloodPrefab;
            HitPrefab = damageDealer.HitPrefab;
            HitCollider = hitable.HitCollider;

            HitForce = hitForce;
            HitPoint = hitPoint;
            BulletPoint = bulletPoint;
            BulletCollider = bulletCollider;
        }

        private static int CreateHash()
        {
            return Random.Range(int.MinValue, int.MaxValue);
        }
    }
}