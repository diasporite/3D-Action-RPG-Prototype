using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyLock : LockOn
    {
        AIController ai;

        public Transform enemyTarget;

        public Transform EnemyTarget
        {
            get => enemyTarget;
            set => enemyTarget = value;
        }

        protected override void Awake()
        {
            base.Awake();

            targetMask = LayerMask.GetMask("Targets");

            ai = GetComponent<AIController>();
        }

        public override void ToggleLock()
        {
            active = !active;
            currentlyLocked = !currentlyLocked;
        }

        public override void TargetLock()
        {
            enemyTarget = ai.Target;

            active = true;
            currentlyLocked = true;
        }

        public override void TargetUnlock()
        {
            active = false;
            currentlyLocked = false;
        }

        public override void LookAtTarget()
        {
            var pos = enemyTarget.position;
            pos.y = transform.position.y;

            transform.LookAt(pos);
        }

        public override void LookAtTarget(Transform subject)
        {
            var pos = enemyTarget.position;
            pos.y = subject.position.y;

            subject.LookAt(pos);
        }
    }
}