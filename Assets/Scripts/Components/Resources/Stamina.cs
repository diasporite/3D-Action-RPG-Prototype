using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Stamina : Resource
    {
        [SerializeField] bool inRecovery = false;

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

        public override void SaveToCharacter()
        {
            character.Stamina.PointValue = resourcePoints.PointValue;
            character.StaminaResource = resource.Count;
        }

        public override void LoadFromCharacter()
        {
            resourcePoints = character.Stamina;
            resource._cooldown = resourcePoints.CurrentStatValue;
            resource.Count = resourcePoints.PointValue;
        }

        public void Run(bool value)
        {
            if (value) CurrentRegen = GameManager.instance.Combat.staminaRun;
            else CurrentRegen = GameManager.instance.Combat.staminaRegen;
        }
    }
}