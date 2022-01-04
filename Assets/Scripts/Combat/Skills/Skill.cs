using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Skill
    {
        [Header("Skill Info")]
        [SerializeField] protected string skillName;
        [SerializeField] protected int id;

        [SerializeField] protected int staminaCost = 5;

        [SerializeField] protected PointStat uses = new PointStat(10, 10);

        [SerializeField] protected int healthDps = 50;
        [SerializeField] protected int poiseDps = 30;

        public string SkillName => skillName;

        public int StaminaCost => staminaCost;

        public PointStat Uses => uses;

        public int HealthDps => healthDps;
        public int PoiseDps => poiseDps;

        public Skill(SkillData data)
        {
            skillName = data.skillName;
            id = data.id;

            staminaCost = data.staminaCost;

            if (data.limitedUse)
                uses = new PointStat(data.uses, data.uses);
        }

        public virtual BattleCommand GetAction(Controller controller, Vector3 dir)
        {
            return null;
        }
    }
}