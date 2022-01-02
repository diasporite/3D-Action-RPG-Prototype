using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerActionState : IState
    {
        PlayerController player;
        StateMachine psm;

        ActionQueue queue;
        WeaponManager weapon;

        public PlayerActionState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            queue = player.Queue;
            weapon = player.Weapon;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            player.Stamina.Regenerative = false;
            player.Poise.Regenerative = false;

            weapon.CanDisarm = false;
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
            player.Stamina.Regenerative = true;
            player.Poise.Regenerative = true;

            weapon.CanDisarm = true;
        }
        #endregion

        void Command()
        {
            //if (!queue.Executing) psm.ChangeState(player.MOVE);

            if (player.UseSkill()) return;
            //else if (player.SheatheLeft()) return;
            //else if (player.SheatheRight()) return;
            else player.MovePlayer();
        }
    }
}