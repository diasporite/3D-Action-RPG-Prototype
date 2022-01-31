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

        float deathTime = 1;
        Cooldown death;

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

            deathTime = anim.GetCurrentAnimatorStateInfo(0).length;
            Debug.Log(deathTime + "s");
            death = new Cooldown(deathTime);
        }

        public void ExecuteFrame()
        {
            controller.DeathCommand();
            death.Tick(Time.deltaTime);
            if (death.Full) controller.Die();
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