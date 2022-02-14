using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Hitbox : HitDetector
    {
        BoxCollider col;

        List<IDamageable> weaponHits = new List<IDamageable>();

        private void Awake()
        {
            col = GetComponent<BoxCollider>();
        }

        public void Init(Controller controller, LayerMask hittables)
        {
            this.controller = controller;
            this.hittables = hittables;
        }

        //void DetectCollisonOverlap()
        //{
        //    if (active)
        //    {
        //        var hits = Physics.OverlapBox(transform.position + col.center,
        //            0.5f * col.size, Quaternion.identity, hittables);
        //        if (hits != null)
        //        {
        //            print(hits.Length);
        //            foreach (var hit in hits)
        //            {
        //                var obj = hit.gameObject;
        //                print(obj);
        //                if (obj != null && obj != gameObject && obj != controller.gameObject)
        //                {
        //                    Destroy(obj);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void OnTriggerEnter(Collider collider)
        //{
        //    if (active)
        //    {
        //        var obj = collider.gameObject;

        //        if (obj != null && obj != controller.gameObject)
        //        {
        //            print(obj);
        //            //Destroy(obj);

        //            var damageable = obj.GetComponent<IDamageable>();
        //            if (damageable != null && !weaponHits.Contains(damageable))
        //            {
        //                //print(34432);
        //                weaponHits.Add(damageable);
        //                damageable.OnDamage(100, controller.Combatant.Character);
        //            }
        //        }
        //    }
        //}
    }
}