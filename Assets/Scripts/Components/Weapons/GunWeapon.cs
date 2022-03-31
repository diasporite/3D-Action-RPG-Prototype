using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GunWeapon : Weapon
    {
        [SerializeField] Hitray ray;

        [SerializeField] float range = 10f;
        [SerializeField] Transform muzzle;

        protected override void Awake()
        {
            base.Awake();

            ray = GetComponentInChildren<Hitray>();

            muzzle.gameObject.SetActive(false);
        }

        public override void InitWeapon(Controller owner, LayerMask hittables)
        {
            base.InitWeapon(owner, hittables);

            ray.Init(owner, hittables, range);

            DeactivateWeapon();
        }

        public override void ActivateWeapon()
        {
            print("aw");
            ray.GetComponent<Collider>().enabled = true;
            ray.CastRaybox();
            ray.ClearHits();

            StartCoroutine(MuzzleFlashCo());
        }

        public override void DeactivateWeapon()
        {
            ray.GetComponent<Collider>().enabled = false;
            ray.ClearHits();
        }

        public override void ShowRay()
        {
            ray.ShowRay(true);
        }

        public override void HideRay()
        {
            ray.ShowRay(false);
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