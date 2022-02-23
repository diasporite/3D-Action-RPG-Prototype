using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class RollCommand : BattleCommand
    {
        public RollCommand(Controller controller) : base(controller)
        {
            dir = controller.transform.forward;

            actionName = "roll";
        }

        public RollCommand(Controller controller, Vector3 dir) : base(controller, dir)
        {
            if (dir == Vector3.zero) dir = controller.transform.forward;

            actionName = "roll";
        }

        public override void Execute()
        {
            controller.Mode = ControllerMode.Roll;

            dir.y = 0;
            if (dir == Vector3.zero) dir = controller.transform.forward;

            if (controller.LockOn.CurrentlyLocked)
                controller.transform.forward = controller.transform.rotation * dir.normalized;
            else controller.transform.forward = dir.normalized;

            anim.SetTrigger("SpecialAction");

            controller.Stamina.ChangeResource(-Mathf.Abs(combat.rollSpCost));
        }
    }
}