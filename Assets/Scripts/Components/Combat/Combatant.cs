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

        CombatDatabase combat;

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

        public void OnDamage(float multiplier, Ability ability, Combatant instigator)
        {
            if (controller.State == ControllerState.Death) return;

            var healthDamage = 0;
            var staminaDamage = 0;

            var effect = ability.Damage;

            if (effect != null)
            {
                healthDamage = Mathf.RoundToInt(multiplier * combat.GetDamage(effect.HealthDamage, 
                    ability.skill.Element, instigator, this));
                staminaDamage = Mathf.RoundToInt(multiplier * combat.GetDamage(effect.StaminaDamage, 
                    ability.skill.Element, instigator, this));
            }

            TakeDamage(healthDamage, staminaDamage);
        }

        public IEnumerator OnDamageCo(float multiplier, Ability ability, Combatant instigator)
        {
            if (controller.State == ControllerState.Death) yield break;
            if (ability.Damage == null) yield break;

            yield return null;
        }

        public void InitCombatant()
        {
            combat = GameManager.instance.combat;
            character = characterData.Character;

            party = GetComponentInParent<PartyManager>();
            controller = GetComponent<Controller>();
            csm = controller.Sm;

            abilities = GetComponent<AbilityManager>();

            health = party.PartyHealth;
            stamina = party.PartyStamina;
        }

        void TakeDamage(int healthDamage, int staminaDamage)
        {
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