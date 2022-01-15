using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Hitray : MonoBehaviour
    {
        float range;

        LayerMask hittables;

        List<IDamageable> weaponHits = new List<IDamageable>();

        LineRenderer lr;

        RaycastHit[] hitInfo;

        Controller controller;

        private void Awake()
        {
            lr = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            hittables = LayerMask.GetMask("Enemies", "Destructibles");
        }

        public void InitHitray(Controller controller, LayerMask hittables, float range)
        {
            this.controller = controller;
            this.hittables = hittables;
            this.range = range;

            lr.enabled = false;
        }

        public void SetController(Controller controller)
        {
            this.controller = controller;
        }

        public void SetRange(float range)
        {
            this.range = range;
        }

        public void CastRay()
        {
            hitInfo = Physics.RaycastAll(transform.position, transform.forward, range, hittables);

            if (hitInfo != null)
            {
                foreach (var hit in hitInfo)
                {
                    var obj = hit.collider.gameObject;
                    //print(obj);
                    if (obj != null && obj != controller.gameObject)
                    {
                        //Destroy(obj);

                        var damageable = obj.GetComponent<IDamageable>();
                        if (damageable != null && !weaponHits.Contains(damageable))
                        {
                            //print(34432);
                            weaponHits.Add(damageable);
                            damageable.OnDamage(100, controller.Combatant.Character);
                        }
                    }
                }
            }
        }

        public void CastRay(Vector3 muzzlePos)
        {
            //Physics.Raycast(muzzlePos, transform.forward, out hitInfo, range, hittables);
            hitInfo = Physics.RaycastAll(muzzlePos, transform.forward, range, hittables);

            if (hitInfo != null)
            {
                foreach(var hit in hitInfo)
                {
                    var obj = hit.collider.gameObject;
                    //print(obj);
                    if (obj != null && obj != controller.gameObject)
                    {
                        //Destroy(obj);

                        var damageable = obj.GetComponent<IDamageable>();
                        if (damageable != null && !weaponHits.Contains(damageable))
                        {
                            //print(34432);
                            weaponHits.Add(damageable);
                            damageable.OnDamage(100, controller.Combatant.Character);
                        }
                    }
                }
            }
        }
    }
}