using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyController : Controller
    {
        [SerializeField] Cooldown attackTimer = new Cooldown(3f);

        public Cooldown AttackTimer => attackTimer;

        #region StateCommands
        public override void MovementCommand()
        {
            base.MovementCommand();
        }

        public override void RunCommand()
        {
            base.RunCommand();
        }

        public override void ActionCommand()
        {
            base.ActionCommand();
        }

        public override void RecoveryCommand()
        {
            base.RecoveryCommand();
        }

        public override void StaggerCommand()
        {
            base.StaggerCommand();
        }

        public override void DeathCommand()
        {
            base.DeathCommand();
        }
        #endregion
    }
}