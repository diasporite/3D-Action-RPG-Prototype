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

    [System.Serializable]
    public class BattleChar
    {
        [SerializeField] string charName;
        [SerializeField] int id;

        [SerializeField] bool dead = false;

        [SerializeField] Progression progression;
        [SerializeField] Rewards rewards;

        [SerializeField] Typing typing = new Typing();

        [SerializeField] PointStat health = new PointStat(100, 999);
        [SerializeField] PointStat magic = new PointStat(100, 999);
        [SerializeField] PointStat stamina = new PointStat(200, 999);

        [SerializeField] Stat attack = new Stat(100, 255);
        [SerializeField] Stat defence = new Stat(100, 255);

        [SerializeField] Stat speed = new Stat(100, 180);
        [SerializeField] Stat initiative = new Stat(9, 18);

        //[SerializeField] Stat terrain = new Stat(100, 255);

        #region Getters/Setters
        public string _charName => charName;
        public int _id => id;
        public bool _dead => dead;

        public Progression _progression => progression;
        public Rewards _rewards => rewards;

        public Typing _typing => typing;

        public PointStat _health => health;
        public PointStat _magic => magic;
        public PointStat _stamina => stamina;

        public Stat _attack => attack;
        public Stat _defence => defence;
        public Stat _speed => speed;

        public Stat _initiative => initiative;
        //public Stat _environment => terrain;

        public int Lv => progression._lv;

        public int CurrentHp => health.PointValue;
        public int Hp => health.CurrentStatValue;
        public float HpFraction => health.PointFraction;

        //public int CurrentMp => magic.PointValue;
        //public int Mp => magic.CurrentStatValue;
        //public float MpFraction => magic.PointFraction;

        public int CurrentStamina => stamina.PointValue;
        public int Sp => stamina.CurrentStatValue;
        public float SpFraction => stamina.PointFraction;

        public int Atk => attack.CurrentStatValue;
        public int Def => defence.CurrentStatValue;
        public int Spd => speed.CurrentStatValue;

        //public int _terrainIndex => terrain._currentStatValue;
        //public float _terrainFactor => 0.01f * terrain._currentStatValue;
        #endregion

        #region Constructors and Init
        public BattleChar()
        {

        }

        public BattleChar(CharData data)
        {
            charName = data.charName;
            id = data.id;

            InitChar(data.baseHp, data.baseAtk, data.baseDef, data.baseSpd);
        }

        public BattleChar(int id, bool random)
        {
            this.id = id;
            charName = id.ToString();

            if (random)
                InitChar(Random.Range(50, 200), Random.Range(10, 50), 
                Random.Range(55, 165), Random.Range(10, 170));
        }

        public BattleChar(int id, int hp, int atk, int def, int spd)
        {
            this.id = id;
            charName = id.ToString();
            InitChar(hp, atk, def, spd);
        }

        void InitChar(int hp, int atk, int def, int spd)
        {
            //progression = new Progression(100, GameManager.instance._combat._levelCap);
            //rewards = new Rewards(100, 0, GameManager.instance._combat._levelCap);

            health = new PointStat(hp, hp, 999);
            magic = new PointStat(hp, hp, 999);
            stamina = new PointStat(hp, hp, 999);

            attack = new Stat(atk, 255);
            defence = new Stat(def, 255);

            speed = new Stat(spd, 180);
            initiative = new Stat(GetInitiative(), 19);
        }
        #endregion

        public void ChangeHealth(int amount)
        {
            health.ChangeCurrentPoints(amount);
            if (health.PointValue <= 0) dead = true;
        }

        public void ChangeMagic(int amount)
        {
            magic.ChangeCurrentPoints(amount);
        }

        public void ChangeStamina(int amount)
        {
            stamina.ChangeCurrentPoints(amount);
        }

        public int GetInitiative()
        {
            return Mathf.FloorToInt(0.1f * (180 - speed.CurrentStatValue) + 1);
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