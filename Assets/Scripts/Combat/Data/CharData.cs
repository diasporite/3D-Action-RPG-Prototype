using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Combat/Character")]
    public class CharData : ScriptableObject
    {
        [Header("Char Info")]
        public string charName;
        public int id;
        public WeightClass weightClass;

        [Header("Base Stats")]
        public int baseExp = 100;
        public int baseExpReward = 100;

        public int baseHp = 100;
        public int baseSp = 100;
        public int basePp = 100;

        public int baseAtk = 100;
        public int baseDef = 100;

        [Header("Skills")]
        public SkillData[] skills;

        public BattleChar Character => new BattleChar(this);

        public BattleChar GetChar()
        {
            return new BattleChar(this);
        }
    }
}