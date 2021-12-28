using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        public BattleUI battleUi;

        CombatManager combat;
        InputManager input;

        PartyManager party;

        public BattleUI BattleUi => battleUi;

        public CombatManager Combat => combat;
        public InputManager Input => input;

        public PartyManager Party => party;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            combat = GetComponent<CombatManager>();
            input = GetComponent<InputManager>();

            party = FindObjectOfType<PartyManager>();
        }
    }
}