using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyInputController : InputController
    {
        EnemyAIController ai;

        protected override void Awake()
        {
            base.Awake();

            ai = GetComponent<EnemyAIController>();
        }

        public override InputMode GetInput()
        {
            return base.GetInput();
        }

        public override Vector3 GetOutputDir1()
        {
            return base.GetOutputDir1();
        }
    }
}