using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Poise : Resource
    {
        protected override void UpdateUI()
        {
            base.UpdateUI();
            if (statText != null)
                statText.text = "PP " + resourcePoints.PointValue + "/" + resourcePoints.CurrentStatValue;
        }

        public override void SaveToCharacter()
        {
            character.Poise.PointValue = resourcePoints.PointValue;
            character.PoiseResource = resource.Count;
        }

        public override void LoadFromCharacter()
        {
            resourcePoints = character.Poise;
            resource._cooldown = resourcePoints.CurrentStatValue;
            resource.Count = resourcePoints.PointValue;
        }
    }
}