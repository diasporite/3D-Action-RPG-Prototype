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

        [Range(1, 5)]
        public int numberOfHits = 1;
    }
}
