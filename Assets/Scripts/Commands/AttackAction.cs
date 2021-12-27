using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class AttackAction : BattleAction
    {
        Animator anim;

        public AttackAction(Controller controller, Vector3 dir) : base(controller, dir)
        {
            anim = controller.Anim;
        }

        public override void Execute()
        {
            anim.SetTrigger("SwordSwing");

            controller.Stamina.ChangeResource(-7);
            controller.Poise.ChangeResource(-47);
        }

        public override IEnumerator ExecuteCo()
        {
            controller.Stamina.ChangeResource(-10);
            controller.Poise.ChangeResource(-47);

            // Continually track direction to target
            // Animation will move instigator in that direction

            yield return new WaitForSeconds(0.7f);

            canProgress = true;
            complete = true;
        }
    }
}