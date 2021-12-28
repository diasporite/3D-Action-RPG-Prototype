using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class WeaponManager : MonoBehaviour
    {
        [Header("Weapons")]
        [SerializeField] Weapon leftWeapon;
        [SerializeField] Weapon rightWeapon;

        [Header("Parameters")]
        [SerializeField] int raycastAmmo = 10;

        [SerializeField] Weapon currentWeapon;

        private void Start()
        {
            var owner = GetComponent<Controller>();

            leftWeapon.InitWeapon(owner);
            rightWeapon.InitWeapon(owner);

            //currentWeapon = null;
            currentWeapon = rightWeapon;
        }

        public void ActivateWeapon()
        {
            currentWeapon.ActivateWeapon();
        }

        public void DeactivateWeapon()
        {
            currentWeapon.DeactivateWeapon();
        }
    }
}