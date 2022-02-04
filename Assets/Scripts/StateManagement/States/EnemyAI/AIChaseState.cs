﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class AIChaseState : IState
    {
        AIController ai;
        StateMachine sm;

        Controller controller;

        public AIChaseState(AIController ai)
        {
            this.ai = ai;
            sm = ai.sm;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {

        }

        public void ExecuteFrame()
        {
            controller = ai.Party.CurrentPartyMember;

            ai.CalculateDistance();

            if (ai.SqrDist <= ai.SqrChaseDist)
            {
                if (ai.SqrDist <= ai.SqrAttackDist)
                    sm.ChangeState(AIState.Attack);
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

        }
        #endregion
    }
}