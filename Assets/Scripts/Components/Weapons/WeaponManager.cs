using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] Weapon weapon;

        private void Start()
        {
            weapon.Hitbox.SetController(GetComponent<Controller>());
        }

        public void ActivateWeapon()
        {
            weapon.ActivateWeapon();
        }

        public void DeactivateWeapon()
        {
            weapon.DeactivateWeapon();
        }
    }
}