using System;
using UnityEngine;

namespace ValeryPopov.Common.Damage
{
    public class HealthPart : Health, IServiceable
    {
        public override float MaxHealth => _healthParent.MaxHealth;
        public override float CurrentHealth => _healthParent.CurrentHealth;
        public override HealthParent Parent => _healthParent;
        [field: SerializeField, Range(.1f, 3)] public float DamageMultiply { get; private set; } = 1;
        private HealthParent _healthParent;



        private void Awake()
        {
            _healthParent = GetComponentInParent<HealthParent>();
        }

        protected override void GetHitInternal(HitData hitData)
        {
            hitData.Value *= DamageMultiply;
            SpawnPrefabs(hitData);
            hitData.SpawnBloodPrefab = false;
            _healthParent.GetHit(hitData);
        }

        protected override void GetHealInternal(HitData hitData)
        {
            _healthParent.GetHeal(hitData);
            SpawnPrefabs(hitData);
        }

        public override void SetHealth(float currentHealth, float maxHealth)
        {
            _healthParent.SetHealth(currentHealth, maxHealth);
        }

        public string CheckServiceable()
        {
            int outside = GetComponentsInParent<HealthParent>().Length;
            int inside = GetComponentsInChildren<HealthParent>().Length;

            if (outside == 0) return "no HealthParent";
            if (outside > 1) return "multiple HealthParents";
            if (inside > 0) return "HealthParent inside";

            return "";
        }
    }
}