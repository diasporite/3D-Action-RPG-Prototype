using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Stamina : Resource
    {
        [SerializeField] bool inRecovery = false;

        [SerializeField] float staminaRegen = 6;
        [SerializeField] float runningRegen = -3;

        public bool InRecovery
        {
            get => inRecovery;
            set => inRecovery = value;
        }

        protected override void UpdateUI()
        {
            base.UpdateUI();
            if (statText != null)
                statText.text = "SP " + resourcePoints.PointValue + "/" + resourcePoints.CurrentStatValue;
        }

        public void Run(bool value)
        {
            if (value) CurrentRegen = runningRegen;
            else CurrentRegen = staminaRegen;
        }
    }
}