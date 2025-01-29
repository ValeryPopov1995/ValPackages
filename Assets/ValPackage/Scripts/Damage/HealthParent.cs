using ValPackage.Common.Extensions;
using System;
using UnityEngine;

namespace ValPackage.Common.Damage
{
    public class HealthParent : Health
    {
        public override float MaxHealth => _maxHealth;
        public override float CurrentHealth => _currentHealth;
        public override HealthParent Parent => this;
        [SerializeField, Min(0)] private float _maxHealth = 10;
        [SerializeField, Min(0)] private float _currentHealth = 10;
        private int _lastDamageHash;
        private int _lastHealHash;



        protected override void GetHitInternal(HitData hitData)
        {
            if (_lastDamageHash == hitData.Hash) return;
            _lastDamageHash = hitData.Hash;

            _currentHealth = Mathf.Clamp(CurrentHealth - hitData.Value, 0, MaxHealth);
            SpawnPrefabs(hitData);
            this.Log("take damage " + hitData.Value);
        }

        protected override void GetHealInternal(HitData hitData)
        {
            if (_lastHealHash == hitData.Hash) return;
            _lastHealHash = hitData.Hash;

            _currentHealth = Mathf.Clamp(CurrentHealth + hitData.Value, 0, MaxHealth);
            SpawnPrefabs(hitData);
            this.Log("take heal " + hitData.Value);
        }

        public override void SetHealth(float currentHealth, float maxHealth)
        {
            if (currentHealth <= 0 || maxHealth <= 0 || maxHealth < currentHealth)
                throw new ArgumentException();

            _currentHealth = currentHealth;
            _maxHealth = maxHealth;
        }
    }
}