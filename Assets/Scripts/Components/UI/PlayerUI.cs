using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class PlayerUI : CombatantUI, ICombatantUI
    {
        //PlayerController player;

        [SerializeField] Slider staminaBar;
        //[SerializeField] Slider staggerBar;

        [SerializeField] Text hpText;
        [SerializeField] Text spText;

        Color staminaWait = new Color(0.5f, 0.5f, 0.5f);
        Color staminaFull = new Color(0, 0, 1);

        public Slider StaminaBar => staminaBar;

        public Text HpText => hpText;
        public Text SpText => spText;

        protected override void Awake()
        {
            base.Awake();

            //player = GetComponent<PlayerController>();
        }

        public override void UpdateUI()
        {
            base.UpdateUI();

            //staminaBar.value = combatant.Character._stamina.PointFraction;

            //hpText.text = "HP " + combatant.Character._health.PointValue + "/" + 
            //    combatant.Character._health.CurrentStatValue;
            //mpText.text = "MP " + combatant.Character._magic.PointValue + "/" + 
            //    combatant.Character._magic.CurrentStatValue;
            //spText.text = "SP " + combatant.Character._stamina.PointValue + "/" + 
            //    combatant.Character._stamina.CurrentStatValue;
        }
    }
}