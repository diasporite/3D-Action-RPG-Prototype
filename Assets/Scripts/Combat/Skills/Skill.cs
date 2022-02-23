using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum CommandType
    {
        Attack = 0,
        SelfCast = 1,
        AttackCast = 2,
    }

    [System.Serializable]
    public class Skill
    {
        [Header("Skill Info")]
        [SerializeField] protected string skillName;
        [SerializeField] ElementID element;
        [SerializeField] CommandType type;

        [SerializeField] protected int staminaCost = 5;

        [SerializeField] protected PointStat uses = new PointStat(10, 10);

        [SerializeField] protected DamageEffect damage;
        [SerializeField] protected BuffEffect buff;

        [SerializeField] protected int healthDps = 50;
        [SerializeField] protected int poiseDps = 30;

        public string SkillName => skillName;

        public int StaminaCost => staminaCost;

        public PointStat Uses => uses;

        public virtual Ability GetAbility(string trigger, CombatAction action) => 
            new Ability(trigger, action, this);

        public Skill(SkillData data)
        {
            skillName = data.skillName;
            element = data.element;
            type = data.type;

            staminaCost = data.staminaCost;

            uses = new PointStat(data.uses, data.uses);

            damage = data.damageEffect;
            buff = data.buffEffect;
        }

        public virtual BattleCommand GetAction(Controller controller, Vector3 dir)
        {
            return null;
        }

        public void CalculateDamage(float duration, int numOfHits)
        {
            if (damage.used) damage.CalculateDamage(duration, numOfHits);
        }
    }
}