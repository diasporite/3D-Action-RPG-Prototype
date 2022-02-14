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

            targetMask = LayerMask.GetMask("Targets");
        }

        public override void LockOntoTarget()
        {
            if (Input.GetKeyDown("m"))
            {
                FindTargets();
                if (targets.Count > 0) sm.ChangeState(LOCKED);
            }
        }

        public override void UnlockFromTarget()
        {

        }
    }
}