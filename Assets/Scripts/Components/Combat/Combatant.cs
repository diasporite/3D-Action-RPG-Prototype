using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Combatant : MonoBehaviour, IDamageable
    {
        bool invincible = false;

        [SerializeField] CharData characterData;

        [SerializeField] BattleChar character;
        [SerializeField] Skillset skillset = new Skillset();

        PartyManager party;
        Controller controller;
        StateMachine csm;

        AbilityManager abilities;

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

        public AbilityManager Abilities => abilities;

        public Stamina Stamina => stamina;
        public Poise Poise => poise;

        private void Awake()
        {
            party = GetComponentInParent<PartyManager>();
            controller = GetComponent<Controller>();
            csm = controller.Sm;

            abilities = GetComponent<AbilityManager>();

            stamina = GetComponent<Stamina>();
            poise = GetComponent<Poise>();
        }

        private void Start()
        {
            health = party.PartyHealth;

            //party.onHealthChanged +=
            //party.onPoiseChanged +=
        }

        private void OnDestroy()
        {
            //party.onHealthChanged -=
            //party.onPoiseChanged -=
        }

        public void OnDamage(int baseDamage, BattleChar instigator)
        {
            var healthDamage = GameManager.instance.Combat.GetDamage(baseDamage, instigator.Attack, Character.Defence);
            var poiseDamage = GameManager.instance.Combat.GetDamage(Mathf.RoundToInt(0.4f * baseDamage), instigator.Attack, Character.Defence);
            
            TakePoiseDamage(poiseDamage);
            TakeHealthDamage(healthDamage);

            //party.onPoiseChanged.Invoke(poiseDamage);
            //party.onHealthChanged.Invoke(healthDamage);
        }

        public IEnumerator OnDamageCo(int baseDamage, BattleChar instigator)
        {
            yield return null;
        }

        public void InitCombatant()
        {
            character = characterData.Character;
        }

        void TakeHealthDamage(int damage)
        {
            //character.ChangeHealth(damage);
            health.ChangeResource(damage);
            if (health.Empty) csm.ChangeState(controller.DEATH);
        }

        void TakePoiseDamage(int damage)
        {
            character.ChangePoise(damage);
            poise.ChangeResource(damage);
            if (poise.Empty) csm.ChangeState(controller.STAGGER);
        }

        public void ApplyFallDamage(float percent)
        {
            var damage = Mathf.RoundToInt(0.01f * Mathf.Abs(percent) * 
                (float)character.Health.CurrentStatValue);

            TakePoiseDamage(-999);  // Guaranteed stagger
            TakeHealthDamage(-damage);
        }
    }
}