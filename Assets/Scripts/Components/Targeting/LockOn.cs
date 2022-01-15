using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class LockOn : MonoBehaviour
    {
        public readonly string LOCKED = "locked";
        public readonly string FREE = "free";

        [SerializeField] string currentState;

        [SerializeField] float searchRadius = 5f;

        [SerializeField] int currentTarget = 0;
        [SerializeField] Target[] targets = new Target[] { };

        PartyManager party;

        CameraController cam;

        LayerMask targetMask;

        public readonly StateMachine sm = new StateMachine();

        public bool LockedOn { get; set; }

        public Controller Controller => party.CurrentPartyMember;

        public Target OwnTarget => Controller.GetComponentInChildren<Target>();

        public Target CurrentTarget
        {
            get
            {
                if (targets.Length > 0) return targets[currentTarget];
                return null;
            }
        }

        public Vector3 CurrentTargetPos => CurrentTarget.transform.position;

        public Vector3 DirToTarget
        {
            get
            {
                var ds = Vector3.zero;
                var y = transform.position.y;

                if (CurrentTarget != null)
                {
                    ds = CurrentTargetPos - transform.position;
                    ds.y = 0;
                    return ds;
                }
                return transform.forward;
            }
        }

        public CameraController Cam => cam;

        private void Awake()
        {
            party = GetComponent<PartyManager>();

            cam = Camera.main.GetComponent<CameraController>();

            targetMask = LayerMask.GetMask("Targets");
        }

        private void Start()
        {
            cam.lockOn = this;

            InitSM();

            sm.ChangeState(FREE);
        }

        private void Update()
        {
            sm.Update();
            currentState = sm.GetCurrentKey.ToString();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }

        void InitSM()
        {
            sm.AddState(LOCKED, new LockOnTargetState(this));
            sm.AddState(FREE, new LockOnFreeState(this));
        }

        public void LockOntoTarget()
        {
            if (Input.GetKeyDown("m"))
            {
                FindTargets();
                if (targets.Length > 0) sm.ChangeState(LOCKED);
            }
        }

        public void UnlockFromTarget()
        {
            if (CurrentTarget == null || Input.GetKeyDown("m"))
                sm.ChangeState(FREE);
        }

        public void FindTargets()
        {
            List<Target> foundTargets = new List<Target>();
            var hits = Physics.OverlapSphere(Controller.transform.position, searchRadius);

            if (hits != null)
            {
                foreach(var hit in hits)
                {
                    var target = hit.GetComponentInChildren<Target>();
                    if (target != null && target != OwnTarget && !foundTargets.Contains(target))
                        foundTargets.Add(target);
                }
            }

            targets = foundTargets.ToArray();
        }

        public void CheckTargets()
        {
            if (targets.Length <= 0) sm.ChangeState(FREE);
        }

        public void LookAtTarget()
        {
            var lookPos = CurrentTarget.transform.position;
            lookPos.y = Controller.transform.position.y;

            Controller.transform.LookAt(lookPos);
            //Controller.transform.rotation = Quaternion.Euler(0, Controller.transform.eulerAngles.y, 0);
        }

        public void LookAtTarget(Transform subject)
        {
            var lookPos = CurrentTarget.transform.position;
            lookPos.y = subject.position.y;

            subject.LookAt(lookPos);
        }
    }
}