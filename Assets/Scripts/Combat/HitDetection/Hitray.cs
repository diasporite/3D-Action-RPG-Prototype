using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [RequireComponent(typeof(LineRenderer))]
    public class Hitray : Hitbox
    {
        float range;

        float lineWidth = 0.05f;

        LineRenderer lr;

        RaycastHit[] hitInfo;

        Vector3 centre = new Vector3(0, 0);
        Vector3 size = new Vector3(0, 0);

        protected override void Awake()
        {
            base.Awake();

            lr = GetComponent<LineRenderer>();

            centre = col.center;
            size = col.size;
        }

        private void Update()
        {
            UpdateRay();
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

        public void CastRaybox()
        {
            centre = Vector3.zero;
            centre.z = 0.5f * range;
            size.z = range;

            col.center = centre;
            col.size = size;
        }

        public void CastRaybox(Vector3 muzzlePos)
        {
            centre = muzzlePos + col.center + 0.5f * range * transform.forward;
            size.z = range;

            col.center = centre;
            col.size = size;
        }

        public void ShowRay(bool value)
        {
            if (value)
            {
                lr.startWidth = lineWidth;
                lr.endWidth = lineWidth;
                lr.SetPositions(new Vector3[] { transform.position,
                    transform.position + range * transform.forward });

                lr.enabled = true;
            }
            else lr.enabled = false;
        }

        public void ShowRay(Vector3 muzzlePos, bool value)
        {
            if (value)
            {
                lr.startWidth = lineWidth;
                lr.endWidth = lineWidth;
                lr.SetPositions(new Vector3[] { muzzlePos, muzzlePos + range * transform.forward });

                lr.enabled = true;
            }
            else lr.enabled = false;
        }

        void UpdateRay()
        {
            if (lr.enabled)
                lr.SetPositions(new Vector3[] { transform.position,
                    transform.position + range * transform.forward });
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