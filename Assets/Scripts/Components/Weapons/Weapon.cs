using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Weapon : MonoBehaviour
    {
        protected Controller owner;
        protected AbilityManager ability;

        [SerializeField] WeaponID id;

        protected virtual void Awake()
        {

        }

        public virtual void InitWeapon(Controller owner, LayerMask hittables)
        {
            this.owner = owner;
            ability = owner.Ability;
        }

        public virtual void ActivateWeapon()
        {

        }

        public virtual void DeactivateWeapon()
        {

        }
    }
}