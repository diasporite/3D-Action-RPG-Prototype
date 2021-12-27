using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] bool active = false;
        BoxCollider col;

        Controller controller;

        List<IDamageable> weaponHits = new List<IDamageable>();

        private void Awake()
        {
            col = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            //DetectCollisonOverlap();
        }

        public void SetActive(bool value)
        {
            active = value;
            col.enabled = active;
        }

        public void SetController(Controller controller)
        {
            this.controller = controller;
        }

        void DetectCollisonOverlap()
        {
            if (active)
            {
                var hits = Physics.OverlapBox(transform.position + col.center,
                    0.5f * col.size, Quaternion.identity, LayerMask.GetMask("Enemies"));
                if (hits != null)
                {
                    print(hits.Length);
                    foreach (var hit in hits)
                    {
                        var obj = hit.gameObject;
                        print(obj);
                        if (obj != null && obj != gameObject && obj != controller.gameObject)
                        {
                            Destroy(obj);
                        }
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (active)
            {
                //var damageable = collision.gameObject.GetComponent<IDamageable>();

                //// Also check that damageable isn't that of the weapon user
                //if (damageable != null)
                //{
                //    if (!weaponHits.Contains(damageable))
                //    {

                //    }
                //}

                var obj = collider.gameObject;
                print(obj);
                if (obj != null && obj != controller.gameObject)
                {
                    Destroy(obj);
                }
            }
        }
    }
}