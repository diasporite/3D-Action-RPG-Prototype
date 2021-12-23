using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerCombatState : IState
    {
        PlayerController player;
        StateMachine psm;

        Animator anim;

        Movement movement;
        Stamina stamina;

        public PlayerCombatState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            anim = player.Anim;

            movement = player.Movement;
            stamina = player.Stamina;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            anim.SetBool("InCombat", true);

            movement.SetRunning(false);
            stamina.Run(false);
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
            //anim.SetBool("InCombat", false);
        }
        #endregion

        void Command()
        {
            player.ResourceTick(Time.deltaTime);

            if (Input.GetKeyDown("u")) psm.ChangeState(player.MOVE);
            // Select skill
            else if (player.Attack()) return;
            else Move();
        }

        void Move()
        {
            var dir = player.RawInputDir;
            player.Move(dir);
        }
    }
}