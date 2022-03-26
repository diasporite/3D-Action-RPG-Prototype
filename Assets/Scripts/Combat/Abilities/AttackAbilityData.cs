using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class AttackAbilityData : AbilityData
    {
        public int healthDps;
        public int staminaDps;

        public override Ability Ability => base.Ability;
    }
}