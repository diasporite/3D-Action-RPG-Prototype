using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Hitray : HitDetector
    {
        float range;

        float lineWidth = 0.05f;

        LineRenderer lr;

        RaycastHit[] hitInfo;

        private void Awake()
        {
            lr = GetComponent<LineRenderer>();
        }

        public void Init(Controller controller, LayerMask hittables, float range)
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

        //public void CastRay()
        //{
        //    hitInfo = Physics.RaycastAll(transform.position, transform.forward, range, hittables);
            
        //    if (hitInfo != null)
        //    {
        //        foreach (var hit in hitInfo)
        //        {
        //            var obj = hit.collider.gameObject;
        //            //print(obj);
        //            if (obj != null && obj != controller.gameObject)
        //            {
        //                //Destroy(obj);

        //                var damageable = obj.GetComponent<IDamageable>();
        //                if (damageable != null && !weaponHits.Contains(damageable))
        //                {
        //                    //print(34432);
        //                    weaponHits.Add(damageable);
        //                    damageable.OnDamage(100, controller.Combatant.Character);
        //                }
        //            }
        //        }
        //    }
        //}

        public void CastRay(Vector3 muzzlePos)
        {
            //Physics.Raycast(muzzlePos, transform.forward, out hitInfo, range, hittables);
            //hitInfo = Physics.RaycastAll(muzzlePos, transform.forward, range, hittables);
            hitInfo = Physics.RaycastAll(muzzlePos, transform.forward, range);

            StartCoroutine(ShowFireLine(muzzlePos, 0.1f));

            //if (hitInfo != null)
            //{
            //    foreach(var hit in hitInfo)
            //    {
            //        var obj = hit.collider.gameObject;
            //        //print(obj);
            //        if (obj != null && obj != controller.gameObject)
            //        {
            //            //Destroy(obj);

            //            var damageable = obj.GetComponent<IDamageable>();
            //            if (damageable != null)
            //            {
            //                //print(34432);
            //                damageable.OnDamage(100, controller.Combatant.Character);
            //            }
            //        }
            //    }
            //}
        }

        public IEnumerator ShowFireLine(Vector3 muzzlePos, float dt)
        {
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.SetPositions(new Vector3[] { muzzlePos, muzzlePos + range * transform.forward });

            lr.enabled = true;

            yield return new WaitForSeconds(dt);

            lr.enabled = false;
        }
    }
}