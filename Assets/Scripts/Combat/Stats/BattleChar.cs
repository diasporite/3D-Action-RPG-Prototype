using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum StatType
    {
        None = 0,

        Level = 1,
        Exp = 2,
        ExpAtLv = 3,

        Health = 5,
        Stamina = 6,
        Poise = 7,
        
        Vitality = 8,
        Endurance = 9,

        Attack = 10,
        Defence = 11,

        Weight = 12,
    }

    public enum WeightClass
    {
        Lightweight = 0,
        Middleweight = 1,
        Heavyweight = 2,
    }

    [System.Serializable]
    public class BattleChar
    {
        [SerializeField] bool dead = false;

        [Header("Info")]
        [SerializeField] string charName;
        [SerializeField] Sprite portrait;

        //[SerializeField] Typing typing = new Typing();
        [SerializeField] ElementData element1;
        [SerializeField] ElementData element2;

        [Header("Progression")]
        [SerializeField] Progression progression;
        [SerializeField] Rewards rewards;

        [Header("Resource Stats")]
        [SerializeField] Stat vitality = new Stat(100, 999);
        [SerializeField] Stat endurance = new Stat(250, 999);

        [Header("Attack Stats")]
        [SerializeField] Stat attack = new Stat(100, 255);
        [SerializeField] Stat defence = new Stat(100, 255);

        [SerializeField] Stat weight = new Stat(100, 200);

        [Header("Stat Tables")]
        [SerializeField] int[] vitAtLv;
        [SerializeField] int[] endAtLv;

        [SerializeField] int[] atkAtLv;
        [SerializeField] int[] defAtLv;

        #region Getters/Setters
        public string CharName => charName;
        public bool Dead => dead;

        public Sprite Portrait => portrait;

        public Progression Progression => progression;
        public Rewards Rewards => rewards;

        //public Typing Typing => typing;

        public ElementData Element1 => element1;
        public ElementData Element2 => element2;

        public Stat Vitality => vitality;
        public Stat Endurance => endurance;

        public Stat Attack => attack;
        public Stat Defence => defence;

        public Stat Weight => weight;
        public WeightClass Weightclass
        {
            get
            {
                if (weight.CurrentStatValue < 70) return WeightClass.Lightweight;
                if (weight.CurrentStatValue > 130) return WeightClass.Heavyweight;
                return WeightClass.Middleweight;
            }
        }

        public int Lv => progression.Lv;

        public int Vit => vitality.CurrentStatValue;
        public int End => endurance.CurrentStatValue;

        public int Atk => attack.CurrentStatValue;
        public int Def => defence.CurrentStatValue;

        public int Wt => weight.CurrentStatValue;
        #endregion

        #region Constructors and Init
        public BattleChar()
        {

        }

        public BattleChar(CharData data)
        {
            charName = data.charName;

            InitChar(data);
        }

        void InitChar(CharData data)
        {
            portrait = data.portrait;

            element1 = GameManager.instance.Combat.GetElement(data.element1);
            element2 = GameManager.instance.combat.GetElement(data.element2);

            progression = new Progression(data.baseExp);
            rewards = new Rewards(data.baseExpReward, data.baseExpReward);

            vitAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Vitality, data.baseVit);
            endAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Endurance, data.baseEnd);

            atkAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Attack, data.baseAtk);
            defAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Defence, data.baseDef);

            var lv = 1;

            var hp = vitAtLv[lv - 1];
            var sp = endAtLv[lv - 1];

            var atk = atkAtLv[lv - 1];
            var def = defAtLv[lv - 1];

            vitality = new Stat(hp, 999);
            endurance = new Stat(sp, 999);

            attack = new Stat(atk, 255);
            defence = new Stat(def, 255);
        }
        #endregion

        //public void ChangeHealth(int amount)
        //{
        //    health.ChangeCurrentPoints(amount);
        //    if (health.PointValue <= 0) dead = true;
        //}

        //public void ChangeStamina(int amount)
        //{
        //    stamina.ChangeCurrentPoints(amount);
        //}

        //public void ChangePoise(int amount)
        //{
        //    poise.ChangeCurrentPoints(amount);
        //}

        //public void UpdateTerrain()
        //{
        //    terrain.ClearModifier();

        //    // Placeholder
        //    var i = Random.Range(-50, 50);
        //    terrain.AddModifier(new StatModifier(StatModifierType.Additive, i));
        //}
    }
}