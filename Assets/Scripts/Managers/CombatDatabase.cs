using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "CombatDatabase", menuName = "Combat/Database/Combat")]
    public class CombatDatabase : ScriptableObject
    {
        [Header("Elements")]
        public ElementData[] elements;
        Dictionary<ElementID, ElementData> elementDict = new Dictionary<ElementID, ElementData>();

        [Header("Damage Multipliers")]
        public float critMultiplier = 1.5f;

        public float weakMultiplier = 1.4f;
        public float resistMultiplier = 0.7f;

        public float stabMultiplier = 1.2f;

        [Header("Running Speeds")]
        public float lightweightWalk = 5f;
        public float lightweightRun = 7f;
        public float lightweightLocked = 3f;

        public float middleweightWalk = 4f;
        public float middleweightRun = 6f;
        public float middleweightLocked = 2.5f;

        public float heavyweightWalk = 3f;
        public float heavyweightRun = 5f;
        public float heavyweightLocked = 2f;

        [Header("Resource Regen")]
        public float lightweightHealthRegen = 2f;
        public float middleweightHealthRegen = 5f;
        public float heavyweightHealthRegen = 8f;

        public float staminaRegen = 45f;
        public float staminaRecoverRegen = 75f;
        public float staminaRunRegen = -25f;

        public float poiseRegen = 18f;

        [Header("Special Action Costs")]
        public int jumpSpCost = 6;
        public int rollSpCost = 9;
        public int guardSpCost = 12;

        public ElementData GetElement(ElementID id)
        {
            return elementDict[id];
        }

        public void InitDatabase()
        {
            elementDict.Clear();

            foreach (var data in elements)
                if (!elementDict.ContainsKey(data.id))
                    elementDict.Add(data.id, data);
        }

        #region Stats
        // Placeholder
        public int GetStatValue(int baseStat)
        {
            return Mathf.RoundToInt(2.55f * baseStat);
        }

        public int[] GetStatAtLv(StatType stat, CharData data)
        {
            int[] statAtLv = new int[10];

            switch (stat)
            {
                case StatType.Vitality:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(1.15f * (i + 1) * data.baseVit + 140, 999);
                    break;
                case StatType.Endurance:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.4f * (i + 1) * data.baseEnd + 105, 999);
                    break;
                case StatType.Poise:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.11f * (i + 1) * data.basePp + 30, 99);
                    break;
                case StatType.Attack:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.3f * (i + 1) * data.baseAtk + 20, 255);
                    break;
                case StatType.Defence:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.3f * (i + 1) * data.baseDef + 20, 255);
                    break;
                default:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.3f * (i + 1) * 1 + 20, 255);
                    break;
            }

            return statAtLv;
        }

        public int[] GetStatAtLv(StatType stat, int baseStat)
        {
            int[] statAtLv = new int[10];

            switch (stat)
            {
                case StatType.Vitality:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(1.15f * (i + 1) * baseStat + 140, 999);
                    break;
                case StatType.Endurance:
                    for (int i = 0; i < statAtLv.Length; i++)
                        statAtLv[i] = ConvertToStatValue(0.4f * (i + 1) * baseStat + 105, 999);
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

        public int GetDamage(int baseDamage, ElementData element, Combatant instigator, Combatant target)
        {
            if (IsImmune(element, target.Character)) return 1;

            var atk = instigator.Character.Attack.CurrentStatValue;
            var def = target.Character.Defence.CurrentStatValue;

            var instPos = instigator.transform.position;
            var targetPos = target.transform.position;

            float multiplier = 1;

            var damage = baseDamage + atk - def;

            if (BackstabCritical(instPos, targetPos)) multiplier *= critMultiplier;
            if (StabBonus(element, instigator.Character)) multiplier *= stabMultiplier;

            multiplier *= ElementMultiplier(element, target.Character);

            damage = Mathf.RoundToInt(damage * multiplier);

            if (damage < 1) damage = 1;
            if (damage > 999) damage = 999;

            Debug.Log(multiplier + " * (" + baseDamage + " + " + atk + " - " + def + ") = " + damage);
            return damage;
        }
        #endregion

        bool BackstabCritical(Vector3 instPos, Vector3 targetPos)
        {
            var ds = targetPos - instPos;

            return false;
        }

        bool StabBonus(ElementData element, BattleChar insigator)
        {
            return element == insigator.Element1 || element == insigator.Element2;
        }

        bool IsImmune(ElementData element, BattleChar target)
        {
            return target.Element1.IsImmune(element);
        }

        float ElementMultiplier(ElementData element, BattleChar target)
        {
            var modifier1 = 1f;
            var modifier2 = 1f;

            if (target.Element1.IsResist(element)) modifier1 *= resistMultiplier;
            else if (target.Element1.IsWeak(element)) modifier1 *= weakMultiplier;

            if (target.Element2.IsResist(element)) modifier2 *= resistMultiplier;
            else if (target.Element2.IsWeak(element)) modifier2 *= weakMultiplier;

            return modifier1 * modifier2;
        }
    }
}