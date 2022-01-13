using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class LockOnFreeState : IState
    {
        LockOn lockOn;
        StateMachine sm;

        CameraController cam;

        public LockOnFreeState(LockOn lockOn)
        {
            this.lockOn = lockOn;
            sm = lockOn.sm;

            cam = lockOn.Cam;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            lockOn.LockedOn = false;

            cam.Sm.ChangeState(cam.UNLOCKED);
        }

        public void ExecuteFrame()
        {
            lockOn.LockOntoTarget();
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