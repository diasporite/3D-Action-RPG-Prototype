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
            if (ai.State == AIState.Attack)
            {
                ai.AttackCooldown.Tick(Time.deltaTime);

                if (ai.AttackCooldown.Full)
                {
                    ai.AttackCooldown.Reset();

                    var i = Random.Range(0, 4);

                    switch (i)
                    {
                        case 0: return InputMode.TLAbility;
                        case 1: return InputMode.TRAbility;
                        case 2: return InputMode.BLAbility;
                        case 3: return InputMode.BRAbility;
                        default:
                            break;
                    }
                }
            }

            return InputMode.None;
        }

        public override Vector3 GetOutputDir1()
        {
            switch (ai.State)
            {
                case AIState.Chase:
                    return ai.PlaneDirToTarget;
                case AIState.Attack:
                    return ai.PlaneDirToTarget;
                default:
                    return Vector3.zero;
            }
        }
    }
}