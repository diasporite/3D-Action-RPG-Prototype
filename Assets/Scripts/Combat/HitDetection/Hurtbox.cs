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
        Controller controller;
        Combatant combatant;

        Destructible destructible;

        public HurtboxState state;

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

            var multiplier = 1f;
            var hitter = other.GetComponent<HitDetector>();

            // Get instigator info from hitter
            //print(controller);

            switch (state)
            {
                case HurtboxState.Resist:
                    multiplier = 0.7f;
                    break;
                case HurtboxState.Weak:
                    multiplier = 1.4f;
                    break;
            }

            if (combatant != null)
                combatant.OnDamage(Mathf.RoundToInt(multiplier * 50), combatant.Character);
            else if (destructible != null)
                destructible.OnDamage(Mathf.RoundToInt(multiplier * 50), null);
        }
    }
}