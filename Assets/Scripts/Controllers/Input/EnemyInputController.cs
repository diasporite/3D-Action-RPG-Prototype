using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyInputController : InputController
    {
        AIController ai;

        protected override void Awake()
        {
            base.Awake();

            ai = GetComponent<EnemyAIController>();
        }

        public override InputMode GetInput()
        {
            return ai.InputMode;
        }

        public override Vector3 GetOutputDir1()
        {
            switch (ai.State)
            {
                case AIState.Chase:
                    //return ai.AbsPlaneDirToTarget;
                    return ai.PlaneDirToTarget.normalized;
                case AIState.Attack:
                    //return ai.AbsPlaneDirToTarget;
                    return ai.PlaneDirToTarget.normalized;
                case AIState.Circle:
                    return transform.right;
                default:
                    return Vector3.zero;
            }
        }
    }
}