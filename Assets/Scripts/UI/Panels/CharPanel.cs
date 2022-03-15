using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RPG_Project
{
    public class CharPanel : UIPanel
    {
        [SerializeField] Combatant character;
        [SerializeField] int characterIndex;

        [SerializeField] Image portrait;
        [SerializeField] Text charName;
        [SerializeField] Text element;

        public Combatant Character => character;

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);
        }

        public override void UpdateUI()
        {

        }

        public override void ShowUI(bool value)
        {
            base.ShowUI(value);

            if (value) UpdateUI();
        }

        public override void SelectUI()
        {
            base.SelectUI();
        }

        public void SetCharacter()
        {
            var controller = party.GetPartyMember(characterIndex);
            if (controller != null) character = controller.GetComponent<Combatant>();

            if (character != null)
            {
                portrait.gameObject.SetActive(true);
                charName.gameObject.SetActive(true);
                element.gameObject.SetActive(true);

                portrait.sprite = character.Character.Portrait;
                charName.text = character.Character.CharName;

                string e1 = character.Character.Element1.ToString();
                string e2 = character.Character.Element2.ToString();

                if (e1 == "Typeless") e1 = "";
                if (e2 == "Typeless") e2 = "";

                if (e2 != "") element.text = e1 + "/" + e2;
                else element.text = e1;
            }
            else
            {
                portrait.gameObject.SetActive(false);
                charName.gameObject.SetActive(false);
                element.gameObject.SetActive(false);
            }
        }

        public void SetCharacter(Combatant character)
        {
            if (character != null)
            {
                portrait.gameObject.SetActive(true);
                charName.gameObject.SetActive(true);
                element.gameObject.SetActive(true);

                this.character = character;

                portrait.sprite = character.Character.Portrait;
                charName.text = character.Character.CharName;

                string e1 = character.Character.Element1.ToString();
                string e2 = character.Character.Element2.ToString();

                if (e1 == "Typeless") e1 = "";
                if (e2 == "Typeless") e2 = "";

                if (e2 != "") element.text = e1 + "/" + e2;
                else element.text = e1;
            }
            else
            {
                portrait.gameObject.SetActive(false);
                charName.gameObject.SetActive(false);
                element.gameObject.SetActive(false);
            }
        }
    }
}