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

        Cooldown delay = new Cooldown(1f);

        public AIAttackState(AIController ai)
        {
            this.ai = ai;
            sm = ai.sm;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            delay.Reset();

            //ai.LookAtTarget();

            ai.ActionCooldown.Reset();

            var i = Random.Range(0, 4);

            switch (i)
            {
                case 0:
                    ai.InputMode = InputMode.TLAbility;
                    break;
                case 1:
                    ai.InputMode = InputMode.TRAbility;
                    break;
                case 2:
                    ai.InputMode = InputMode.BLAbility;
                    break;
                case 3:
                    ai.InputMode= InputMode.BRAbility;
                    break;
                default:
                    break;
            }
        }

        public void ExecuteFrame()
        {
            controller = ai.Party.CurrentController;

            ai.CalculateDistance();

            delay.Tick(Time.deltaTime);

            if (delay.Full)
            {
                if (ai.SqrDist <= ai.SqrChaseDist)
                {
                    if (ai.SqrDist > ai.SqrAttackDist)
                        sm.ChangeState(AIState.Chase);
                    else sm.ChangeState(AIState.Circle);
                }
                else sm.ChangeState(AIState.Idle);
            }
            else ai.InputMode = InputMode.None;
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            ai.InputMode = InputMode.None;
        }
        #endregion
    }
}