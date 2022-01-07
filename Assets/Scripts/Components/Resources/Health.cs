using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Health : Resource
    {
        protected override void Start()
        {
            base.Start();

            //party.onHealthChanged +=
        }

        protected override void OnDestroy()
        {
            //party.onHealthChanged -=
        }

        protected override void UpdateUI()
        {
            base.UpdateUI();
            if (statText != null)
                statText.text = "HP " + resourcePoints.PointValue + "/" + resourcePoints.CurrentStatValue;
        }

        public override void SaveToCharacter()
        {
            character.Health.PointValue = resourcePoints.PointValue;
            character.HealthResource = resource.Count;
        }

        public override void LoadFromCharacter()
        {
            //print(324);
            resourcePoints = character.Health;
            resource._cooldown = resourcePoints.CurrentStatValue;
            resource.Count = resourcePoints.PointValue;
        }
    }
}