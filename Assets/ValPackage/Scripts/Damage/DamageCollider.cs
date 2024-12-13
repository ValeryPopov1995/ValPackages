using UnityEngine;

namespace ValeryPopov.Common.Damage
{
    public class DamageCollider : MonoBehaviour
    {
        [field: SerializeField] public DamageDealer DamageDealer { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
        [field: SerializeField] public int DestroyCollisionCount { get; private set; } = -1;
        private int _collisionCount;

        private void Awake()
        {
            if (!Collider)
                Collider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _collisionCount++;

            if (collision.gameObject.TryGetComponent<HealthParent>(out var health))
            {
                HitData hitData = new(
                    DamageDealer,
                    (collision.transform.transform.position - transform.position).normalized,
                    collision.contacts[0].point,
                    transform.position,
                    collision.contacts[0].otherCollider,
                    Collider);

                health.GetHit(hitData);
            }

            if (_collisionCount == DestroyCollisionCount)
                Destroy(gameObject);
        }
    }
}