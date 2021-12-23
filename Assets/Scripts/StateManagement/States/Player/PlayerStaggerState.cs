using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerStaggerState : IState
    {
        PlayerController player;
        StateMachine psm;

        ActionQueue queue;

        Cooldown cooldown = new Cooldown(2f, 1);

        public PlayerStaggerState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            queue = player.Queue;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            player.Stamina.Regenerative = false;
            player.Poise.Regenerative = false;

            //player.Movement.StopRb();
        }

        public void ExecuteFrame()
        {
            Tick();
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

        void Tick()
        {
            cooldown.Tick(Time.deltaTime);
            if (cooldown.Full) psm.ChangeState(player.MOVE);
        }
    }
}