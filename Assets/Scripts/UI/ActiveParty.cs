using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ActiveParty : UIElement
    {
        [SerializeField] int startingIndex = 0;
        [SerializeField] CharPanel[] panels;

        private void Awake()
        {
            panels = GetComponentsInChildren<CharPanel>();
        }

        protected override void SubscribeToDelegates()
        {
            party.OnCharacterChanged += UpdateCombatant;
        }

        protected override void UnsubscribeFromDelegates()
        {
            party.OnCharacterChanged -= UpdateCombatant;
        }

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            InitPanels();
        }

        void InitPanels()
        {
            for(int i = 0; i < panels.Length; i++)
            {
                panels[i].InitUI(party);
                panels[i].SetCharacter();
            }
        }

        void UpdatePanels()
        {
            foreach (var panel in panels) panel.SetCharacter();
        }

        void UpdateCombatant(Combatant combatant)
        {
            foreach (var panel in panels)
            {
                if (panel.Character == combatant)
                {
                    panel.SelectUI();
                    panel.HighlightPanel(true);
                }
                else panel.HighlightPanel(false);
            }
        }

        void SelectCharacter(int index)
        {
            var i = index - startingIndex;
            i = Mathf.Abs(i) % panels.Length;

            for (int j = 0; j < panels.Length; j++)
            {
                if (j == i)
                {
                    panels[j].SelectUI();
                    panels[j].HighlightPanel(true);
                }
                else panels[j].HighlightPanel(false);
            }
        }
    }
}