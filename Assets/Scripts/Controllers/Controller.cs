using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum ControllerMode
    {
        Walk = 0,
        Run = 1,
        Combat = 2,
        Action = 3,
        Stagger = 4,
    }

    [RequireComponent(typeof(Movement), typeof(Combatant), typeof(ActionQueue))]
    public class Controller : MonoBehaviour
    {
        public readonly string MOVE = "move";
        public readonly string RUN = "run";
        public readonly string RECOVER = "recover";
        public readonly string STAGGER = "stagger";
        public readonly string ACTION = "action";
        public readonly string COMBAT = "combat";

        Vector3 inputDir = new Vector3(0, 0);

        protected Animator anim;

        protected Movement movement;
        protected Combatant combatant;
        protected ActionQueue queue;

        protected Health health;
        protected Stamina stamina;
        protected Poise poise;

        protected StateMachine sm = new StateMachine();

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

        public Health Health => health;
        public Stamina Stamina => stamina;
        public Poise Poise => poise;

        public StateMachine Sm => sm;

        private void Awake()
        {
            anim = GetComponent<Animator>();

            movement = GetComponent<Movement>();
            combatant = GetComponent<Combatant>();
            queue = GetComponent<ActionQueue>();

            health = GetComponent<Health>();
            stamina = GetComponent<Stamina>();
            poise = GetComponent<Poise>();
        }

        protected virtual void Start()
        {
            InitSM();
        }

        protected virtual void Update()
        {
            sm.Update();
        }

        protected virtual void OnDrawGizmos()
        {
            
        }

        protected virtual void InitSM()
        {

        }

        public virtual void Move(Vector3 dir)
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
    }
}