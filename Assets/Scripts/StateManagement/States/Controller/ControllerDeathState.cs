using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerDeathState : IState
    {
        Controller controller;
        StateMachine csm;

        Animator anim;
        ActionQueue queue;

        public ControllerDeathState(Controller controller)
        {
            this.controller = controller;
            csm = controller.Sm;

            anim = controller.Anim;
            queue = controller.Queue;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            controller.Stamina.Regenerative = false;
            controller.Poise.Regenerative = false;

            anim.SetTrigger("Death");
        }

        public void ExecuteFrame()
        {
            controller.DeathCommand();
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