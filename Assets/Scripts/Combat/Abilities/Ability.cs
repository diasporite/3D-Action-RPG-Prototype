using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    // Animations can be paired with different skill effects to create different abilities
    [System.Serializable]
    public class Ability
    {
        [SerializeField] protected string trigger;

        public CombatAction action;
        public Skill skill;

        [SerializeField] protected int spCost;

        public string Trigger => trigger;

        public int SpCost => spCost;

        #region Constructors
        public Ability()
        {

        }

        public Ability(string trigger)
        {
            this.trigger = trigger;
        }

        public Ability(CombatAction action, Skill skill)
        {
            this.action = action;
            this.skill = skill;
        }

        public Ability(string trigger, CombatAction action, Skill skill)
        {
            this.trigger = trigger;
            this.action = action;
            this.skill = skill;
        }
        #endregion

        public virtual BattleCommand GetCommand(Controller controller, Vector3 dir)
        {
            return new AttackCommand(this, controller, dir, trigger);
        }

        public virtual void InitAbility(string trigger)
        {
            this.trigger = trigger;

            spCost = Mathf.Abs(action.spCost) + Mathf.Abs(skill.StaminaCost);
        }

        public void UseResource(int amount)
        {
            skill.Uses.ChangeCurrentPoints(-Mathf.Abs(amount));
        }
    }
}