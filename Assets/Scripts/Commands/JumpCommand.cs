using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class JumpCommand : BattleCommand
    {
        public JumpCommand(Controller controller) : base(controller)
        {
            actionName = "jump";
        }

        public override void Execute()
        {
            controller.State = ControllerState.Jump;

            anim.SetTrigger("SpecialAction");

            controller.Stamina.ChangeResource(-Mathf.Abs(combat.jumpSpCost));
        }
    }
}