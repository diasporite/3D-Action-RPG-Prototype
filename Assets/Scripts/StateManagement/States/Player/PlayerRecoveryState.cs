using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerRecoveryState : IState
    {
        PlayerController player;
        StateMachine psm;

        Movement movement;

        Animator anim;

        public PlayerRecoveryState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            movement = player.Movement;

            anim = player.Anim;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            player.Stamina.Run(false);
            player.Movement.SetRunning(false);

            anim.SetTrigger("Recovery");
        }

        public void ExecuteFrame()
        {
            Command();
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

        void Command()
        {
            player.ResourceTick(Time.deltaTime);

            if (player.Stamina.Full)
            {
                psm.ChangeState(player.MOVE);
                return;
            }

            Move();
        }

        void Move()
        {
            var dir = player.RawInputDir;
            player.Move(dir);
        }
    }
}