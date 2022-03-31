using UnityEngine;

namespace RPG_Project
{
    public class GroundCheck : MonoBehaviour
    {
        public float radius = 0.05f;
        public float skin = 0.02f;

        float slopeLimit = 45;

        Vector3 startPos;

        CharacterController cc;

        LayerMask ground;

        public bool IsGrounded => Physics.CheckSphere(transform.position, radius, ground);

        private void Awake()
        {
            cc = GetComponentInParent<CharacterController>();

            slopeLimit = cc.slopeLimit;

            startPos = transform.position + skin * Vector3.down;

            ground = LayerMask.GetMask("Ground");
        }

        private void Update()
        {
            startPos = transform.position + skin * Vector3.down;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            //Gizmos.DrawRay(startPos, skin * Vector3.down);
            Gizmos.DrawWireSphere(transform.position, radius);
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