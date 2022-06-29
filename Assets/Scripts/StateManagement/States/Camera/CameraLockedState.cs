using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraLockedState : IState
    {
        ThirdPersonCameraController camera;
        StateMachine csm;

        public CameraLockedState(ThirdPersonCameraController camera)
        {
            this.camera = camera;
            csm = camera.Sm;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            camera.reticle.gameObject.SetActive(true);
        }

        public void ExecuteFrame()
        {

        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {
            camera.FollowTarget();
            camera.ReticleFollow();
        }

        public void Exit()
        {
            camera.thetaXz = 0;
        }
        #endregion
    }
}