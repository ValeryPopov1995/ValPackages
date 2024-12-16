using System;

namespace ValPackage.Common.Damage
{
    public interface IDamagable : IHitable
    {
        public event Action<HitData> OnGetDamage;
    }
}