using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        public bool Paused { get; private set; }

        public BattleUI battleUi;

        public CombatDatabase combat;

        InputManager input;

        PartyManager party;

        public BattleUI BattleUi => battleUi;

        public CombatDatabase Combat => combat;
        public InputManager Input => input;

        public PartyManager Party => party;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            combat.InitDatabase();

            input = GetComponent<InputManager>();

            party = FindObjectOfType<PartyManager>();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("h")) PauseGame();
        }

        void PauseGame()
        {
            Paused = !Paused;

            if (Paused) Time.timeScale = 0;
            else Time.timeScale = 1;
        }

        void PauseGame(bool value)
        {
            Paused = value;

            if (Paused) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }
}