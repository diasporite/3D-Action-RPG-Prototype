using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GunWeapon : Weapon
    {
        Hitray ray;

        [SerializeField] float range = 5f;
        [SerializeField] Transform muzzle;

        protected override void Awake()
        {
            base.Awake();

            ray = GetComponent<Hitray>();

            muzzle.gameObject.SetActive(false);
        }

        public override void InitWeapon(Controller owner, LayerMask hittables)
        {
            base.InitWeapon(owner, hittables);

            ray.Init(owner, hittables, 10f);
        }

        public override void ActivateWeapon()
        {
            print("aw");
            ray.CastRay(muzzle.position);
            //ability.ChangeGunAmmo(-1);

            //StartCoroutine(MuzzleFlashCo());
        }

        public void ActivateMuzzleFlash()
        {
            muzzle.gameObject.SetActive(true);
        }

        public void DeactivateMuzzleFlash()
        {
            muzzle.gameObject.SetActive(false);
        }

        IEnumerator MuzzleFlashCo()
        {
            print(3);
            muzzle.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.1f);

            muzzle.gameObject.SetActive(false);
        }
    }
}