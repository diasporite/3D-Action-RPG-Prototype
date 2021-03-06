using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraUnlockedState : IState
    {
        ThirdPersonCameraController camera;
        StateMachine csm;

        public CameraUnlockedState(ThirdPersonCameraController camera)
        {
            this.camera = camera;
            csm = camera.Sm;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            camera.reticle.gameObject.SetActive(false);
            //camera.transform.LookAt(camera.lastViewedPosition);
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