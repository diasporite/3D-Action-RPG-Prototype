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
        Magic = 6,
        Stamina = 7,
        Stagger = 8,

        Attack = 10,
        Defence = 11,
        Speed = 12,
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
        [SerializeField] int id;

        [SerializeField] Typing typing = new Typing();
        [SerializeField] WeightClass weightClass = WeightClass.Middleweight;

        [Header("Progression")]
        [SerializeField] Progression progression;
        [SerializeField] Rewards rewards;

        [Header("Resource Stats")]
        [SerializeField] PointStat health = new PointStat(100, 999);
        [SerializeField] PointStat stamina = new PointStat(25, 99);
        [SerializeField] PointStat poise = new PointStat(150, 999);

        [Header("Attack Stats")]
        [SerializeField] Stat attack = new Stat(100, 255);
        [SerializeField] Stat defence = new Stat(100, 255);

        //[SerializeField] Stat speed = new Stat(100, 180);

        //[SerializeField] Stat terrain = new Stat(100, 255);

        #region Getters/Setters
        public string CharName => charName;
        public int Id => id;
        public bool Dead => dead;

        public Progression Progression => progression;
        public Rewards Rewards => rewards;

        public Typing Typing => typing;
        public WeightClass WeightClass => weightClass;

        public PointStat Health => health;
        public PointStat Stamina => stamina;
        public PointStat Poise => poise;

        public Stat Attack => attack;
        public Stat Defence => defence;
        //public Stat _speed => speed;

        public int Lv => progression._lv;

        public int CurrentHp => health.PointValue;
        public int Hp => health.CurrentStatValue;
        public float HpFraction => health.PointFraction;

        public int CurrentStamina => stamina.PointValue;
        public int Sp => stamina.CurrentStatValue;
        public float SpFraction => stamina.PointFraction;

        public int CurrentPoise => poise.PointValue;
        public int Pp => poise.CurrentStatValue;
        public float PpFraction => poise.PointFraction;

        public int Atk => attack.CurrentStatValue;
        public int Def => defence.CurrentStatValue;
        //public int Spd => speed.CurrentStatValue;
        #endregion

        #region Constructors and Init
        public BattleChar()
        {

        }

        public BattleChar(CharData data)
        {
            charName = data.charName;
            id = data.id;

            InitChar(data.baseHp, data.baseAtk, data.baseDef);
        }

        public BattleChar(int id, bool random)
        {
            this.id = id;
            charName = id.ToString();

            if (random)
                InitChar(Random.Range(50, 200), Random.Range(10, 50), 
                Random.Range(55, 165));
        }

        public BattleChar(int id, int hp, int atk, int def)
        {
            this.id = id;
            charName = id.ToString();
            InitChar(hp, atk, def);
        }

        void InitChar(int hp, int atk, int def)
        {
            //progression = new Progression(100, GameManager.instance._combat._levelCap);
            //rewards = new Rewards(100, 0, GameManager.instance._combat._levelCap);

            health = new PointStat(hp, hp, 999);
            stamina = new PointStat(hp, hp, 999);
            poise = new PointStat(hp, hp, 999);

            attack = new Stat(atk, 255);
            defence = new Stat(def, 255);

            //speed = new Stat(spd, 180);
            //initiative = new Stat(GetInitiative(), 19);
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