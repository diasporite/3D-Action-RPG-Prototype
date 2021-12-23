using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class SkillData : ScriptableObject
    {
        [Header("Skill Info")]
        public string skillName;
        public int id;
        public float duration;

        [Header("Skill Resources")]
        public int staminaCost = 1;
        public bool limitedUse = false;
        public int uses = 1;

        [Header("Skill Initiative")]
        public int initiativeWindow = 1;
        public int skillSpeed = 0;

        public virtual Skill GetSkill()
        {
            return new Skill(this);
        }
    }
}