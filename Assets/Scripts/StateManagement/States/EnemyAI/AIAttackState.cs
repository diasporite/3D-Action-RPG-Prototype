using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class AIAttackState : IState
    {
        AIController ai;
        StateMachine sm;

        Controller controller;

        public AIAttackState(AIController ai)
        {
            this.ai = ai;
            sm = ai.sm;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            // Attack
        }

        public void ExecuteFrame()
        {
            controller = ai.Party.CurrentController;

            ai.CalculateDistance();

            //ai.AttackCooldown.Tick(Time.deltaTime);

            //if (ai.AttackCooldown.Full)
            //{
            //    // Attack
            //    ai.AttackCooldown.Reset();
            //}
            //else
            //{
                if (ai.SqrDist <= ai.SqrChaseDist)
                {
                    if (ai.SqrDist > ai.SqrAttackDist)
                        sm.ChangeState(AIState.Chase);
                }
                else sm.ChangeState(AIState.Idle);
            //}
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {

        }
        #endregion
    }
}