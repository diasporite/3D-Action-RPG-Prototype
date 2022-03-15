using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum AIState
    {
        Empty = 0,

        Idle = 1,
        Chase = 2,
        Attack = 3,
        Circle = 4,
    }

    public class AIController : MonoBehaviour
    {
        [SerializeField] protected AIState state = AIState.Empty;

        [SerializeField] protected InputMode inputMode;

        [SerializeField] protected Transform target;
        [SerializeField] protected Vector3 dirToTarget;

        [SerializeField] protected float chaseDist = 8f;
        [SerializeField] protected float attackDist = 2f;
        [SerializeField] protected float innerAttackDist = 1.2f;
        [SerializeField] protected float outerAttackDist = 2f;
        [SerializeField] protected float actionDelay = 3f;

        [SerializeField] protected Cooldown actionCooldown = new Cooldown(1f);

        float sqrChaseDist;
        float sqrAttackDist;
        float sqrInnerAtkDist;
        float sqrOuterAtkDist;
        float sqrDist;

        protected PartyManager party;

        public readonly StateMachine sm = new StateMachine();

        public AIState State => state;

        public InputMode InputMode
        {
            get => inputMode;
            set => inputMode = value;
        }

        public Transform Target => target;

        public Vector3 PlaneTargetPos
        {
            get
            {
                var pos = target.position;
                pos.y = transform.position.y;
                return pos;
            }
        }

        public Vector3 DirToTarget => dirToTarget;

        public Vector3 PlaneDirToTarget
        {
            get
            {
                var dir = dirToTarget;
                dir.y = 0;
                return dir;
            }
        }

        // Accounts for camera rotation
        public Vector3 AbsPlaneDirToTarget
        {
            get
            {
                var dir = PlaneDirToTarget;
                var forward = party.CurrentCombatant.transform.forward;

                dir.y = 0;
                forward.y = 0;

                return party.CurrentCombatant.transform.rotation * dir;
            }
        }

        public Cooldown ActionCooldown => actionCooldown;

        public float SqrChaseDist => sqrChaseDist;
        public float SqrAttackDist => sqrAttackDist;
        public float SqrInnerAtkDist => sqrInnerAtkDist;
        public float SqrOuterAtkDist => sqrOuterAtkDist;
        public float SqrDist => sqrDist;

        public PartyManager Party => party;

        private void Awake()
        {
            sqrChaseDist = chaseDist * chaseDist;
            sqrAttackDist = attackDist * attackDist;
            sqrInnerAtkDist = innerAttackDist * innerAttackDist;
            sqrOuterAtkDist = outerAttackDist * outerAttackDist;

            actionCooldown = new Cooldown(actionDelay);

            party = GetComponent<PartyManager>();
        }

        protected virtual void Start()
        {
            InitSM();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDist);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, PlaneDirToTarget);

            if (party != null)
            {
                if (party.CurrentCombatant != null)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(transform.position, party.CurrentCombatant.transform.forward);
                }
            }
        }

        protected void InitSM()
        {
            sm.AddState(AIState.Idle, new AIIdleState(this));
            sm.AddState(AIState.Chase, new AIChaseState(this));
            sm.AddState(AIState.Attack, new AIAttackState(this));
            sm.AddState(AIState.Circle, new AICircleState(this));

            sm.ChangeState(AIState.Idle);
        }

        public void UpdateSM()
        {
            sm.Update();
            state = (AIState)sm.GetCurrentKey;
        }

        public void CalculateDistance()
        {
            dirToTarget = target.position - party.CurrentController.transform.position;

            sqrDist = dirToTarget.sqrMagnitude;
        }

        public void LookAtTarget()
        {
            var pos = target.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);
        }

        public void FillActionCooldown(float delta)
        {
            actionCooldown.Count += delta;
        }
    }
}