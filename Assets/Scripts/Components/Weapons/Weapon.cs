using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Weapon : MonoBehaviour
    {
        Hitbox hitbox;

        public Hitbox Hitbox => hitbox;

        private void Awake()
        {
            hitbox = GetComponentInChildren<Hitbox>();
        }

        public void ActivateWeapon()
        {
            hitbox.SetActive(true);
            print("act");
        }

        public void DeactivateWeapon()
        {
            hitbox.SetActive(false);
            print("deact");
        }
    }
}