using System;

namespace ValPackage.Common.Damage
{
    public interface IHealable : IHitable
    {
        public event Action<HitData> OnGetHeal;
        public void GetHeal(HitData hitData);
    }
}