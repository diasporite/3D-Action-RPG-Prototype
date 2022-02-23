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
    }
}