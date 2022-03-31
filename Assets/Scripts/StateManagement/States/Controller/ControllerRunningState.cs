using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerRunningState : IState
    {
        Controller controller;
        StateMachine csm;

        Animator anim;
        Movement movement;

        public ControllerRunningState(Controller controller)
        {
            this.controller = controller;
            csm = controller.Sm;

            anim = controller.Anim;
            movement = controller.Movement;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            controller.State = ControllerState.Run;
            controller.Health.Regenerative = false;
            controller.Stamina.CurrentRegen = GameManager.instance.combat.staminaRunRegen;

            movement.SetRunning(true);
            controller.Stamina.Run(true);

            anim.SetBool("Run", true);
        }

        public void ExecuteFrame()
        {
            controller.RunCommand();
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

            controller.Stamina.Run(false);
            anim.SetBool("Run", false);
        }
        #endregion

        void Command()
        {
            //controller.ResourceTick(Time.deltaTime);

            //if (controller.Stamina.Empty)
            //    csm.ChangeState(controller.RECOVER);
            //else if (!Input.GetKey("j"))
            //    csm.ChangeState(controller.MOVE);
            //else if (controller.UseSkill()) return;
            //else if (controller.SpecialAction()) return;
            //else controller.MovePlayer();
        }

        void Tick()
        {
            controller.Health.Tick(Time.deltaTime);
            controller.Stamina.Tick(Time.deltaTime);
            controller.Poise.Tick(Time.deltaTime);
        }
    }
}