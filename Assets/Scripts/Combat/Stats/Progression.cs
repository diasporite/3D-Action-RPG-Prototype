using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Progression
    {
        [SerializeField] int lv = 1;
        [SerializeField] int currentLvCap = 1;  // Can be increased
        [SerializeField] int exp = 0;
        [SerializeField] int[] expAtLv; // 0 at LV 1 (element 0)
        [SerializeField] int[] expRewardAtLv;

        int lvCap = 10;

        public int Lv => lv;

        public int Exp
        {
            get => exp;
            set
            {
                exp = value;
                lv = GetLv(exp);
            }
        }

        public int ExpToNext => expAtLv[lv] - exp;

        public int ExpFromLast => exp - expAtLv[lv - 1];

        public int GetExpAtLv(int level)
        {
            return expAtLv[level];
        }

        public int GetExpRewardAtLv(int level)
        {
            return expRewardAtLv[level];
        }

        public int GetLv(int exp)
        {
            var lv = 1;

            for (int i = 0; i < expAtLv.Length; i++)
            {
                if (exp < expAtLv[i])
                {
                    lv = i + 1;
                    break;
                }
            }

            if (lv > currentLvCap) lv = currentLvCap;
            return lv;
        }

        public Progression(int baseExp)
        {
            expAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.ExpAtLv, baseExp);
        }

        public Progression(int initialExp, int[] expAtLv, int currentLvCap)
        {
            exp = initialExp;
            this.expAtLv = expAtLv;

            lvCap = expAtLv.Length;
            SetCurrentLvCap(currentLvCap);
        }

        public void SetLv(int lv)
        {

        }

        public void SetExp(int exp)
        {
            this.exp = exp;
            var newLv = GetLv(exp);

            if (newLv > lv)
            {
                lv = newLv;
            }
        }

        public void SetCurrentLvCap(int cap)
        {
            cap = Mathf.Abs(cap);
            if (cap > lvCap) cap = lvCap;

            currentLvCap = cap;
        }

        public void GainExperience(int amount)
        {
            amount = Mathf.Abs(amount);
            SetExp(exp + amount);
        }
    }
}