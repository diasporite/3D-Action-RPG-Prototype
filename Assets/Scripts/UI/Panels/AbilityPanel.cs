using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class AbilityPanel : UIPanel
    {
        public int index;
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
            spCost.text = ability.SpCost + " SP";
            uses.text = ability.Uses.PointValue + "/" +
                ability.Uses.CurrentStatValue;
        }

        public void UpdatePanel(Combatant combatant)
        {
            var ability = combatant.Abilities.GetAbility(index);
            this.ability = ability;

            UpdateUI();
        }
    }
}