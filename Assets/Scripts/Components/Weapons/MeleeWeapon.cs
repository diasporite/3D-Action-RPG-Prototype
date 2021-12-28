using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class MeleeWeapon : Weapon
    {
        Hitbox hitbox;

        public override Hitbox Hitbox => hitbox;

        protected override void Awake()
        {
            hitbox = GetComponentInChildren<Hitbox>();
        }

        public override void InitWeapon(Controller owner)
        {
            base.InitWeapon(owner);

            hitbox.SetController(owner);
        }

        public override void ActivateWeapon()
        {
            hitbox.SetActive(true);
            print("act");
        }

        public override void DeactivateWeapon()
        {
            hitbox.SetActive(false);
            print("deact");
        }
    }
}