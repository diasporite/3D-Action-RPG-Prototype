using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class AICircleState : IState
    {
        AIController ai;
        StateMachine sm;

        Controller controller;
        LockOn lockOn;

        public AICircleState(AIController ai)
        {
            this.ai = ai;
            sm = ai.sm;

            lockOn = ai.GetComponent<LockOn>();
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            lockOn.TargetLock();
        }

        public void ExecuteFrame()
        {
            controller = ai.Party.CurrentController;

            ai.CalculateDistance();

            ai.ActionCooldown.Tick(Time.deltaTime);

            if (ai.SqrDist <= ai.SqrChaseDist)
            {
                if (ai.ActionCooldown.Full)
                    sm.ChangeState(AIState.Attack);
                else sm.ChangeState(AIState.Chase);
            }
            else sm.ChangeState(AIState.Idle);
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            lockOn.TargetUnlock();
        }
        #endregion
    }
}