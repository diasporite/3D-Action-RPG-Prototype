using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Action", menuName = "Combat/Action")]
    public class ActionData : ScriptableObject
    {
        [Header("Info")]
        public string actionName;

        public Weapon weapon;
        public AnimationClip animation;

        [Range(1, 5)]
        public int numberOfHits = 1;

        public int spCost = 1;
    }
}