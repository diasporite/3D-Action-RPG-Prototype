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
        [SerializeField] ResourceUI partyStamina;
        [SerializeField] ResourceUI charPoise;

        public override void InitUI(PartyManager player)
        {
            base.InitUI(player);

            partyHealth.InitUI(player);
            partyStamina.InitUI(player);
            charPoise.InitUI(player);

            SubscribeToDelegates();
        }

        protected override void SubscribeToDelegates()
        {
            party.OnCharacterChanged += SetCharName;
            party.OnCharacterChanged += UpdateCharacter;

            party.OnHealthChange += UpdateHealth;
            party.OnStaminaChange += UpdateStamina;
            party.OnPoiseChange += UpdatePoise;
        }

        protected override void UnsubscribeFromDelegates()
        {
            party.OnCharacterChanged -= SetCharName;
            party.OnCharacterChanged -= UpdateCharacter;

            party.OnHealthChange -= UpdateHealth;
            party.OnStaminaChange -= UpdateStamina;
            party.OnPoiseChange -= UpdatePoise;
        }

        void SetCharName(Combatant combatant)
        {
            characterName.text = combatant.Character.CharName;
        }

        void UpdateCharacter(Combatant combatant)
        {
            partyHealth.UpdateCharacter(party.PartyHealth);
            partyStamina.UpdateCharacter(party.PartyStamina);
            //charPoise.UpdateCharacter(combatant.Poise);
        }

        void UpdateActiveParty()
        {
            partyHealth.UpdateCharacter(party.PartyHealth);
            partyStamina.UpdateCharacter(party.PartyStamina);
            //charPoise.UpdateCharacter(party.CurrentPartyMember.Poise);
        }

        void UpdateUI()
        {
            UpdateHealth();
            UpdateStamina();
            //UpdatePoise();
        }

        void UpdateHealth()
        {
            partyHealth.UpdateUI();
        }

        void UpdateStamina()
        {
            partyStamina.UpdateUI();
        }

        void UpdatePoise()
        {
            charPoise.UpdateUI();
        }
    }
}