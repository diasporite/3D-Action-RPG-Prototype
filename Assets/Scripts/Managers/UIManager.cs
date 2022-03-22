using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class UIManager : MonoBehaviour
    {
        public PartyManager playerParty;

        public BattleUI battleUi;
        public GameOverScreen gameOverScreen;

        //public CharacterInfo charInfo;
        //public DPadMenu shortcuts;
        //public CharacterAbilities charAbilities;

        public void InitUI(PartyManager party)
        {
            playerParty = party;

            battleUi.InitUI(playerParty);

            //charInfo.InitUI(party);
            //shortcuts.InitUI(party);
            //charAbilities.InitUI(party);
        }
    }
}