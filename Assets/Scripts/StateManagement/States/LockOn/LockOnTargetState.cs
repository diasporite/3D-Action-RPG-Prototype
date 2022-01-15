using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class LockOnTargetState : IState
    {
        LockOn lockOn;
        StateMachine sm;

        Controller controller;

        CameraController cam;

        public LockOnTargetState(LockOn lockOn)
        {
            this.lockOn = lockOn;
            sm = lockOn.sm;

            controller = lockOn.Controller;

            cam = lockOn.Cam;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            lockOn.LockedOn = true;

            cam.LockedTarget = lockOn.CurrentTarget;
            cam.Sm.ChangeState(cam.LOCKED);

            lockOn.LookAtTarget();
        }

        public void ExecuteFrame()
        {
            lockOn.UnlockFromTarget();

            lockOn.FindTargets();
            lockOn.CheckTargets();
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {
            //controller.transform.LookAt(lockOn.CurrentTarget.transform);
        }

        public void Exit()
        {

        }
        #endregion
    }
}