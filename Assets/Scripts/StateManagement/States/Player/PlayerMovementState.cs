﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerMovementState : IState
    {
        PlayerController player;
        StateMachine psm;

        Animator anim;

        Movement movement;

        public PlayerMovementState(PlayerController player)
        {
            this.player = player;
            psm = player.Sm;

            anim = player.Anim;

            movement = player.Movement;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            movement.SetRunning(false);

            player.Hand = WeaponHand.Empty;
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

            if (player.Run()) return;
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