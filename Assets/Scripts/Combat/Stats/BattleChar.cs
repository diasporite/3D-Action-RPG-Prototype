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
        Stagger = 8,

        Attack = 10,
        Defence = 11,
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
        [SerializeField] PointStat health = new PointStat(100, 999);
        [SerializeField] PointStat stamina = new PointStat(250, 999);
        [SerializeField] PointStat poise = new PointStat(50, 99);

        [SerializeField] Stat vitality = new Stat(100, 999);
        [SerializeField] Stat endurance = new Stat(250, 999);

        [Header("Attack Stats")]
        [SerializeField] Stat attack = new Stat(100, 255);
        [SerializeField] Stat defence = new Stat(100, 255);

        [SerializeField] Stat weight = new Stat(100, 200);

        [Header("Stat Tables")]
        [SerializeField] int[] hpAtLv;
        [SerializeField] int[] spAtLv;
        [SerializeField] int[] ppAtLv;

        [SerializeField] int[] atkAtLv;
        [SerializeField] int[] defAtLv;

        //[SerializeField] Stat terrain = new Stat(100, 255);

        #region Getters/Setters
        public string CharName => charName;
        public bool Dead => dead;

        public Progression Progression => progression;
        public Rewards Rewards => rewards;

        //public Typing Typing => typing;

        public ElementData Element1 => element1;
        public ElementData Element2 => element2;

        public PointStat Health => health;
        public PointStat Stamina => stamina;
        public PointStat Poise => poise;

        // Stores float values for hp, sp, pp
        public float HealthResource { get; set; }
        public float StaminaResource { get; set; }
        public float PoiseResource { get; set; }

        public Stat Vitality => vitality;
        public Stat Endurance => endurance;

        public Stat Attack => attack;
        public Stat Defence => defence;
        //public Stat _speed => speed;

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

        public int CurrentHp => health.PointValue;
        public int Hp => health.CurrentStatValue;
        public float HpFraction => health.PointFraction;

        public int CurrentStamina => stamina.PointValue;
        public int Sp => stamina.CurrentStatValue;
        public float SpFraction => stamina.PointFraction;

        public int CurrentPoise => poise.PointValue;
        public int Pp => poise.CurrentStatValue;
        public float PpFraction => poise.PointFraction;

        public int Vit => vitality.CurrentStatValue;
        public int End => endurance.CurrentStatValue;

        public int Atk => attack.CurrentStatValue;
        public int Def => defence.CurrentStatValue;
        //public int Spd => speed.CurrentStatValue;
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

        public BattleChar(bool random)
        {
            if (random)
                InitChar(Random.Range(50, 200), Random.Range(10, 50), 
                Random.Range(55, 165));
        }

        public BattleChar(int hp, int atk, int def)
        {
            InitChar(hp, atk, def);
        }

        void InitChar(int baseHp, int baseAtk, int baseDef)
        {
            progression = new Progression(100);
            rewards = new Rewards(100, 0);

            hpAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Health, baseHp);
            spAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Stamina, baseHp);
            ppAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Poise, baseHp);

            atkAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Attack, baseAtk);
            defAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Defence, baseDef);

            var lv = 1;

            var hp = spAtLv[lv - 1];
            var sp = spAtLv[lv - 1];
            var pp = ppAtLv[lv - 1];

            var atk = spAtLv[lv - 1];
            var def = spAtLv[lv - 1];

            health = new PointStat(hp, hp, 3999);
            stamina = new PointStat(sp, sp, 999);
            poise = new PointStat(pp, pp, 999);

            attack = new Stat(atk, 255);
            defence = new Stat(def, 255);
        }

        void InitChar(CharData data)
        {
            portrait = data.portrait;

            element1 = GameManager.instance.Combat.GetElement(data.element1);
            element2 = GameManager.instance.combat.GetElement(data.element2);

            progression = new Progression(data.baseExp);
            rewards = new Rewards(data.baseExpReward, data.baseExpReward);

            hpAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Health, data.baseHp);
            spAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Stamina, data.baseHp);
            ppAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Poise, data.baseHp);

            atkAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Attack, data.baseAtk);
            defAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Defence, data.baseDef);

            var lv = 1;

            var hp = hpAtLv[lv - 1];
            var sp = spAtLv[lv - 1];
            var pp = ppAtLv[lv - 1];

            var atk = spAtLv[lv - 1];
            var def = spAtLv[lv - 1];

            health = new PointStat(hp, hp, 999);
            stamina = new PointStat(sp, sp, 999);
            poise = new PointStat(pp, pp, 999);

            attack = new Stat(atk, 255);
            defence = new Stat(def, 255);
        }
        #endregion

        public void ChangeHealth(int amount)
        {
            health.ChangeCurrentPoints(amount);
            if (health.PointValue <= 0) dead = true;
        }

        public void ChangeStamina(int amount)
        {
            stamina.ChangeCurrentPoints(amount);
        }

        public void ChangePoise(int amount)
        {
            poise.ChangeCurrentPoints(amount);
        }

        //public void UpdateTerrain()
        //{
        //    terrain.ClearModifier();

        //    // Placeholder
        //    var i = Random.Range(-50, 50);
        //    terrain.AddModifier(new StatModifier(StatModifierType.Additive, i));
        //}

        public bool CanKill(int damage)
        {
            return damage > Hp;
        }
    }
}