using UnityEngine;

namespace RPG_Project
{
    public class GroundCheck : MonoBehaviour
    {
        public float radius = 0.5f;
        public float offset = 0.01f;
        public float skin = 0.05f;

        float height = 2;
        float slopeLimit = 45;

        CharacterController cc;
        CapsuleCollider col;

        Vector3 startPos;

        private void Awake()
        {
            cc = GetComponentInParent<CharacterController>();
            col = GetComponentInParent<CapsuleCollider>();

            radius = col.radius;
            height = col.height;

            skin = cc.skinWidth;
            slopeLimit = cc.slopeLimit;

            startPos = transform.position + (0.5f * height + offset) * Vector3.down;
            //transform.position += skin * Vector3.down;
        }

        private void Update()
        {
            startPos = transform.position + (0.5f * height + offset) * Vector3.down;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            //Gizmos.DrawRay(startPos, skin * Vector3.down);
            Gizmos.DrawWireSphere(startPos, 0.5f * skin);
        }

        public bool IsGrounded(GameObject excludeObj)
        {
            //var hits = Physics.OverlapSphere(transform.position, 0.5f * skin);

            //if (hits != null)
            //    foreach (var hit in hits)
            //        if (hit.gameObject != excludeObj)
            //            return true;

            //return false;

            var hit = Physics.Raycast(startPos, skin * Vector3.down);
            return hit;
        }

        // For steep ground, angle of incline > cc slope limit
        public bool OnSteepGround()
        {
            RaycastHit hit;

            if (Physics.Raycast(startPos, 0.2f * Vector3.down, out hit))
            {
                var theta = Vector3.Angle(Vector3.down, hit.normal);
                return theta > slopeLimit;
            }

            return false;
        }

        public float GroundAngle()
        {
            RaycastHit hit;

            if (Physics.Raycast(startPos, Vector3.down, out hit))
                return Vector3.Angle(Vector3.down, hit.normal);

            return 0;
        }
    }
}