using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class RollCommand : BattleCommand
    {
        public RollCommand(Controller controller) : base(controller)
        {
            actionName = "roll";
        }

        public RollCommand(Controller controller, Vector3 dir) : base(controller, dir)
        {
            actionName = "roll";
        }

        public override void Execute()
        {
            //controller.transform.LookAt(controller.transform.position + dir, Vector3.up);

            anim.SetTrigger("SpecialAction");

            controller.Stamina.ChangeResource(-Mathf.Abs(combat.rollSpCost));
        }
    }
}