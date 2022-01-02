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
        [SerializeField] protected float duration = 0.1f;

        [SerializeField] protected int staminaCost;
        [SerializeField] protected int skillSpeed;

        [SerializeField] protected PointStat uses;

        public string _skillName => skillName;
        public float _duration => duration;

        public int _staminaCost => staminaCost;
        public int _skillSpeed => skillSpeed;

        public PointStat _uses => uses;

        public Skill(SkillData data)
        {
            skillName = data.skillName;
            id = data.id;
            duration = data.duration;

            staminaCost = data.staminaCost;

            if (data.limitedUse)
                uses = new PointStat(data.uses, data.uses);

            skillSpeed = data.skillSpeed;
        }

        public virtual BattleAction GetAction(Controller controller, Vector3 dir)
        {
            return null;
        }
    }
}