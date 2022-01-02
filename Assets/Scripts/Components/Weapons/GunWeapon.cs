using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GunWeapon : Weapon
    {
        [SerializeField] float range = 5f;

        public override void ActivateWeapon()
        {
            base.ActivateWeapon();
        }

        public override void DeactivateWeapon()
        {
            base.DeactivateWeapon();
        }
    }
}