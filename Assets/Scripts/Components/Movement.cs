using System.Collections;
using UnityEngine;

namespace RPG_Project
{
    [RequireComponent(typeof(CharacterController), typeof(GroundCheck))]
    public class Movement : MonoBehaviour
    {
        [Header("Linear speed")]
        [SerializeField] float walkSpeed = 3;
        [SerializeField] float runningSpeed = 8;
        [SerializeField] float combatSpeed = 3;
        [SerializeField] float currentSpeed = 0;

        [Header("Rotational speed")]
        [SerializeField] float turnTime = 0.1f;
        float turnVelocity = 0;

        [Header("Gravity")]
        [SerializeField] GroundCheck groundCheck;
        [SerializeField] bool grounded = true;
        [SerializeField] float timeSinceGrounded = 0;
        [SerializeField] Vector3 fallVelocity = new Vector3(0, 0, 0);
        [SerializeField] float terminalSpeed = 40f;
        Vector3 gravity = new Vector3(0, -9.81f, 0);
        float fallSpeed = 0;
        float sqrTerminalSpeed = 1600f;
        
        Transform cam;

        Controller controller;

        CharacterController cc;
        CapsuleCollider col;

        public float Speed
        {
            get => walkSpeed;
            set => walkSpeed = Mathf.Abs(value);
        }

        public float RunningSpeed
        {
            get => runningSpeed;
            set
            {
                if (value < walkSpeed) value = walkSpeed;
                runningSpeed = value;
            }
        }

        private void Awake()
        {
            controller = GetComponent<Controller>();

            cc = GetComponent<CharacterController>();
            col = GetComponent<CapsuleCollider>();

            groundCheck = GetComponentInChildren<GroundCheck>();

            sqrTerminalSpeed = terminalSpeed * terminalSpeed;

            cam = Camera.main.transform;
        }

        private void Update()
        {
            //Fall(Time.deltaTime);
        }

        public void SetRunning(bool value)
        {
            if (value) currentSpeed = runningSpeed;
            else currentSpeed = walkSpeed;
        }

        public void SetSpeed(ControllerMode mode)
        {
            switch (mode)
            {
                case ControllerMode.Walk:
                    currentSpeed = walkSpeed;
                    break;
                case ControllerMode.Run:
                    currentSpeed = runningSpeed;
                    break;
                case ControllerMode.Combat:
                    currentSpeed = combatSpeed;
                    break;
                default:
                    break;
            }
        }

        // Source: https://www.youtube.com/watch?v=4HpC--2iowE
        public void MovePosition(Vector3 dir, float dt)
        {
            if (dir != Vector3.zero)
            {
                dir.Normalize();

                var targetAngle = cam.eulerAngles.y + Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, 
                    ref turnVelocity, turnTime);
                cc.transform.rotation = Quaternion.Euler(0, angle, 0);

                var ds = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                cc.Move(currentSpeed * ds.normalized * dt);
            }

            Fall(dt);
        }

        void Fall(float dt)
        {
            //var onSteepSurface = false;

            cc.Move(fallVelocity * Time.deltaTime);

            grounded = groundCheck.IsGrounded(gameObject);

            if (cc.isGrounded)
            {
                if (timeSinceGrounded != 0)
                    timeSinceGrounded = 0;
                if (fallVelocity.y != 0)
                    fallVelocity.y = 0;
            }
            else
            {
                timeSinceGrounded += dt;

                // Increase downward y component of velocity
                fallSpeed += gravity.y * Time.deltaTime;
                fallVelocity += gravity * Time.deltaTime;
                if (fallVelocity.sqrMagnitude > sqrTerminalSpeed)
                    fallVelocity = terminalSpeed * fallVelocity.normalized;
            }
        }

        public IEnumerator MoveGridCo(Vector3 dir, float dt)
        {
            float t = 0;
            Vector3 startPos = controller.transform.position;
            Vector3 endPos = startPos + dir;

            while (t < 1)
            {
                t += walkSpeed * Time.deltaTime;
                if (t > 1) t = 1;
                controller.transform.position = startPos + t * dir;
                yield return null;
            }

            controller.transform.position = endPos;
        }
    }
}