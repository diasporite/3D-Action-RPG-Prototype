using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Combatant : MonoBehaviour, IDamageable
    {
        [SerializeField] bool player = false;

        bool invincible = false;

        [SerializeField] BattleChar character = new BattleChar(0, 400, 128, 128);
        [SerializeField] Skillset skillset = new Skillset();

        Controller controller;
        StateMachine csm;
        PartyManager party;

        Health health;
        Stamina stamina;
        Poise poise;

        public bool Invincible
        {
            get => invincible;
            set => invincible = value;
        }

        public BattleChar Character => character;
        public Skillset Skillset => skillset;

        private void Awake()
        {
            controller = GetComponent<Controller>();
            csm = controller.Sm;
            party = GetComponentInParent<PartyManager>();

            health = GetComponent<Health>();
            stamina = GetComponent<Stamina>();
            poise = GetComponent<Poise>();
        }

        private void Start()
        {
            //party.onHealthChanged +=
            //party.onPoiseChanged +=
        }

        private void OnDestroy()
        {
            //party.onHealthChanged -=
            //party.onPoiseChanged -=
        }

        public void OnDamage(int healthDamage, int poiseDamage)
        {
            TakePoiseDamage(poiseDamage);
            TakeHealthDamage(healthDamage);

            //party.onPoiseChanged.Invoke(poiseDamage);
            //party.onHealthChanged.Invoke(healthDamage);
        }

        public IEnumerator OnDamageCo(int healthDamage, int poiseDamage)
        {
            yield return null;
        }

        void TakeHealthDamage(int damage)
        {
            character.ChangeHealth(damage);
            health.ChangeResource(damage);
            //if (health.Empty) csm.ChangeState();
        }

        void TakePoiseDamage(int damage)
        {
            character.ChangePoise(damage);
            health.ChangeResource(damage);
            if (poise.Empty) csm.ChangeState(controller.STAGGER);
        }
    }
}