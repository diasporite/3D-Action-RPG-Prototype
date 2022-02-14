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

            hitbox.gameObject.SetActive(false);
        }

        public override void ActivateWeapon()
        {
            hitbox.gameObject.SetActive(true);
            //hitbox.SetActive(true);
            print("act");
        }

        public override void DeactivateWeapon()
        {
            hitbox.gameObject.SetActive(false);
            //hitbox.SetActive(false);
            print("deact");
        }
    }
}