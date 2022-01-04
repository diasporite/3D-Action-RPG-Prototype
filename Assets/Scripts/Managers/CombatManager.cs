using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CombatManager : MonoBehaviour
    {
        [Header("Damage Multipliers")]
        public float critMultiplier = 1.5f;

        public float weakMultiplier = 1.4f;
        public float resistMultiplier = 0.7f;

        public float stabMultiplier = 1.2f;

        [Header("Running Speeds")]
        public float lightweightWalk = 7f;
        public float lightweightRun = 10.5f;

        public float middleweightWalk = 6f;
        public float middleweightRun = 9f;

        public float heavyweightWalk = 4.5f;
        public float heavyweightRun = 6.75f;

        [Header("Resource Regen")]
        public float staminaRegen = 12f;
        public float staminaRun = -2f;

        public float poiseRegen = 18f;
    }
}