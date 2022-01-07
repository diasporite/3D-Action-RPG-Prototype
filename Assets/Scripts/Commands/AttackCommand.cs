using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class AttackCommand : BattleCommand
    {
        [SerializeField] string trigger;

        public AttackCommand(Controller controller, Vector3 dir, string trigger) : base(controller, dir)
        {
            anim = controller.Anim;

            actionName = "attack";
            this.trigger = trigger;
        }

        public AttackCommand(Ability ability, Controller controller, Vector3 dir, string trigger) : base(controller, dir)
        {
            anim = controller.Anim;

            this.ability = ability;

            actionName = ability.Trigger;
            this.trigger = trigger;
        }

        public AttackCommand(string name, Controller controller, Vector3 dir, string trigger) : base(controller, dir)
        {
            anim = controller.Anim;

            actionName = name;
            this.trigger = trigger;
        }

        public override void Execute()
        {
            anim.SetTrigger(trigger);

            controller.Stamina.ChangeResource(-ability.SpCost);
            //controller.Poise.ChangeResource(-47);
        }

        public override IEnumerator ExecuteCo()
        {
            controller.Stamina.ChangeResource(-10);
            //controller.Poise.ChangeResource(-47);

            // Continually track direction to target
            // Animation will move instigator in that direction

            yield return new WaitForSeconds(0.7f);

            canProgress = true;
            complete = true;
        }
    }
}