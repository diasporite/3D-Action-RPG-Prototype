using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerController : Controller
    {
        protected override void Start()
        {
            base.Start();

            sm.ChangeState(MOVE);
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, 2f * transform.forward);
        }

        protected override void InitSM()
        {
            sm.AddState(MOVE, new PlayerMovementState(this));
            sm.AddState(RUN, new PlayerRunningState(this));
            sm.AddState(RECOVER, new PlayerRecoveryState(this));
            sm.AddState(STAGGER, new PlayerStaggerState(this));
            sm.AddState(ACTION, new PlayerActionState(this));
            sm.AddState(COMBAT, new PlayerCombatState(this));
        }

        public bool Run()
        {
            if (Input.GetKey("j") /*&& !movement.Stationary && !stamina.Empty*/)
            {
                sm.ChangeState(RUN);
                return true;
            }

            return false;
        }

        public bool Attack()
        {
            if (Input.GetKeyDown("i"))
            {
                AddCommand(0);
                return true;
            }

            return false;
        }
    }
}