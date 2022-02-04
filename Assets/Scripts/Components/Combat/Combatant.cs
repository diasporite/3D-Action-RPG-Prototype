﻿using System.Collections;
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
            var healthDamage = GameManager.instance.Combat.GetDamage(baseDamage, instigator.Attack, Character.Defence);
            var poiseDamage = GameManager.instance.Combat.GetDamage(Mathf.RoundToInt(0.4f * baseDamage), instigator.Attack, Character.Defence);

            TakeDamage(healthDamage, poiseDamage);

            //party.onPoiseChanged.Invoke(poiseDamage);
            //party.onHealthChanged.Invoke(healthDamage);
        }

        public IEnumerator OnDamageCo(int baseDamage, BattleChar instigator)
        {
            yield return null;
        }

        public void InitCombatant()
        {
            party = GetComponentInParent<PartyManager>();
            controller = GetComponent<Controller>();
            csm = controller.Sm;

            abilities = GetComponent<AbilityManager>();

            stamina = GetComponent<Stamina>();
            poise = GetComponent<Poise>();

            character = characterData.Character;
            health = party.PartyHealth;
        }

        void TakeHealthDamage(int damage)
        {
            //character.ChangeHealth(damage);
            health.ChangeResource(-damage);
            if (health.Empty) party.ActionQueue.StopActionDeath();
        }

        void TakePoiseDamage(int damage)
        {
            character.ChangePoise(-damage);
            poise.ChangeResource(-damage);
            if (poise.Empty) party.ActionQueue.StopActionStagger();
        }

        void TakeDamage(int healthDamage, int poiseDamage)
        {
            health.ChangeResource(-healthDamage);

            character.ChangePoise(-poiseDamage);
            poise.ChangeResource(-poiseDamage);

            party.InvokeDamage(-healthDamage);

            if (health.Empty) party.ActionQueue.StopActionDeath();
            else if (poise.Empty) party.ActionQueue.StopActionStagger();
        }

        public void ApplyFallDamage(float percent)
        {
            var damage = Mathf.RoundToInt(0.01f * Mathf.Abs(percent) * 
                (float)character.Health.CurrentStatValue);

            //TakePoiseDamage(-999);  // Guaranteed stagger
            //TakeHealthDamage(-damage);

            TakeDamage(damage, 999);
        }
    }
}