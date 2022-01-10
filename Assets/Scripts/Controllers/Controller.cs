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
        Guard = 5,
        Jump = 6,
        BasicAction = 7,    // Non combat actions (talk, pickup, interact)
        Death = 8,
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
        public readonly string ACTION = "action";
        public readonly string SPECIAL_ACTION = "special action";
        public readonly string STAGGER = "stagger";
        public readonly string DEATH = "death";

        [SerializeField] protected ControllerMode mode;
        [SerializeField] protected WeaponHand currentWeapon;

        [SerializeField] protected string currentState;

        Vector3 inputDir = new Vector3(0, 0);

        protected Animator anim;

        protected Movement movement;
        protected Combatant combatant;
        protected ActionQueue queue;
        protected AbilityManager ability;
        protected LockOn lockOn;

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
                inputDir.y = Input.GetAxisRaw("Vertical");

                return inputDir;
            }
        }

        public Vector3 RawInputDirXz
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
                inputDir.y = Input.GetAxis("Vertical");

                return inputDir;
            }
        }

        public Vector3 InputDirXz
        {
            get
            {
                inputDir.x = Input.GetAxis("Horizontal");
                inputDir.y = Input.GetAxis("Vertical");

                return inputDir;
            }
        }

        public Animator Anim => anim;

        public Movement Movement => movement;
        public Combatant Combatant => combatant;
        public ActionQueue Queue => queue;
        public AbilityManager Ability => ability;
        public LockOn LockOn => lockOn;

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
            ability = GetComponent<AbilityManager>();
            lockOn = GetComponent<LockOn>();

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
            sm.AddState(MOVE, new ControllerMovementState(this));
            sm.AddState(RUN, new ControllerRunningState(this));
            sm.AddState(ACTION, new ControllerActionState(this));
            sm.AddState(RECOVER, new ControllerRecoveryState(this));
            sm.AddState(STAGGER, new ControllerStaggerState(this));
            sm.AddState(DEATH, new ControllerDeathState(this));
        }

        #region StateCommands
        public virtual void MovementCommand()
        {
            ResourceTick(Time.deltaTime);
        }

        public virtual void RunCommand()
        {
            ResourceTick(Time.deltaTime);

            if (stamina.Empty) sm.ChangeState(RECOVER);
        }

        public virtual void ActionCommand()
        {

        }

        public virtual void RecoveryCommand()
        {
            ResourceTick(Time.deltaTime);
        }

        public virtual void StaggerCommand()
        {

        }

        public virtual void DeathCommand()
        {

        }
        #endregion

        public void Move(Vector3 dir)
        {
            movement.MovePosition(dir, Time.deltaTime);
        }

        public void Run()
        {
            sm.ChangeState(RUN);
        }

        public void SpecialAction()
        {
            var weightclass = combatant.Character.Weightclass;
            BattleCommand act;

            switch (weightclass)
            {
                case WeightClass.Lightweight:
                    act = new JumpCommand(this);
                    AddCommand(act);
                    break;
                case WeightClass.Heavyweight:
                    act = new GuardCommand(this);
                    AddCommand(act);
                    break;
                default:
                    act = new RollCommand(this);
                    AddCommand(act);
                    break;
            }
        }

        public void UseAbility(int index)
        {
            index = Mathf.Abs(index);
            index = index % 4;

            var command = ability.GetAbility(index).GetCommand(this);
            if (command != null) queue.AddAction(command);
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

        public void AddAbilityCommand(int index)
        {
            var command = ability.GetAbility(index).GetCommand(this);
            if (command != null) queue.AddAction(command);
            //queue.AddAction(new AttackCommand(this, transform.forward));
        }

        public void AddCommand(BattleCommand action)
        {
            if (action != null) queue.AddAction(action);
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public void LeaveStagger()
        {
            sm.ChangeState(MOVE);
        }
    }
}