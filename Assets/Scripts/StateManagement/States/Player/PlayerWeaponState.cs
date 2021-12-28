using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerWeaponState : IState
    {
        PlayerController player;
        StateMachine psm;

        WeaponHand hand;

        Animator anim;

        public PlayerWeaponState(PlayerController player, WeaponHand hand)
        {
            this.player = player;
            psm = player.Sm;

            this.hand = hand;

            anim = player.Anim;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            player.Hand = hand;

            switch (hand)
            {
                case WeaponHand.Left:
                    GameManager.instance.battleUi.ChangeButtonMenu(SkillMenuState.LeftWeaponSkills);
                    anim.SetBool("LeftWeapon", true);
                    break;
                case WeaponHand.Right:
                    GameManager.instance.battleUi.ChangeButtonMenu(SkillMenuState.RightWeaponSkills);
                    anim.SetBool("RightWeapon", true);
                    break;
            }
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

            if (player.SheatheLeft()) return;
            else if (player.SheatheRight()) return;
            else if (player.UseSkill()) return;
            else player.MovePlayer();
        }
    }
}