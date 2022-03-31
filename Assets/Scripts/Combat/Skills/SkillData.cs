using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Combat/Skill")]
    public class SkillData : ScriptableObject
    {
        [Header("Skill Info")]
        public string skillName;
        public ElementID element;
        public CommandType type;

        [Header("Skill Resources")]
        public int staminaCost = 1;
        [Range(1, 50)]
        public int uses = 1;

        public DamageEffect damageEffect;
        public BuffEffect buffEffect;

        public virtual Skill GetSkill()
        {
            return new Skill(this);
        }
    }
}