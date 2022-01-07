using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerRunningState : IState
    {
        PlayerController player;
        StateMachine psm;

        Animator anim;
        Movement movement;

        public PlayerRunningState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            anim = player.Anim;
            movement = player.Movement;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            movement.SetRunning(true);
            player.Stamina.Run(true);

            anim.SetBool("Run", true);
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
            player.Stamina.Run(false);
            anim.SetBool("Run", false);
        }
        #endregion

        void Command()
        {
            player.ResourceTick(Time.deltaTime);

            if (player.Stamina.Empty)
                psm.ChangeState(player.RECOVER);
            else if (!Input.GetKey("j"))
                psm.ChangeState(player.MOVE);
            else if (player.UseSkill()) return;
            else if (player.SpecialAction()) return;
            else player.MovePlayer();
        }

        void Tick()
        {
            player.Health.Tick(Time.deltaTime);
            player.Stamina.Tick(Time.deltaTime);
            player.Poise.Tick(Time.deltaTime);
        }
    }
}