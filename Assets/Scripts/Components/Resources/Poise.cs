using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Poise : Resource
    {
        protected override void UpdateUI()
        {
            party.InvokePoiseChange();
        }
    }
}