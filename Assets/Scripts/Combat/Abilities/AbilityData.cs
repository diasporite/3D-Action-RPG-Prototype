using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class AbilityData : ScriptableObject
    {
        public string abilityName;

        public int baseSpCost;
        public int baseUsage;

        public AnimationClip animation;
        public Weapon weapon;

        public int numOfHits;

        public virtual Ability Ability => new Ability();
    }
}