using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerStaggerState : IState
    {
        Controller controller;
        StateMachine csm;

        Animator anim;
        ActionQueue queue;

        public ControllerStaggerState(Controller controller)
        {
            this.controller = controller;
            csm = controller.Sm;

            anim = controller.Anim;
            queue = controller.Queue;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            controller.Party.ActionQueue.StopAction();

            controller.Health.Regenerative = false;
            controller.Stamina.Regenerative = false;
            //controller.Poise.Regenerative = false;

            //player.Movement.StopRb();

            anim.SetTrigger("Stagger");
        }

        public void ExecuteFrame()
        {
            controller.StaggerCommand();
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            controller.Health.Regenerative = true;
            controller.Stamina.Regenerative = true;
            //controller.Poise.Regenerative = true;
        }
        #endregion
    }
}