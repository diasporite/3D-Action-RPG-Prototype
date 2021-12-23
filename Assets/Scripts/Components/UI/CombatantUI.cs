using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class CombatantUI : MonoBehaviour, ICombatantUI
    {
        //protected Combatant combatant;

        [SerializeField] protected Slider healthBar;

        //protected BattleManager battle;

        public Slider HealthBar => healthBar;

        protected virtual void Awake()
        {
            //combatant = GetComponent<Combatant>();
        }

        public virtual void UpdateUI()
        {
            //healthBar.value = combatant.Character._health.PointFraction;
        }
    }
}