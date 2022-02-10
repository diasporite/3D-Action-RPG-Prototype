using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraController : MonoBehaviour
    {
        public readonly string UNLOCKED = "unlocked";
        public readonly string LOCKED = "locked";

        public Transform follow;

        public readonly StateMachine sm = new StateMachine();

        private void Start()
        {
            InitSM();
        }

        private void LateUpdate()
        {
            sm.UpdateLate();
        }

        void InitSM()
        {

        }
    }
}