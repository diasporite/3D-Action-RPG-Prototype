using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class AttackCommand : BattleCommand
    {
        [SerializeField] string trigger;

        LockOn lockOn;

        //public AttackCommand(Controller controller, Vector3 dir, string trigger) : base(controller, dir)
        //{
        //    anim = controller.Anim;

        //    actionName = "attack";
        //    this.trigger = trigger;
        //}

        public AttackCommand(Ability ability, Controller controller, Vector3 dir, string trigger) : base(controller, dir)
        {
            anim = controller.Anim;

            lockOn = controller.LockOn;

            this.ability = ability;

            actionName = ability.Trigger;
            this.trigger = trigger;
        }

        public override void Execute()
        {
            controller.Mode = ControllerMode.Action;

            controller.Ability.SetCurrentAbility(ability);

            //controller.transform.LookAt(controller.transform.position - dir);
            if (lockOn.LockedOn) controller.LockOn.LookAtTarget();

            anim.SetTrigger(trigger);

            controller.Stamina.ChangeResource(-ability.SpCost);
            //controller.Poise.ChangeResource(-47);

            ability.UseResource(1);
        }

        public override IEnumerator ExecuteCo()
        {
            controller.Mode = ControllerMode.Action;

            anim.SetTrigger(trigger);

            controller.Stamina.ChangeResource(-ability.SpCost);
            //controller.Poise.ChangeResource(-47);

            ability.UseResource(1);

            // Continually track direction to target
            // Animation will rotate instigator in that direction
            if (lockOn.LockedOn) controller.LockOn.LookAtTarget();

            yield return new WaitForSeconds(ability.action.animation.length);

            canProgress = true;
            complete = true;
        }
    }
}