using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerMovementState : IState
    {
        Controller controller;
        StateMachine csm;

        Animator anim;

        Movement movement;

        public ControllerMovementState(Controller controller)
        {
            this.controller = controller;
            csm = controller.Sm;

            anim = controller.Anim;

            movement = controller.Movement;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            controller.State = ControllerState.Walk;

            controller.Health.Regenerative = true;
            controller.Stamina.Regenerative = true;

            controller.Stamina.CurrentRegen = GameManager.instance.combat.staminaRegen;

            movement.SetRunning(false);
        }

        public void ExecuteFrame()
        {
            controller.MovementCommand();
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

        //void Command()
        //{
        //    player.ResourceTick(Time.deltaTime);

        //    if (player.Run()) return;
        //    else if (player.UseSkill()) return;
        //    else if (player.SpecialAction()) return;
        //    else player.MovePlayer();
        //}

        void Tick()
        {
            controller.Health.Tick(Time.deltaTime);
            controller.Stamina.Tick(Time.deltaTime);
            controller.Poise.Tick(Time.deltaTime);
        }
    }
}