using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class AttackAbility : Ability
    {
        [SerializeField] int healthDamage;
        [SerializeField] int poiseDamage;

        #region Constructors
        public AttackAbility(Action action, Skill skill) : base(action, skill)
        {

        }
        #endregion

        public override void InitAbility(string trigger)
        {
            base.InitAbility(trigger);

            CalculateDamage();
        }

        public override BattleCommand GetCommand(Controller controller, Vector3 dir)
        {
            return new AttackCommand(this, controller, dir, trigger);
        }

        public void CalculateDamage()
        {
            healthDamage = Mathf.RoundToInt(skill.HealthDps * action.animation.length / action.numberOfHits);
            if (healthDamage < 1) healthDamage = 1;

            poiseDamage = Mathf.RoundToInt(skill.PoiseDps * action.animation.length / action.numberOfHits);
            if (poiseDamage < 1) poiseDamage = 1;
        }
    }
}