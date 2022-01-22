using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] protected Transform target;

        public readonly StateMachine sm = new StateMachine();

        protected virtual void Start()
        {
            InitSM();
        }

        private void Update()
        {
            sm.Update();
        }

        protected void InitSM()
        {

        }
    }
}