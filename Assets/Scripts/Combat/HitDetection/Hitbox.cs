using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class HitData
    {
        [SerializeField] Combatant combatant;
        [SerializeField] int basePower;

        public Combatant Combatant => combatant;

        public HitData(Combatant combatant, int basePower)
        {
            this.combatant = combatant;
            this.basePower = basePower;
        }
    }

    public class Hitbox : MonoBehaviour
    {
        protected Controller controller;
        protected AbilityManager abilities;

        protected LayerMask hittables;

        protected Skill actionSkill;

        protected HitData data;

        [SerializeField] protected List<Hurtbox> hits = new List<Hurtbox>();
        protected List<IDamageable> weaponHits = new List<IDamageable>();

        protected BoxCollider col;

        public Controller Controller => controller;
        public HitData Data => data;

        public bool AlreadyHit(Hurtbox hurtbox) => hits.Contains(hurtbox);

        protected virtual void Awake()
        {
            col = GetComponent<BoxCollider>();
        }

        public void Init(Controller controller, LayerMask hittables)
        {
            this.controller = controller;
            this.hittables = hittables;
        }

        public void AddHit(Hurtbox hit)
        {
            //if (hit != null) hits.Add(hit);
        }

        public void ClearHits()
        {
            hits.Clear();
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