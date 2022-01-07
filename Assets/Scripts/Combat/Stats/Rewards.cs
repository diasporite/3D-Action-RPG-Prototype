using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [Serializable]
    public class Rewards
    {
        [SerializeField] int[] expAtLv;
        [SerializeField] int[] moneyAtLv;

        public int GetRewardExp(int lv)
        {
            return expAtLv[lv - 1];
        }

        public int GetRewardMoney(int lv)
        {
            return moneyAtLv[lv - 1];
        }

        public Rewards(int baseExpReward, int baseMoneyReward)
        {
            expAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Exp, baseExpReward);
            expAtLv = GameManager.instance.Combat.GetStatAtLv(StatType.Exp, baseMoneyReward);
        }
    }
}