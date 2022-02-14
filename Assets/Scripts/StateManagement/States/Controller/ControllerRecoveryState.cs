using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerRecoveryState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;

        Animator anim;

        public ControllerRecoveryState(Controller controller)
        {
            this.controller = controller;
            csm = controller.Sm;

            movement = controller.Movement;

            anim = controller.Anim;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            controller.Stamina.Run(false);
            controller.Movement.SetRunning(false);

            anim.SetBool("Recovery", true);
        }

        public void ExecuteFrame()
        {
            controller.RecoveryCommand();
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            anim.SetBool("Recovery", false);
        }
        #endregion

        void Command()
        {
            controller.Stamina.Tick(Time.deltaTime);

            if (controller.Stamina.Full)
            {
                csm.ChangeState(controller.MOVE);
                return;
            }

            Move();
        }

        void Move()
        {
            var dir = controller.RawInputDir;
            controller.Move(dir);
        }
    }
}