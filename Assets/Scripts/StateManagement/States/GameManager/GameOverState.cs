using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GameOverState : IState
    {
        GameManager game;
        StateMachine sm;

        public GameOverState(GameManager game)
        {
            this.game = game;
            sm = game.sm;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            game.Ui.gameOverScreen.StartCoroutine(game.Ui.gameOverScreen.GameOverCo());
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