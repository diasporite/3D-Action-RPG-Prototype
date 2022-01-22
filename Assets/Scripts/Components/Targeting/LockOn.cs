﻿using System.Collections;
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
        [SerializeField] List<Target> targets = new List<Target>();

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
                if (targets.Count > 0) return targets[currentTarget];
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
                if (targets.Count > 0) sm.ChangeState(LOCKED);
            }
        }

        public void UnlockFromTarget()
        {
            if (targets.Count <= 0 || Input.GetKeyDown("m"))
                sm.ChangeState(FREE);
        }

        public void FindTargets()
        {
            foreach (var t in targets.ToArray())
            {
                if (t == null) targets.Remove(t);
                else
                {
                    var dist = Vector3.Distance(transform.position, t.transform.position);
                    if (dist > searchRadius) targets.Remove(t);
                }
            }

            var hits = Physics.OverlapSphere(Controller.transform.position, searchRadius);

            if (hits != null)
            {
                foreach (var hit in hits)
                {
                    var target = hit.GetComponentInChildren<Target>();
                    if (target != null && target != OwnTarget)
                        if (!targets.Contains(target))
                            targets.Add(target);
                }
            }

            if (currentTarget > targets.Count) currentTarget = targets.Count - 1;
        }

        public void CheckTargets()
        {
            if (targets.Count <= 0) sm.ChangeState(FREE);
        }

        public void SelectTarget()
        {
            if (Input.GetKeyDown("left")) currentTarget--;
            else if (Input.GetKeyDown("right")) currentTarget++;

            if (currentTarget < 0) currentTarget = targets.Count - 1;
            if (currentTarget >= targets.Count) currentTarget = 0;

            if (targets.Count > 0)
            {
                cam.LockedTarget = CurrentTarget;
                cam.target = CurrentTarget.transform;
                LookAtTarget(CurrentTarget.transform);
            }
        }

        public void LookAtTarget()
        {
            if (CurrentTarget == null) return;

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