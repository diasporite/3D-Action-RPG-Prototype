﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [Serializable]
    public class Stat
    {
        [SerializeField] protected int statValue;
        [SerializeField] protected int currentStatValue;

        protected readonly List<StatModifier> statModifiers = new List<StatModifier>();

        protected int valueCap = 255;

        // Look up 'dirty flag'
        protected bool valueChanged = false;

        public int StatValue
        {
            get => statValue;

            set
            {
                statValue = value;

                if (statValue < 1) statValue = 1;
                if (statValue > valueCap) statValue = valueCap;
            }
        }

        public int CurrentStatValue => currentStatValue;

        #region Constructors
        public Stat(int statValue)
        {
            InitValues(statValue);
        }

        public Stat(int statValue, int statCap)
        {
            valueCap = statCap;

            InitValues(statValue);
        }

        protected virtual void InitValues(int value)
        {
            //valueCap = GameManager.instance._statCap;

            StatValue = value;
            currentStatValue = value;
        }
        #endregion

        int CurrentValue()
        {
            int value = statValue;

            foreach(var mod in statModifiers)
            {
                switch (mod._modifierType)
                {
                    case StatModifierType.Additive:
                        value += mod._valueChange;
                        break;
                    case StatModifierType.Subtractive:
                        value -= mod._valueChange;
                        break;

                    case StatModifierType.PercentIncrease:
                        value += Mathf.RoundToInt(0.01f * mod._valueChange * statValue);
                        break;
                    case StatModifierType.PercentDecrease:
                        value -= Mathf.RoundToInt(0.01f * mod._valueChange * statValue);
                        break;

                    case StatModifierType.PercentIncreaseCompound:
                        value = Mathf.RoundToInt(value * (1 + 0.01f * mod._valueChange));
                        break;
                    case StatModifierType.PercentDecreaseCompound:
                        value = Mathf.RoundToInt(value * (1 - 0.01f * mod._valueChange));
                        break;

                    default:
                        break;
                }
            }

            if (value < 1) value = 1;
            if (value > valueCap) value = valueCap;

            return value;
        }

        public void AddModifier(StatModifier modifier)
        {
            statModifiers.Add(modifier);
            valueChanged = true;
            currentStatValue = CurrentValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (statModifiers.Contains(modifier))
            {
                statModifiers.Remove(modifier);
                valueChanged = true;
                currentStatValue = CurrentValue();
            }
        }

        public void ClearModifier()
        {
            statModifiers.Clear();
            valueChanged = true;
            currentStatValue = CurrentValue();
        }
    }
}