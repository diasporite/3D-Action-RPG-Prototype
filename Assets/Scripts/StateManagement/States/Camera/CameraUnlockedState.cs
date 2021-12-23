using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraUnlockedState : IState
    {
        CameraController camera;
        StateMachine csm;

        public CameraUnlockedState(CameraController camera)
        {
            this.camera = camera;
            csm = camera.Sm;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {

        }

        public void ExecuteFrame()
        {
            camera.RotateCamera();
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {
            camera.FollowPlayer();
        }

        public void Exit()
        {

        }
        #endregion
    }
}