using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] Hitbox hitbox;

        protected override void Awake()
        {
            //hitbox = GetComponentInChildren<Hitbox>();
        }

        public override void InitWeapon(Controller owner, LayerMask hittables)
        {
            base.InitWeapon(owner, hittables);

            hitbox.Init(owner, hittables);

            DeactivateWeapon();
        }

        public override void ActivateWeapon()
        {
            hitbox.GetComponent<Collider>().enabled = true;
            hitbox.ClearHits();

            print("act");
        }

        public override void DeactivateWeapon()
        {
            hitbox.ClearHits();
            hitbox.GetComponent<Collider>().enabled = false;

            print("deact");
        }
    }
}