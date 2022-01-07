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

        CameraController cam;

        private void Awake()
        {
            ownTarget = GetComponentInChildren<Target>();
        }

        private void Start()
        {
            cam = Camera.main.GetComponent<CameraController>();
            cam.lockOn = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown("right shift")) ToggleLock();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }

        public void ToggleLock()
        {
            print("toggle");
            FindTargets();

            if (targets != null && targets.Length > 0)
            {
                cam.LockedTargets = targets;
                if ((string)cam.Sm.GetCurrentKey != cam.LOCKED) cam.Sm.ChangeState(cam.LOCKED);
            }
            else
            {
                cam.LockedTargets = null;
                cam.Sm.ChangeState(cam.UNLOCKED);
            }
        }

        public void FindTargets()
        {
            List<Target> foundTargets = new List<Target>();
            var hits = Physics.OverlapSphere(transform.position, searchRadius);

            if (hits != null)
            {
                foreach(var hit in hits)
                {
                    var target = hit.GetComponentInChildren<Target>();
                    if (target != null) foundTargets.ToArray();
                }
            }

            targets = foundTargets.ToArray();
        }
    }
}