using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Weapon : MonoBehaviour
    {
        protected Controller owner;

        [SerializeField] WeaponID id;

        protected virtual void Awake()
        {

        }

        public virtual void InitWeapon(Controller owner)
        {
            this.owner = owner;
        }

        public virtual void ActivateWeapon()
        {

        }

        public virtual void DeactivateWeapon()
        {

        }
    }
}