using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerLock : LockOn
    {
        protected override void Awake()
        {
            base.Awake();

            cam = Camera.main.GetComponent<ThirdPersonCameraController>();
        }

        protected override void Update()
        {
            UpdateLock();

            ToggleLock();

            SelectTarget();
        }

        public override void ToggleLock()
        {
            if (party.CurrentController != null)
            {
                if (Input.GetKeyDown("m"))
                {
                    active = !active;

                    if (active)
                    {
                        FindTargets();
                        searchCooldown.Reset();

                        if (targets.Count > 0)
                        {
                            FindClosestTarget();
                            LockCamera();
                        }
                        else UnlockCamera();
                    }
                    else UnlockCamera();
                }
            }
        }

        public override void SelectTarget()
        {
            if (party.CurrentController != null)
            {
                if (Input.GetKeyDown("left")) currentTarget--;
                else if (Input.GetKeyDown("right")) currentTarget++;

                if (currentTarget < 0) currentTarget = targets.Count - 1;
                if (currentTarget >= targets.Count) currentTarget = 0;

                if (targets.Count > 0)
                {
                    if (CurrentTarget != null)
                    {
                        cam.LockedTarget = CurrentTarget;
                        cam.target = CurrentTarget.transform;
                        LookAtTarget(CurrentTarget.transform);
                    }
                }
            }
        }
    }
}