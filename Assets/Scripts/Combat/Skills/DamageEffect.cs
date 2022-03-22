using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class DamageEffect : SkillEffect
    {
        [SerializeField] int healthDps = 1;
        [SerializeField] int staminaDps = 1;

        [SerializeField] int healthDamage = 1;
        [SerializeField] int staminaDamage = 1;

        public int HealthDps => healthDps;
        public int StaminaDps => staminaDps;

        public int HealthDamage => healthDamage;
        public int StaminaDamage => staminaDamage;

        public DamageEffect() : base()
        {

        }

        public DamageEffect(int healthDps, int staminaDps, CombatAction action): base()
        {
            this.healthDps = healthDps;
            this.staminaDps = staminaDps;

            CalculateDamage(action);
        }

        public void CalculateDamage(float duration, int numOfHits)
        {
            healthDamage = Mathf.RoundToInt((float)healthDps * duration / (float)numOfHits);
            staminaDamage = Mathf.RoundToInt((float)staminaDps * duration / (float)numOfHits);
        }

        public void CalculateDamage(CombatAction action)
        {
            healthDamage = Mathf.RoundToInt(healthDps * 
                action.animation.length / action.numberOfHits);
            staminaDamage = Mathf.RoundToInt(staminaDps * 
                action.animation.length / action.numberOfHits);
        }
    }
}