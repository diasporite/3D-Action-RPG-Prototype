using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Action", menuName = "Combat/Action")]
    public class CombatAction : ScriptableObject
    {
        public AnimationClip animation;
        public Weapon weapon;

        [Range(1, 5)]
        public int numberOfHits = 1;

        public int baseSpCost = 1;

        [Range(1, 40)]
        public int baseUsage = 20;
    }
}
