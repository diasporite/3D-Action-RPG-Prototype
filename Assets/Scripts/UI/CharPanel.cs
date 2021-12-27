using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RPG_Project
{
    public class CharPanel : UIPanel
    {
        PartyManager party;
        PlayerController character;
        [SerializeField] int characterIndex;

        [SerializeField] Image portrait;
        [SerializeField] Slider spBar;
        [SerializeField] Slider ppBar;

        public override void InitPanel()
        {
            base.InitPanel();

            if (party == null) party = FindObjectOfType<PartyManager>();
            character = party.GetPartyMember(characterIndex);

            if (character != null)
            {
                ShowUI(true);
                UpdateUI(true);
            }
            else ShowUI(false);
        }

        public override void UpdateUI(bool init)
        {
            //if (init) Get character's portrait
            //spBar.value = character.Stamina._resource.CooldownFraction;
            //ppBar.value = character.Poise._resource.CooldownFraction;
        }

        public override void ShowUI(bool value)
        {
            base.ShowUI(value);

            //portrait.gameObject.SetActive(value);
            //spBar.gameObject.SetActive(value);
            //ppBar.gameObject.SetActive(value);
        }

        public override void SelectUI()
        {
            base.SelectUI();

            if (character != null) ShowUI(true);
        }
    }
}