using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] PartyManager party;

        public CharacterInfo charInfo;
        public DPadMenu shortcuts;
        public CharacterAbilities charAbilities;
        public BasicActions basicActions;

        public void InitUI(PartyManager player)
        {
            party = player;

            charInfo.InitUI(party);
            shortcuts.InitUI(party);
            charAbilities.InitUI(party);
            basicActions.InitUI(party);
        }
    }
}