using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        public bool Paused { get; private set; }

        [SerializeField] float inGameTime = 0;
        [SerializeField] float realTime = 0;

        public CharDatabase characters;
        public CombatDatabase combat;

        UIManager ui;

        PartyManager party;

        public CombatDatabase Combat => combat;

        public UIManager Ui => ui;

        public PartyManager Party => party;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            characters.InitDatabase();
            combat.InitDatabase();

            ui = FindObjectOfType<UIManager>();

            party = FindObjectOfType<PlayerInputController>().GetComponent<PartyManager>();
        }

        private void Start()
        {
            inGameTime = 0;
            realTime = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown("g")) PauseGame();

            inGameTime += Time.deltaTime;
            realTime += Time.unscaledDeltaTime;
        }

        void InitGame()
        {

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