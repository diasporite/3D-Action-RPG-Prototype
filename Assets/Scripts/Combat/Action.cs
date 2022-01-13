using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Action
    {
        public Weapon weapon;
        public AnimationClip animation;

        [SerializeField] WeaponID weaponId;

        [Range(1, 5)]
        public int numberOfHits = 1;

        public int spCost = 3;

        public void InitAction()
        {

        }
    }
}
