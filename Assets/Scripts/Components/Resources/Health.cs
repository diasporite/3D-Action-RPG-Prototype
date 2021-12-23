using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Health : Resource
    {
        protected override void UpdateUI()
        {
            base.UpdateUI();
            if (statText != null)
                statText.text = "HP " + resourcePoints.PointValue + "/" + resourcePoints.CurrentStatValue;
        }
    }
}