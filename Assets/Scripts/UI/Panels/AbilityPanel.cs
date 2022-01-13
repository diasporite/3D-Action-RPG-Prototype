using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class AbilityPanel : UIPanel
    {
        public AbilityOrientation orientation;
        [SerializeField] Ability ability;

        [SerializeField] Image element;
        [SerializeField] Image weapon;
        [SerializeField] Text skillName;
        [SerializeField] Text spCost;
        [SerializeField] Text uses;

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);
        }

        public override void UpdateUI()
        {
            base.UpdateUI();

            skillName.text = ability.skill.SkillName;
            spCost.text = "SP " + ability.SpCost;
            uses.text = ability.skill.Uses.PointValue + "/" +
                ability.skill.Uses.CurrentStatValue;
        }

        public void UpdatePanel(Combatant combatant)
        {
            var ability = combatant.Abilities.GetAbility(orientation);
            this.ability = ability;

            UpdateUI();
        }
    }
}