using System.Collections;

namespace RPG_Project
{
    public interface IDamageable
    {
        void OnDamage(int healthDamage, int poiseDamage);
        IEnumerator OnDamageCo(int healthDamage, int poiseDamage);
    }
}