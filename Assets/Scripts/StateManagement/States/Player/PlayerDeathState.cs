using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerDeathState : IState
    {
        PlayerController player;
        StateMachine psm;

        Animator anim;

        public PlayerDeathState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            anim = player.Anim;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            anim.SetTrigger("Death");
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

        }
        #endregion
    }
}