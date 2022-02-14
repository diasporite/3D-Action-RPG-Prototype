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

    [RequireComponent(typeof(Movement), typeof(Combatant))]
    public class Controller : MonoBehaviour
    {
        // State keys
        public readonly string MOVE = "move";
        public readonly string RUN = "run";
        public readonly string RECOVER = "recover";
        public readonly string ACTION = "action";
        public readonly string SPECIAL_ACTION = "special action";
        public readonly string STAGGER = "stagger";
        public readonly string DEATH = "death";

        [Header("Inputs")]
        [SerializeField] InputMode inputMode;
        [SerializeField] Vector3 inputDir = new Vector3(0, 0);

        [Header("Info")]
        [SerializeField] protected ControllerMode mode;
        [SerializeField] protected string currentState;

        protected PartyManager party;
        protected Health health;
        protected ActionQueue queue;
        protected LockOn lockOn;
        protected InputController inputController;
        protected AIController ai;

        protected Animator anim;

        protected Movement movement;
        protected Combatant combatant;
        protected AbilityManager ability;

        protected Stamina stamina;
        protected Poise poise;

        protected HitDetector[] hitDetectors;
        protected Hurtbox hurtbox;

        protected CharacterHealth charHealth;

        protected StateMachine sm = new StateMachine();

        public ControllerMode Mode
        {
            get => mode;
            set => mode = value;
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

        public PartyManager Party => party;
        public Health Health => health;
        public ActionQueue Queue => queue;
        public LockOn LockOn => lockOn;
        public InputController InputController => inputController;

        public Animator Anim => anim;

        public Movement Movement => movement;
        public Combatant Combatant => combatant;
        public AbilityManager Ability => ability;

        public Stamina Stamina => stamina;
        public Poise Poise => poise;

        public HitDetector[] HitDetectors => hitDetectors;
        public Hurtbox Hurtbox => hurtbox;

        public BattleChar Character => combatant.Character;

        public StateMachine Sm => sm;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            //InitController();
        }

        protected virtual void Update()
        {
            GetInputs();

            sm.Update();

            currentState = sm.GetCurrentKey.ToString();
        }

        private void OnEnable()
        {
            SubscribeToDelegates();
        }

        private void OnDisable()
        {
            UnsubscribeFromDelegates();
        }

        protected virtual void OnDrawGizmos()
        {
            
        }

        public virtual void InitController(bool player, LayerMask hittables)
        {
            party = GetComponentInParent<PartyManager>();
            ai = GetComponentInParent<AIController>();

            health = GetComponentInParent<Health>();
            stamina = GetComponentInParent<Stamina>();
            //poise = GetComponentInParent<Poise>();

            anim = GetComponent<Animator>();

            movement = GetComponent<Movement>();
            combatant = GetComponent<Combatant>();
            ability = GetComponent<AbilityManager>();

            hitDetectors = GetComponentsInChildren<HitDetector>();
            hurtbox = GetComponentInChildren<Hurtbox>();
            
            //if (!player)
            //{
                charHealth = GetComponentInChildren<CharacterHealth>();
                charHealth.InitUI(this);
            //}

            queue = party.ActionQueue;
            lockOn = party.LockOn;
            inputController = party.InputController;

            combatant.InitCombatant();
            movement.InitMovement(player);
            ability.InitAbilities(hittables);

            hurtbox.Init(this);

            //health.InitResource();
            //stamina.InitResource();
            //poise.InitResource();

            InitSM();
        }

        protected virtual void InitSM()
        {
            sm.AddState(MOVE, new ControllerMovementState(this));
            sm.AddState(RUN, new ControllerRunningState(this));
            sm.AddState(ACTION, new ControllerActionState(this));
            sm.AddState(RECOVER, new ControllerRecoveryState(this));
            sm.AddState(STAGGER, new ControllerStaggerState(this));
            sm.AddState(DEATH, new ControllerDeathState(this));

            sm.ChangeState(MOVE);
        }

        protected virtual void SubscribeToDelegates()
        {

        }

        protected virtual void UnsubscribeFromDelegates()
        {

        }

        void GetInputs()
        {
            if (ai != null) ai.UpdateSM();

            inputMode = InputController.GetInput();
            inputDir = InputController.GetOutputDir1();
        }

        #region StateCommands
        public virtual void MovementCommand()
        {
            stamina.Tick(Time.deltaTime);

            switch (inputMode)
            {
                case InputMode.Run:
                    Run();
                    break;
                case InputMode.Defend:
                    SpecialAction();
                    break;
                case InputMode.TLAbility:
                    UseAbility(0);
                    break;
                case InputMode.TRAbility:
                    UseAbility(1);
                    break;
                case InputMode.BLAbility:
                    UseAbility(2);
                    break;
                case InputMode.BRAbility:
                    UseAbility(3);
                    break;
                default:
                    Move(inputDir);
                    break;
            }

            //if (Input.GetKey("j")) Run();
            //else if (Input.GetKeyDown("l")) SpecialAction();
            //else if (Input.GetKeyDown("y")) UseAbility(0);
            //else if (Input.GetKeyDown("p")) UseAbility(1);
            //else if (Input.GetKeyDown("u")) UseAbility(2);
            //else if (Input.GetKeyDown("o")) UseAbility(3);
            //else Move(inputDir);
        }

        public virtual void RunCommand()
        {
            stamina.Tick(Time.deltaTime);

            if (stamina.Empty) sm.ChangeState(RECOVER);

            switch (inputMode)
            {
                case InputMode.Walk:
                    sm.ChangeState(MOVE);
                    break;
                case InputMode.Defend:
                    SpecialAction();
                    break;
                case InputMode.TLAbility:
                    UseAbility(0);
                    break;
                case InputMode.TRAbility:
                    UseAbility(1);
                    break;
                case InputMode.BLAbility:
                    UseAbility(2);
                    break;
                case InputMode.BRAbility:
                    UseAbility(3);
                    break;
                default:
                    Move(inputDir);
                    break;
            }

            //if (!Input.GetKey("j")) sm.ChangeState(MOVE);
            //else if (Input.GetKeyDown("l")) SpecialAction();
            //else if (Input.GetKeyDown("y")) UseAbility(0);
            //else if (Input.GetKeyDown("p")) UseAbility(1);
            //else if (Input.GetKeyDown("u")) UseAbility(2);
            //else if (Input.GetKeyDown("o")) UseAbility(3);
            //else Move(inputDir);
        }

        public virtual void ActionCommand()
        {
            if (lockOn.LockedOn) lockOn.LookAtTarget();

            switch (inputMode)
            {
                case InputMode.Defend:
                    SpecialAction();
                    break;
                case InputMode.TLAbility:
                    UseAbility(0);
                    break;
                case InputMode.TRAbility:
                    UseAbility(1);
                    break;
                case InputMode.BLAbility:
                    UseAbility(2);
                    break;
                case InputMode.BRAbility:
                    UseAbility(3);
                    break;
                default:
                    break;
            }
        }

        public virtual void RecoveryCommand()
        {
            stamina.Tick(Time.deltaTime);

            if (Stamina.Full) sm.ChangeState(MOVE);

            Move(inputDir.normalized);
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
                    act = new RollCommand(this, inputDir);
                    AddCommand(act);
                    break;
            }
        }

        public void UseAbility(int index)
        {
            index = Mathf.Abs(index);
            index = index % 4;

            var command = ability.GetAbility(index).GetCommand(this, lockOn.DirToTarget);
            if (command != null) queue.AddAction(command);
        }

        public void SetRegen(bool value)
        {
            health.Regenerative = value;
            stamina.Regenerative = value;
            poise.Regenerative = value;
        }

        public void AddAbilityCommand(int index)
        {
            var command = ability.GetAbility(index).GetCommand(this, lockOn.DirToTarget);
            if (command != null) queue.AddAction(command);
            //queue.AddAction(new AttackCommand(this, transform.forward));
        }

        public void AddCommand(BattleCommand action)
        {
            if (action != null) queue.AddAction(action);
        }

        public void NextAction()
        {
            party.ActionQueue.NextAction();
        }

        public void StopActionDeath()
        {
            party.ActionQueue.StopActionDeath();
        }

        public void StopActionStagger()
        {
            party.ActionQueue.StopActionStagger();
        }

        public void Die()
        {
            //print(89);
            party.RemovePartyMember(this);
            party.InvokeDeath();
            Destroy(gameObject);
        }

        public void LeaveStagger()
        {
            sm.ChangeState(MOVE);
        }
    }
}