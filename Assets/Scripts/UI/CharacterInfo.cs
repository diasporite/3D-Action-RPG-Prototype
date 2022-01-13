using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class CharacterInfo : MonoBehaviour
    {
        [SerializeField] PartyManager player;

        [Header("UI")]
        [SerializeField] Text characterName;

        [SerializeField] ResourceUI partyHealth;
        [SerializeField] ResourceUI charStamina;
        [SerializeField] ResourceUI charPoise;

        private void OnDisable()
        {
            UnsubscribeFromDelegates();
        }

        public void InitUI(PartyManager player)
        {
            this.player = player;

            partyHealth.InitUI(player);
            charStamina.InitUI(player);
            charPoise.InitUI(player);

            SubscribeToDelegates();
        }

        void SubscribeToDelegates()
        {
            player.onCharacterChanged += SetCharName;
            player.onCharacterChanged += UpdateCharacter;

            player.onHealthTick += UpdateHealth;
            player.onStaminaTick += UpdateStamina;
            player.onPoiseTick += UpdatePoise;
        }

        void UnsubscribeFromDelegates()
        {
            player.onCharacterChanged -= SetCharName;
            player.onCharacterChanged -= UpdateCharacter;

            player.onHealthTick -= UpdateHealth;
            player.onStaminaTick -= UpdateStamina;
            player.onPoiseTick -= UpdatePoise;
        }

        void SetCharName(Combatant combatant)
        {
            characterName.text = combatant.Character.CharName;
        }

        void UpdateCharacter(Combatant combatant)
        {
            partyHealth.UpdateCharacter(player.PartyHealth);
            charStamina.UpdateCharacter(combatant.Stamina);
            charPoise.UpdateCharacter(combatant.Poise);
        }

        void UpdateActiveParty()
        {
            partyHealth.UpdateCharacter(player.PartyHealth);
            charStamina.UpdateCharacter(player.CurrentPartyMember.Stamina);
            charPoise.UpdateCharacter(player.CurrentPartyMember.Poise);
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