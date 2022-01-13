using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class HUDMenu : MonoBehaviour
    {
        PartyManager party;
        [SerializeField] UIPanel[] panels = new UIPanel[4];

        public void InitUI(PartyManager party)
        {
            this.party = party;
            panels = GetComponentsInChildren<UIPanel>();
            foreach (var p in panels) p.InitUI(party);
        }
    }
}