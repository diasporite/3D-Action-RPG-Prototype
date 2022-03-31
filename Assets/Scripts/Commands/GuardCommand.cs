using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GuardCommand : BattleCommand
    {
        public GuardCommand(Controller controller) : base(controller)
        {
            actionName = "guard";
        }

        public override void Execute()
        {
            controller.State = ControllerState.Guard;

            anim.SetTrigger("SpecialAction");

            controller.Stamina.ChangeResource(-Mathf.Abs(combat.guardSpCost));
        }
    }
}