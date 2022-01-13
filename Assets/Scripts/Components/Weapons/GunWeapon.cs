using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GunWeapon : Weapon
    {
        Hitray ray;

        [SerializeField] float range = 5f;

        protected override void Awake()
        {
            base.Awake();

            ray = GetComponent<Hitray>();
        }

        public override void InitWeapon(Controller owner)
        {
            base.InitWeapon(owner);
        }

        public override void ActivateWeapon()
        {
            ray.CastRay();
        }
    }
}