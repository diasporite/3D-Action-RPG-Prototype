using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RPG_Project
{
    public class CharPanel : UIPanel
    {
        Controller character;
        [SerializeField] int characterIndex;

        [SerializeField] Image portrait;
        [SerializeField] ResourceUI spBar;
        [SerializeField] ResourceUI ppBar;

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            if (character != null)
            {
                ShowUI(true);
                UpdateUI();
            }
            else ShowUI(false);
        }

        public override void UpdateUI()
        {
            //if (init) Get character's portrait
            //spBar.value = character.Stamina._resource.CooldownFraction;
            //ppBar.value = character.Poise._resource.CooldownFraction;
        }

        public override void ShowUI(bool value)
        {
            base.ShowUI(value);

            if (value) UpdateUI();
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