using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum HurtboxState
    {
        Normal = 0,
        Resist = 1,
        Weak = 2,
    }

    public class Hurtbox : MonoBehaviour
    {
        [SerializeField] bool weakpoint = false;

        public HurtboxState state;

        Controller controller;
        Combatant combatant;

        Destructible destructible;

        public void Init(Controller controller)
        {
            this.controller = controller;
            combatant = controller.Combatant;
        }

        public void Init(Destructible destructible)
        {
            this.destructible = destructible;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root == transform.root) return;

            Ability ability;
            var multiplier = 1f;

            var hitter = other.GetComponent<Hitbox>();
            if (hitter != null)
                ability = hitter.Controller.Ability.CurrentAbility;
            else ability = null;

            if (hitter != null)
            {
                //if (!hitter.AlreadyHit(this))
                //{
                if (weakpoint) multiplier = 1.4f;

                //hitter.AddHit(this);

                // Get instigator info from hitter

                switch (state)
                {
                    case HurtboxState.Resist:
                        multiplier = 0.7f;
                        break;
                    case HurtboxState.Weak:
                        multiplier = 1.4f;
                        break;
                }

                print(ability.skill.SkillName);

                if (combatant != null)
                    combatant.OnDamage(multiplier, ability, hitter.Controller.Combatant);
                else if (destructible != null)
                    destructible.OnDamage(multiplier, ability, null);
                //}
            }
        }
    }
}