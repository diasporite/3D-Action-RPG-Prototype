using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class DamageEffect : SkillEffect
    {
        [Range(1, 5)]
        [SerializeField] int numOfHits = 1;

        [SerializeField] int healthDps = 1;
        [SerializeField] int staminaDps = 1;

        [SerializeField] int healthDamage = 1;
        [SerializeField] int staminaDamage = 1;

        public int HealthDamage(float duration) => 
            Mathf.RoundToInt(healthDps * duration / numOfHits);
        public int StaminaDamage(float duration) => 
            Mathf.RoundToInt(staminaDps * duration / numOfHits);

        public DamageEffect() : base()
        {

        }

        public void CalculateDamage(float duration)
        {
            healthDamage = Mathf.RoundToInt(healthDps * duration / numOfHits);
            staminaDamage = Mathf.RoundToInt(staminaDps * duration / numOfHits);
        }

        public void CalculateDamage(float duration, int numOfHits)
        {
            healthDamage = Mathf.RoundToInt(healthDps * duration / numOfHits);
            staminaDamage = Mathf.RoundToInt(staminaDps * duration / numOfHits);
        }
    }
}