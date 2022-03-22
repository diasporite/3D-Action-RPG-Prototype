using System.Collections;

namespace RPG_Project
{
    public interface IDamageable
    {
        void OnDamage(float multiplier, Ability ability, Combatant instigator);
        IEnumerator OnDamageCo(float multiplier, Ability ability, Combatant instigator);
    }
}