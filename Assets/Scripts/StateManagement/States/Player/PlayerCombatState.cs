using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerCombatState : IState
    {
        PlayerController player;
        StateMachine psm;

        WeaponManager weapon;
        Animator anim;

        WeaponHand hand;

        public PlayerCombatState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            weapon = player.Weapon;
            anim = player.Anim;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {

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

            //if (player.SheatheLeft()) return;
            //else if (player.SheatheRight()) return;
            if (player.UseSkill()) return;
            else player.MovePlayer();
        }
    }
}