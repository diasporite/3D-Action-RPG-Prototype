using System.Collections;

namespace RPG_Project
{
    public interface IDamageable
    {
        void OnDamage(int baseDamage, BattleChar instigator);
        IEnumerator OnDamageCo(int baseDamage, BattleChar instigator);
    }
}