using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CharacterAbilities : UIElement
    {
        public AbilityPanel topLeftAbility;
        public AbilityPanel topRightAbility;
        public AbilityPanel bottomLeftAbility;
        public AbilityPanel bottomRightAbility;

        public AbilityPanel[] abilityPanels;

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            SubscribeToDelegates();

            UpdateAbilities(party.CurrentCombatant);
        }

        protected override void SubscribeToDelegates()
        {
            party.OnCharacterChanged += UpdateAbilities;
        }

        protected override void UnsubscribeFromDelegates()
        {
            party.OnCharacterChanged -= UpdateAbilities;
        }

        void UpdateAbilities(Combatant combatant)
        {
            topLeftAbility.UpdatePanel(combatant);
            topRightAbility.UpdatePanel(combatant);
            bottomLeftAbility.UpdatePanel(combatant);
            bottomRightAbility.UpdatePanel(combatant);

            foreach (var panel in abilityPanels)
                panel.UpdatePanel(combatant);
        }
    }
}