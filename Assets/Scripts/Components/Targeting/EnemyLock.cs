using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyLock : LockOn
    {
        protected override void Awake()
        {
            base.Awake();

            targetMask = LayerMask.GetMask("Targets");
        }

        public override void LockOntoTarget()
        {
            base.LockOntoTarget();
        }

        public override void UnlockFromTarget()
        {
            base.UnlockFromTarget();
        }
    }
}