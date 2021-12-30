using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class LockOn : MonoBehaviour
    {
        bool lockedOn;
        [SerializeField] float searchRadius = 5f;

        int currentTarget = 0;
        [SerializeField] Target[] targets;

        Target ownTarget;

        private void Awake()
        {
            ownTarget = GetComponentInChildren<Target>();
        }

        public void FindTargets()
        {
            Target[] foundTargets;
            var hits = Physics.OverlapSphere(transform.position, searchRadius, 
                LayerMask.GetMask("Targets"));

            if (hits != null)
            {
                foundTargets = new Target[hits.Length];
                for (int i = 0; i < hits.Length; i++)
                    foundTargets[i] = hits[i].GetComponent<Target>();
                targets = foundTargets;
            }
        }
    }
}