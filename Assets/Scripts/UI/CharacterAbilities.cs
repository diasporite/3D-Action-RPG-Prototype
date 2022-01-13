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

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            UpdateAbilities(party.CurrentPartyMember.Combatant);
        }

        protected override void SubscribeToDelegates()
        {
            party.onCharacterChanged += UpdateAbilities;
        }

        protected override void UnsubscribeFromDelegates()
        {
            party.onCharacterChanged -= UpdateAbilities;
        }

        void UpdateAbilities(Combatant combatant)
        {
            topLeftAbility.UpdatePanel(combatant);
            topRightAbility.UpdatePanel(combatant);
            bottomLeftAbility.UpdatePanel(combatant);
            bottomRightAbility.UpdatePanel(combatant);
        }
    }
}