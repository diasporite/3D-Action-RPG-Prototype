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

        LayerMask hittables;

        List<IDamageable> weaponHits = new List<IDamageable>();

        private void Awake()
        {
            col = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            hittables = LayerMask.GetMask("Combatants", "Destructibles");
        }

        private void Update()
        {
            //DetectCollisonOverlap();
        }

        public void SetActive(bool value)
        {
            active = value;
            col.enabled = active;

            if (active) weaponHits.Clear();
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
                    0.5f * col.size, Quaternion.identity, hittables);
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
                var obj = collider.gameObject;

                if (obj != null && obj != controller.gameObject)
                {
                    print(obj);
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