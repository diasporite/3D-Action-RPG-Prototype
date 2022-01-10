using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class SwitchCommand : BattleCommand
    {
        Controller newController;

        public SwitchCommand(Controller controller, Controller newController) : base(controller)
        {
            this.newController = newController;
        }

        public override void Execute()
        {
            base.Execute();
        }
    }
}