using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerStaggerState : IState
    {
        PlayerController player;
        StateMachine psm;

        Animator anim;
        ActionQueue queue;

        public PlayerStaggerState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            anim = player.Anim;
            queue = player.Queue;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            player.Stamina.Regenerative = false;
            player.Poise.Regenerative = false;

            //player.Movement.StopRb();

            anim.SetBool("Stagger", true);
        }

        public void ExecuteFrame()
        {

        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            player.Stamina.Regenerative = true;
            player.Poise.Regenerative = true;
        }
        #endregion
    }
}