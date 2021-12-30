using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum ControllerMode
    {
        Walk = 0,
        Run = 1,
        Action = 2,
        Stagger = 3,
        Roll = 4,
        Dodge = 5,
        Jump = 6,
        BasicAction = 7,    // Non combat actions (talk, pickup, interact)
        LeftWeapon = 8,
        RightWeapon = 9,
    }

    public enum WeaponHand
    {
        Empty = 0,
        Left = 1,
        Right = 2,
    }

    [RequireComponent(typeof(Movement), typeof(Combatant), typeof(ActionQueue))]
    public class Controller : MonoBehaviour
    {
        public readonly string MOVE = "move";
        public readonly string RUN = "run";
        public readonly string RECOVER = "recover";
        public readonly string STAGGER = "stagger";
        public readonly string ACTION = "action";
        public readonly string LEFT_WEAPON = "left weapon";
        public readonly string RIGHT_WEAPON = "right weapon";
        public readonly string WEAPON = "weapon";
            
        [SerializeField] protected ControllerMode mode;
        [SerializeField] protected WeaponHand currentWeapon;

        [SerializeField] protected string currentState;

        Vector3 inputDir = new Vector3(0, 0);

        protected Animator anim;

        protected Movement movement;
        protected Combatant combatant;
        protected ActionQueue queue;
        protected WeaponManager weapon;
        //protected LockOn lockOn;

        protected Health health;
        protected Stamina stamina;
        protected Poise poise;

        protected StateMachine sm = new StateMachine();

        protected InputManager inputManager;

        public ControllerMode Mode
        {
            get => mode;
            set => mode = value;
        }

        public WeaponHand Hand
        {
            get => currentWeapon;
            set => currentWeapon = value;
        }

        public Vector3 RawInputDir
        {
            get
            {
                inputDir.x = Input.GetAxisRaw("Horizontal");
                inputDir.z = Input.GetAxisRaw("Vertical");

                return inputDir;
            }
        }

        public Vector3 InputDir
        {
            get
            {
                inputDir.x = Input.GetAxis("Horizontal");
                inputDir.z = Input.GetAxis("Vertical");

                return inputDir;
            }
        }

        public Animator Anim => anim;

        public Movement Movement => movement;
        public Combatant Combatant => combatant;
        public ActionQueue Queue => queue;
        public WeaponManager Weapon => weapon;
        //public LockOn LockOn => lockOn;

        public Health Health => health;
        public Stamina Stamina => stamina;
        public Poise Poise => poise;

        public StateMachine Sm => sm;

        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();

            movement = GetComponent<Movement>();
            combatant = GetComponent<Combatant>();
            queue = GetComponent<ActionQueue>();
            weapon = GetComponent<WeaponManager>();
            //lockOn = GetComponent<LockOn>();

            health = GetComponent<Health>();
            stamina = GetComponent<Stamina>();
            poise = GetComponent<Poise>();
        }

        protected virtual void Start()
        {
            inputManager = GameManager.instance.Input;

            InitSM();
        }

        protected virtual void Update()
        {
            sm.Update();

            currentState = sm.GetCurrentKey.ToString();
        }

        protected virtual void OnDrawGizmos()
        {
            
        }

        protected virtual void InitSM()
        {

        }

        public void Move(Vector3 dir)
        {
            movement.MovePosition(dir, Time.deltaTime);
        }

        public void ResourceTick(float dt)
        {
            health.Tick(dt);
            stamina.Tick(dt);
            poise.Tick(dt);
        }

        public void SetRegen(bool value)
        {
            health.Regenerative = value;
            stamina.Regenerative = value;
            poise.Regenerative = value;
        }

        public void AddCommand(int index)
        {
            queue.AddAction(new AttackAction(this, transform.forward));
        }

        public void AddCommand(BattleAction action)
        {
            queue.AddAction(action);
        }
    }
}