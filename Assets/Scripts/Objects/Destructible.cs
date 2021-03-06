using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Destructible : MonoBehaviour, IDamageable
    {
        public int hp = 100;
        public int def = 50;

        //public PointStat health { get; private set; }
        //public Stat defence { get; private set; }

        public PointStat health;
        public Stat defence;

        Hurtbox hurtbox;

        private void Awake()
        {
            health = new PointStat(hp, hp, hp);
            defence = new Stat(def, def);

            hurtbox = GetComponentInChildren<Hurtbox>();
        }

        private void Start()
        {
            hurtbox.Init(this);
        }

        public void OnDamage(float multiplier, Ability ability, Combatant instigator)
        {
            var damage = 1;

            if (instigator != null) damage = Mathf.RoundToInt(multiplier * 
                GameManager.instance.Combat.GetDamage(instigator.Character.Atk, defence.CurrentStatValue));

            //var damage = GameManager.instance.Combat.GetDamage(baseDamage, , defence);

            health.ChangeCurrentPoints(-damage);
            if (health.PointValue <= 0) DestroyObject();
        }

        public IEnumerator OnDamageCo(float multiplier, Ability ability, Combatant instigator)
        {
            yield return null;
        }

        void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}