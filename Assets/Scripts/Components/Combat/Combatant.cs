using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Combatant : MonoBehaviour, IDamageable
    {
        public OnHealthChanged onHealthChanged;
        public OnStaminaChanged onStaminaChanged;
        public OnPoiseChanged onPoiseChanged;

        [SerializeField] bool player = false;

        bool invincible = false;

        Controller controller;

        Health health;
        Stamina stamina;
        Poise poise;

        StateMachine sm;

        public bool Invincible
        {
            get => invincible;
            set => invincible = value;
        }

        private void Awake()
        {
            health = GetComponent<Health>();
            stamina = GetComponent<Stamina>();
            poise = GetComponent<Poise>();

            controller = GetComponent<Controller>();
            sm = controller.Sm;
        }

        private void OnDestroy()
        {
            
        }

        public void OnDamage(int healthDamage, int poiseDamage)
        {
            if (poise.Empty) sm.ChangeState(controller.STAGGER);
        }

        public IEnumerator OnDamageCo(int healthDamage, int poiseDamage)
        {
            yield return null;
        }
    }
}