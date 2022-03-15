using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class LockOn : MonoBehaviour
    {
        public event Action onLock;

        public readonly string LOCKED = "locked";
        public readonly string FREE = "free";

        [SerializeField] protected bool active = false;
        [SerializeField] protected bool currentlyLocked = false;

        [SerializeField] protected float searchRadius = 5f;
        [SerializeField] protected float searchFrequency = 10f;
        protected Cooldown searchCooldown;

        [SerializeField] protected int currentTarget = 0;
        [SerializeField] protected List<Target> targets = new List<Target>();

        protected PartyManager party;

        protected ThirdPersonCameraController cam;

        protected LayerMask targetMask;

        public bool Active => active;
        public bool CurrentlyLocked => currentlyLocked;

        public Vector3 LastTrackedPosition { get; private set; }

        public float SqrSearchRadius => searchRadius * searchRadius;

        public Controller Controller => party.CurrentController;

        public Target OwnTarget => Controller.GetComponentInChildren<Target>();

        public Target CurrentTarget
        {
            get
            {
                if (targets.Count > 0) return targets[currentTarget];
                return null;
            }
        }

        public Vector3 CurrentTargetPos
        {
            get
            {
                if (CurrentTarget != null) return CurrentTarget.transform.position;
                return transform.position;
            }
        }

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

        public ThirdPersonCameraController Cam => cam;

        protected virtual void Awake()
        {
            searchCooldown = new Cooldown(1 / searchFrequency);

            party = GetComponent<PartyManager>();

            targetMask = LayerMask.GetMask("Targets");
        }

        protected virtual void Update()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }

        public virtual void InitLockOn()
        {
            cam.lockOn = this;
            cam.follow = party.CurrentController.transform;
        }

        public virtual void ToggleLock()
        {

        }

        public virtual void UpdateLock()
        {
            if (active)
            {
                if (targets.Count <= 0) UnlockCamera();

                searchCooldown.Tick(Time.deltaTime);

                if (searchCooldown.Full)
                {
                    FindTargets();
                    searchCooldown.Reset();
                }
            }
        }

        public virtual void SelectTarget()
        {

        }

        public void FindTargets()
        {
            if (Controller == null) return;

            foreach (var t in targets.ToArray())
            {
                if (t == null) targets.Remove(t);
                else
                {
                    var dist = Vector3.Distance(transform.position, t.transform.position);
                    if (dist > searchRadius) targets.Remove(t);
                }
            }

            var hits = Physics.OverlapSphere(transform.position, searchRadius);

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
        }

        protected void FindClosestTarget()
        {
            if (targets.Count <= 0) UnlockCamera();
            else
            {
                // Determine initial target to lock to by finding the target closest
                //   to the controller's line of sight (dot product closest to 1)
                var dots = new float[targets.Count];
                var max = 0f;
                var index = 0;

                for (int i = 0; i < targets.Count; i++)
                {
                    var dir = targets[i].transform.position - Controller.transform.position;
                    dots[i] = Vector3.Dot(transform.forward, dir.normalized);
                }

                for (int i = 0; i < dots.Length; i++)
                {
                    if (dots[i] > max)
                    {
                        max = dots[i];
                        index = i;
                    }
                }

                currentTarget = index;

                //if (currentTarget > targets.Count) currentTarget = targets.Count - 1;
            }
        }

        public virtual void LookAtTarget()
        {
            if (Controller == null || CurrentTarget == null) return;

            var lookPos = CurrentTarget.transform.position;
            lookPos.y = Controller.transform.position.y;

            Controller.transform.LookAt(lookPos);
            LastTrackedPosition = lookPos;
        }

        public virtual void LookAtTarget(Transform subject)
        {
            if (CurrentTarget != null)
            {
                var lookPos = CurrentTarget.transform.position;
                lookPos.y = subject.position.y;

                subject.LookAt(lookPos);
                LastTrackedPosition = lookPos;
            }
        }

        public void LockCamera()
        {
            currentlyLocked = true;
            if (cam!=null) cam.Sm.ChangeState(cam.LOCKED);
        }

        public void UnlockCamera()
        {
            currentlyLocked = false;
            if (cam!=null) cam.Sm.ChangeState(cam.UNLOCKED);
        }

        public virtual void TargetLock()
        {

        }

        public virtual void TargetUnlock()
        {

        }
    }
}