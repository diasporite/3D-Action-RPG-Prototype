using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class BasicActions : UIElement
    {
        //public TextPanel shortcutPanel;
        //public TextPanel runPanel;
        public TextPanel defencePanel;
        public TextPanel actionPanel;

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            //shortcutPanel.InitUI(party);
            //runPanel.InitUI(party);
            defencePanel.InitUI(party);
            actionPanel.InitUI(party);

            SubscribeToDelegates();
        }

        protected override void SubscribeToDelegates()
        {
            party.onCharacterChanged += UpdateDefence;
        }

        protected override void UnsubscribeFromDelegates()
        {
            party.onCharacterChanged -= UpdateDefence;
        }

        public void UpdateDefence(Combatant combatant)
        {
            defencePanel.SelectUI();

            var weight = combatant.Character.Weightclass;

            switch (weight)
            {
                case WeightClass.Lightweight:
                    defencePanel.SetText("Jump");
                    break;
                case WeightClass.Heavyweight:
                    defencePanel.SetText("Guard");
                    break;
                default:
                    defencePanel.SetText("Roll");
                    break;
            }
        }
    }
}