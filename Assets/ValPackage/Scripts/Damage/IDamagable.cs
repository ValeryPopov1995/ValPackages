using System;

namespace ValeryPopov.Common.Damage
{
    public interface IDamagable : IHitable
    {
        public event Action<HitData> OnGetDamage;
    }
}