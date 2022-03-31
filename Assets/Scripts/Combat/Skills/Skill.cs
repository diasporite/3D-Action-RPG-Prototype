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

        [SerializeField] protected int useModifierPercent;

        [SerializeField] protected DamageEffect damage;
        [SerializeField] protected BuffEffect buff;

        [SerializeField] protected int healthDps = 50;
        [SerializeField] protected int poiseDps = 30;

        public string SkillName => skillName;

        public ElementData Element => GameManager.instance.combat.GetElement(element);

        public int StaminaCost => staminaCost;

        public int Uses(int baseUsage) => 
            Mathf.RoundToInt(0.01f * (100 + useModifierPercent) * baseUsage);

        public DamageEffect Damage => damage;
        public BuffEffect Buff => buff;

        public virtual Ability GetAbility(string trigger, CombatAction action) => 
            new Ability(trigger, action, this);

        public Skill(SkillData data)
        {
            skillName = data.skillName;
            element = data.element;
            type = data.type;

            staminaCost = data.staminaCost;

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