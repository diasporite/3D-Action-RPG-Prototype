using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{ 
    [System.Serializable]
    public class InstigatorData
    {
        [SerializeField] Combatant combatant;
        [SerializeField] int basePower;
    
        public Combatant Combatant => combatant;

        public InstigatorData(Combatant combatant, int basePower)
        {
            this.combatant = combatant;
            this.basePower = basePower;
        }
    }

    public class HitDetector : MonoBehaviour
    {
        protected Controller controller;
        protected LayerMask hittables;

        protected InstigatorData data;

        public Controller Controller => controller;
        public InstigatorData Data => data;
    }
}