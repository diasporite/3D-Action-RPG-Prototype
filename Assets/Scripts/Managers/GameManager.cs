using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        public bool Paused { get; private set; }

        [Header("Time")]
        [SerializeField] float inGameTime = 0;
        [SerializeField] float realTime = 0;

        [Header("Databases")]
        public CharDatabase characters;
        public CombatDatabase combat;
        public SkillDatabase skill;

        public string[] gameOverTexts;

        UIManager ui;

        PartyManager party;

        public readonly StateMachine sm = new StateMachine();

        public CombatDatabase Combat => combat;

        public UIManager Ui => ui;

        public PartyManager Party => party;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            characters.InitDatabase();
            combat.InitDatabase();
            skill.InitDatabase();

            ui = FindObjectOfType<UIManager>();

            party = FindObjectOfType<PlayerInputController>().GetComponent<PartyManager>();

            sm.AddState("running", new GameRunningState(this));
            sm.AddState("game over", new GameOverState(this));
        }

        private void Start()
        {
            inGameTime = 0;
            realTime = 0;

            sm.ChangeState("running");

            FindObjectOfType<PlayerSpawner>().GetComponent<PartyManager>().OnDeath += GameOver;
        }

        private void Update()
        {
            if (Input.GetKeyDown("g")) PauseGame();

            inGameTime += Time.deltaTime;
            realTime += Time.unscaledDeltaTime;
        }

        private void OnDisable()
        {
            FindObjectOfType<PlayerSpawner>().GetComponent<PartyManager>().OnDeath -= GameOver;
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

        void GameOver()
        {
            sm.ChangeState("game over");
        }
    }
}