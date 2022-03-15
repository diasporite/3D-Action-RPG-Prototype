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

        public void OnDamage(int baseDamage, BattleChar instigator)
        {
            if (controller.State == ControllerState.Death) return;

            var healthDamage = GameManager.instance.Combat.GetDamage(baseDamage, instigator.Attack, Character.Defence);
            var staminaDamage = GameManager.instance.Combat.GetDamage(Mathf.RoundToInt(2.4f * baseDamage), instigator.Attack, Character.Defence);

            TakeDamage(healthDamage, staminaDamage);

            //party.onPoiseChanged.Invoke(poiseDamage);
            //party.onHealthChanged.Invoke(healthDamage);
        }

        public IEnumerator OnDamageCo(int baseDamage, BattleChar instigator)
        {
            if (controller.State == ControllerState.Death) yield break;

            yield return null;
        }

        public void InitCombatant()
        {
            party = GetComponentInParent<PartyManager>();
            controller = GetComponent<Controller>();
            csm = controller.Sm;

            abilities = GetComponent<AbilityManager>();

            stamina = GetComponentInParent<Stamina>();
            //poise = GetComponent<Poise>();

            character = characterData.Character;
            health = party.PartyHealth;
        }

        void TakeDamage(int healthDamage, int staminaDamage)
        {
            print(gameObject.name);
            health.ChangeResource(-healthDamage);
            if (controller.State != ControllerState.Stagger)
                stamina.ChangeResource(-staminaDamage);

            party.InvokeDamage(-healthDamage);

            if (health.Empty) party.ActionQueue.StopActionDeath();
            else if (stamina.Empty && controller.State != ControllerState.Stagger)
                party.ActionQueue.StopActionStagger();
            else if (party.AIController != null) party.AIController.FillActionCooldown(1f);
        }

        public void ApplyFallDamage(float percent)
        {
            var damage = Mathf.RoundToInt(0.01f * Mathf.Abs(percent) * 
                (float)party.PartyHealth.ResourcePoints.CurrentStatValue);

            TakeDamage(damage, 0);

            party.ActionQueue.StopActionStagger();
        }
    }
}