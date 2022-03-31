using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class TeleportCommand : BattleCommand
    {
        float teleportDistance = 3f;

        public TeleportCommand(Controller controller, Vector3 dir) : base(controller, dir)
        {

        }

        public override void Execute()
        {
            base.Execute();
        }
    }
}