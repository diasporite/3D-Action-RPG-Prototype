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
        [SerializeField] PointStat uses;

        [SerializeField] DamageEffect damage = null;
        [SerializeField] BuffEffect buff = null;

        public string Trigger => trigger;

        public int SpCost => spCost;

        public PointStat Uses => uses;

        public DamageEffect Damage => damage;
        public BuffEffect Buff => buff;

        public int HealthDamage
        {
            get
            {
                if (damage != null) return damage.HealthDamage;
                return 0;
            }
        }

        public int StaminaDamage
        {
            get
            {
                if (damage != null) return damage.StaminaDamage;
                return 0;
            }
        }

        #region Constructors
        public Ability()
        {

        }

        public Ability(CombatAction action, Skill skill)
        {
            this.action = action;
            this.skill = skill;

            InitAbility();
        }

        public Ability(string trigger, CombatAction action, Skill skill)
        {
            this.trigger = trigger;
            this.action = action;
            this.skill = skill;

            InitAbility();
        }
        #endregion

        public virtual BattleCommand GetCommand(Controller controller, Vector3 dir, int index)
        {
            return new AttackCommand(controller, dir, trigger, index);
        }

        void InitAbility()
        {
            spCost = Mathf.Abs(action.baseSpCost) + Mathf.Abs(skill.StaminaCost);

            var numUses = action.baseUsage;
            uses = new PointStat(numUses, numUses, 40);

            if (skill.Damage.used) damage = new DamageEffect(skill.Damage.HealthDps, 
                skill.Damage.StaminaDps, action);

            if (skill.Buff.used) buff = skill.Buff;
        }

        public void ChangeResource(int amount)
        {
            uses.ChangeCurrentPoints(amount);
        }

        public void UseResource(int amount)
        {
            uses.ChangeCurrentPoints(-Mathf.Abs(amount));
        }
    }
}