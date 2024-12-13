using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common.Damage
{
    public class DamageArea : MonoBehaviour
    {
        [field: SerializeField] public DamageDealer DamageDealer { get; set; }
        [field: SerializeField, Min(0)] public float Radius { get; set; } = 1;
        [field: SerializeField, Min(0)] public float HitForceMagnitude { get; set; } = 1;
        [field: SerializeField] public bool IsDamageOnStart { get; set; } = true;
        [field: SerializeField] public bool IsDestroyAfterDamage { get; private set; }



        protected virtual void Start()
        {
            if (IsDamageOnStart)
                DealDamage();
        }

        public virtual void DealDamage()
        {
            var persievables = OverlapPercievables();

            persievables.ForEach(hitable =>
            {
                HitData hitData = new(DamageDealer, hitable);
                hitable.GetHit(hitData);
            });

            if (IsDestroyAfterDamage)
                Destroy(gameObject);
        }

        protected IEnumerable<IHitable> OverlapPercievables()
        {
            return Physics.OverlapSphere(transform.position, Radius)
                .Select(c => c.GetComponent<IHitable>())
                .Where(h => h != null);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
#endif
    }
}