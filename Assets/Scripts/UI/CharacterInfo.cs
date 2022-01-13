using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class CharacterInfo : UIElement
    {
        //[SerializeField] PartyManager player;

        [Header("UI")]
        [SerializeField] Text characterName;

        [SerializeField] ResourceUI partyHealth;
        [SerializeField] ResourceUI charStamina;
        [SerializeField] ResourceUI charPoise;

        public override void InitUI(PartyManager player)
        {
            base.InitUI(player);

            partyHealth.InitUI(player);
            charStamina.InitUI(player);
            charPoise.InitUI(player);

            SubscribeToDelegates();
        }

        protected override void SubscribeToDelegates()
        {
            party.onCharacterChanged += SetCharName;
            party.onCharacterChanged += UpdateCharacter;

            party.onHealthTick += UpdateHealth;
            party.onStaminaTick += UpdateStamina;
            party.onPoiseTick += UpdatePoise;
        }

        protected override void UnsubscribeFromDelegates()
        {
            party.onCharacterChanged -= SetCharName;
            party.onCharacterChanged -= UpdateCharacter;

            party.onHealthTick -= UpdateHealth;
            party.onStaminaTick -= UpdateStamina;
            party.onPoiseTick -= UpdatePoise;
        }

        void SetCharName(Combatant combatant)
        {
            characterName.text = combatant.Character.CharName;
        }

        void UpdateCharacter(Combatant combatant)
        {
            partyHealth.UpdateCharacter(party.PartyHealth);
            charStamina.UpdateCharacter(combatant.Stamina);
            charPoise.UpdateCharacter(combatant.Poise);
        }

        void UpdateActiveParty()
        {
            partyHealth.UpdateCharacter(party.PartyHealth);
            charStamina.UpdateCharacter(party.CurrentPartyMember.Stamina);
            charPoise.UpdateCharacter(party.CurrentPartyMember.Poise);
        }

        void UpdateUI()
        {
            UpdateHealth();
            UpdateStamina();
            UpdatePoise();
        }

        void UpdateHealth()
        {
            partyHealth.UpdateUI();
        }

        void UpdateStamina()
        {
            charStamina.UpdateUI();
        }

        void UpdatePoise()
        {
            charPoise.UpdateUI();
        }
    }
}