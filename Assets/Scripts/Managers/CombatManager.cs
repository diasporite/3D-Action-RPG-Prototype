using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CombatManager : MonoBehaviour
    {
        [Header("Damage Multipliers")]
        public float critMultiplier = 1.5f;

        public float weakMultiplier = 1.4f;
        public float resistMultiplier = 0.7f;

        public float stabMultiplier = 1.2f;

        [Header("Running Speeds")]
        public float lightweightWalk = 7f;
        public float lightweightRun = 10.5f;

        public float middleweightWalk = 6f;
        public float middleweightRun = 9f;

        public float heavyweightWalk = 4.5f;
        public float heavyweightRun = 6.75f;

        [Header("Resource Regen")]
        public float staminaRegen = 12f;
        public float staminaRun = -2f;

        public float poiseRegen = 18f;

        [Header("Basic Action Costs")]
        public int jumpSpCost = 6;
        public int rollSpCost = 9;
        public int guardSpCost = 12;

        #region Stats
        // Placeholder
        public int GetStatValue(int baseStat)
        {
            return Mathf.RoundToInt(2.55f * baseStat);
        }

        public int[] GetStatAtLv(StatType stat, int baseStat)
        {
            int[] statAtLv = new int[10];

            switch (stat)
            {
                case StatType.Health:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(1.3f * (i + 1) * baseStat + 80, 999);
                    break;
                case StatType.Stamina:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.07f * (i + 1) * baseStat + 20, 99);
                    break;
                case StatType.Poise:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.11f * (i + 1) * baseStat + 30, 99);
                    break;
                default:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.3f * (i + 1) * baseStat + 20, 255);
                    break;
            }

            return statAtLv;
        }

        public int ConvertToStatValue(float value, int cap)
        {
            var intValue = Mathf.RoundToInt(value);
            if (value > cap) value = cap;
            if (value < 1) value = 1;
            return intValue;
        }
        #endregion

        #region Damage
        public int GetDamage(int attack, int defence)
        {
            var damage = attack - defence;
            if (damage < 1) damage = 1;
            return damage;
        }

        public int GetDamage(int baseDamage, Stat offensive, Stat defensive)
        {
            var damage = baseDamage + offensive.CurrentStatValue - defensive.CurrentStatValue;
            if (damage < 1) damage = 1;
            return damage;
        }
        #endregion
    }
}