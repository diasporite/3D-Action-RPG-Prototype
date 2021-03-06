using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerActionState : IState
    {
        Controller controller;
        StateMachine csm;

        ActionQueue queue;
        AbilityManager weapon;
        LockOn lockOn;

        public ControllerActionState(Controller controller)
        {
            this.controller = controller;
            csm = controller.Sm;

            queue = controller.Queue;
            weapon = controller.Ability;
            lockOn = controller.LockOn;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            controller.Health.Regenerative = false;
            controller.Stamina.Regenerative = false;

            // Rotation will be locked during animation
            controller.Movement.LockPosition();
        }

        public void ExecuteFrame()
        {
            controller.ActionCommand();
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            controller.Movement.UnlockPosition();
            controller.Movement.UnlockRotation();
        }
        #endregion

        void Command()
        {
            //if (!queue.Executing) psm.ChangeState(player.MOVE);

            //if (controller.UseSkill()) return;
            //else if (controller.SpecialAction()) return;
            //else player.MovePlayer();
        }
    }
}