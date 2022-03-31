using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyAIController : AIController
    {
        protected override void Start()
        {
            target = FindObjectOfType<PlayerInputController>().transform;

            base.Start();
        }
    }
}