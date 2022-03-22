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

        int abilityIndex;

        //public AttackCommand(Controller controller, Vector3 dir, string trigger) : base(controller, dir)
        //{
        //    anim = controller.Anim;

        //    actionName = "attack";
        //    this.trigger = trigger;
        //}

        public AttackCommand(Controller controller, Vector3 dir, string trigger, int abilityIndex) : base(controller, dir)
        {
            anim = controller.Anim;

            lockOn = controller.LockOn;

            this.abilityIndex = abilityIndex;
            ability = controller.Ability.GetAbility(abilityIndex);

            actionName = ability.Trigger;
            this.trigger = trigger;
        }

        public override void Execute()
        {
            controller.State = ControllerState.Action;

            controller.Ability.SetAbility(abilityIndex);

            //controller.transform.LookAt(controller.transform.position - dir);
            if (lockOn.CurrentlyLocked) controller.LockOn.LookAtTarget();

            anim.SetTrigger(trigger);

            controller.Stamina.ChangeResource(-ability.SpCost);
            //controller.Poise.ChangeResource(-47);

            ability.UseResource(-1);
            controller.Party.InvokeAbilityUse(abilityIndex);
        }

        public override IEnumerator ExecuteCo()
        {
            controller.State = ControllerState.Action;

            anim.SetTrigger(trigger);

            controller.Stamina.ChangeResource(-ability.SpCost);
            //controller.Poise.ChangeResource(-47);

            ability.UseResource(1);

            // Continually track direction to target
            // Animation will rotate instigator in that direction
            if (lockOn.CurrentlyLocked) controller.LockOn.LookAtTarget();

            yield return new WaitForSeconds(ability.action.animation.length);

            canProgress = true;
            complete = true;
        }
    }
}