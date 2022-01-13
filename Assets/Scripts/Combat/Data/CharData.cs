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

        public Sprite portrait;

        public ElementID element1;
        public ElementID element2;

        [Header("Base Stats")]
        [Range(1, 99)]
        public int baseExp = 50;
        [Range(1, 99)]
        public int baseExpReward = 50;

        [Range(1, 99)]
        public int baseHp = 50;
        [Range(1, 99)]
        public int baseSp = 50;
        [Range(1, 99)]
        public int basePp = 50;

        [Range(1, 99)]
        public int baseAtk = 50;
        [Range(1, 99)]
        public int baseDef = 50;

        [Range(25, 175)]
        public int weight = 100;

        [Header("Actions")]
        public ActionData topLeft;
        public ActionData topRight;
        public ActionData bottomLeft;
        public ActionData bottomRight;

        [Header("Skillset")]
        public SkillData[] skills;

        public BattleChar Character => new BattleChar(this);

        public BattleChar GetChar()
        {
            return new BattleChar(this);
        }
    }
}