using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class UIManager : MonoBehaviour
    {
        public PartyManager playerParty;

        public CharacterInfo charInfo;
        public DPadMenu shortcuts;
        public CharacterAbilities charAbilities;

        private void Awake()
        {

        }

        private void Start()
        {

        }

        public void InitUI(PartyManager party)
        {
            playerParty = party;

            charInfo.InitUI(party);
            shortcuts.InitUI(party);
            charAbilities.InitUI(party);
        }
    }
}