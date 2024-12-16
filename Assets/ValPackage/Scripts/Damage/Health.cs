using System;
using UnityEngine;

namespace ValPackage.Common.Damage
{
    public abstract class Health : MonoBehaviour, IDamagable, IHealable
    {
        public event Action<HitData> OnGetDamage, OnGetHeal, OnDie;

        public bool IsAlive => CurrentHealth > 0;
        public bool IsDead => !IsAlive;
        public float CurrentHealth01 => CurrentHealth / MaxHealth;

        public abstract float MaxHealth { get; }
        public abstract float CurrentHealth { get; }
        public abstract HealthParent Parent { get; }

        [field: SerializeField] public bool TryBloodPrefabFromParent { get; private set; } = true;
        [field: SerializeField] public Collider HitCollider { get; protected set; }
        [field: SerializeField] public bool SpawnBloodPrefab { get; protected set; } = true;
        [field: SerializeField] public GameObject BloodPrefab { get; protected set; }



        public virtual void GetHit(HitData hitData)
        {
            if (hitData.Value <= 0 || IsDead) return;
            GetHitInternal(hitData);
            OnGetDamage?.Invoke(hitData);

            if (CurrentHealth <= 0)
                Die(hitData);
        }

        protected abstract void GetHitInternal(HitData hitData1);

        protected virtual void Die(HitData hitData)
        {
            OnDie?.Invoke(hitData);
        }

        public virtual void GetHeal(HitData hitData)
        {
            if (hitData.Value <= 0 || IsDead || CurrentHealth >= MaxHealth) return;
            GetHealInternal(hitData);
            OnGetHeal?.Invoke(hitData);
        }

        protected abstract void GetHealInternal(HitData hitData);



        public void SetHealth(float currentMaxHealth) => SetHealth(currentMaxHealth, currentMaxHealth);

        public abstract void SetHealth(float currentHealth, float maxHealth);



        /// <summary>Spawn blood and hit objects</summary>
        protected void SpawnPrefabs(HitData hitData)
        {
            if (!hitData.SpawnBloodPrefab && !hitData.HitPrefab) return;

            var pos = transform.position;
            if (hitData.HitPoint != default) pos = hitData.HitPoint;
            else if (hitData.HitCollider) pos = hitData.HitCollider.transform.position;

            var rot = Quaternion.identity;
            if (hitData.HitForce != default) rot = Quaternion.FromToRotation(Vector3.forward, hitData.HitForce);

            if (hitData.HitPrefab)
                Instantiate(hitData.HitPrefab, pos, rot);

            if (hitData.SpawnBloodPrefab)
            {
                if (BloodPrefab)
                    Instantiate(BloodPrefab, pos, rot);
                else if (TryBloodPrefabFromParent && Parent.BloodPrefab)
                    Instantiate(Parent.BloodPrefab, pos, rot);
            }
        }
    }
}