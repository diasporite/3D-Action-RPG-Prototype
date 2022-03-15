using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CharacterAbilities : UIElement
    {
        //public AbilityPanel topLeftAbility;
        //public AbilityPanel topRightAbility;
        //public AbilityPanel bottomLeftAbility;
        //public AbilityPanel bottomRightAbility;

        public AbilityPanel[] abilityPanels;

        private void Awake()
        {
            abilityPanels = GetComponentsInChildren<AbilityPanel>();
        }

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            foreach (var panel in abilityPanels) panel.InitUI(party);

            SubscribeToDelegates();

            UpdateAbilities(party.CurrentCombatant);
        }

        protected override void SubscribeToDelegates()
        {
            party.OnCharacterChanged += UpdateAbilities;

            party.OnAbilityUse += UpdateAbilityPanel;
            party.OnAbilityUse += SelectAbilityPanel;
        }

        protected override void UnsubscribeFromDelegates()
        {
            party.OnCharacterChanged -= UpdateAbilities;

            party.OnAbilityUse -= UpdateAbilityPanel;
            party.OnAbilityUse -= SelectAbilityPanel;
        }

        void UpdateAbilities(Combatant combatant)
        {
            //topLeftAbility.UpdatePanel(combatant);
            //topRightAbility.UpdatePanel(combatant);
            //bottomLeftAbility.UpdatePanel(combatant);
            //bottomRightAbility.UpdatePanel(combatant);

            foreach (var panel in abilityPanels)
                panel.UpdatePanel(combatant);
        }

        void UpdateAbilityPanel(int index)
        {
            index = Mathf.Abs(index);
            index = index % abilityPanels.Length;

            abilityPanels[index].UpdateUI();
        }

        void SelectAbilityPanel(int index)
        {
            index = Mathf.Abs(index);
            index = index % abilityPanels.Length;

            abilityPanels[index].SelectUI();
        }
    }
}