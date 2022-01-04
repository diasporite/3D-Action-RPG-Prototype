using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    // Animations can be paired with different skill effects to create different abilities
    [System.Serializable]
    public class Ability
    {
        [SerializeField] string trigger;

        public Action action;
        public Skill effect;

        [SerializeField] int healthDamage;
        [SerializeField] int poiseDamage;

        public Ability()
        {

        }

        public Ability(string trigger)
        {
            this.trigger = trigger;
        }

        public Ability(Action action, Skill skill)
        {
            this.action = action;
            effect = skill;
        }

        public Ability(string trigger, Action action, Skill skill)
        {
            this.trigger = trigger;
            this.action = action;
            effect = skill;
        }

        public void InitAbility(string trigger)
        {
            this.trigger = trigger;

            CalculateDamage();
        }

        public BattleCommand GetCommand(Controller controller)
        {
            //return new BattleCommand(controller);
            return new AttackCommand(controller, controller.transform.forward, trigger);
        }

        public virtual void CalculateDamage()
        {
            healthDamage = Mathf.RoundToInt(effect.HealthDps * action.animation.length / action.numberOfHits);
            if (healthDamage < 1) healthDamage = 1;

            poiseDamage = Mathf.RoundToInt(effect.PoiseDps * action.animation.length / action.numberOfHits);
            if (poiseDamage < 1) poiseDamage = 1;
        }
    }
}