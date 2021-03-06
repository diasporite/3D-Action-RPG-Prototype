using System.Collections;
using UnityEngine;

namespace RPG_Project
{
    [RequireComponent(typeof(CharacterController), typeof(GroundCheck))]
    public class Movement : MonoBehaviour
    {
        bool isPlayer = false;

        [SerializeField] bool lockedLinear = false;
        [SerializeField] bool lockedRotation = false;

        [SerializeField] float distanceTravelled = 0;
        [SerializeField] float timeSinceStationary = 0;

        [SerializeField] Vector3 ds;

        [Header("Linear speed")]
        [SerializeField] float walkSpeed = 3;
        [SerializeField] float runningSpeed = 6;
        [SerializeField] float currentSpeed = 0;

        [Header("Rotational speed")]
        [SerializeField] float turnTime = 0.1f;
        float turnVelocity = 0;

        [Header("Gravity")]
        [SerializeField] GroundCheck groundCheck;
        [SerializeField] bool grounded = true;
        [SerializeField] float timeSinceGrounded = 0;
        [SerializeField] float fallDamageTime = 0;
        [SerializeField] Vector3 velocity = new Vector3(0, 0, 0);
        [SerializeField] float jumpHeight = 3f;
        [SerializeField] float terminalSpeed = 40f;
        Vector3 gravity = new Vector3(0, -9.81f, 0);
        float fallSpeed = 0;
        float sqrTerminalSpeed = 1600f;

        [Header("Fall Damage")]
        [SerializeField] float fallDamageThreshold = -3f;
        [SerializeField] float damageSpeed = 35;
        
        Transform cam;

        Controller controller;
        LockOn lockOn;

        CharacterController cc;
        CapsuleCollider col;

        CombatDatabase combat;

        public bool LockedLinear => lockedLinear;
        public bool LockedRotation => lockedRotation;

        public Vector3 Ds => ds;

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

        public bool Stationary => velocity.sqrMagnitude <= 0.12f;

        float FallDamagePercent => 10 + damageSpeed * fallDamageTime;

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
            //if (Input.GetKeyDown("space")) Jump();
            FallPosition(Time.deltaTime);
        }

        public void InitMovement(bool isPlayer)
        {
            //controller = GetComponent<Controller>();

            //cc = GetComponent<CharacterController>();
            //col = GetComponent<CapsuleCollider>();

            //groundCheck = GetComponentInChildren<GroundCheck>();

            //cam = Camera.main.transform;
            combat = GameManager.instance.Combat;
            lockOn = controller.Party.LockOn;

            this.isPlayer = isPlayer;

            distanceTravelled = 0;

            sqrTerminalSpeed = terminalSpeed * terminalSpeed;

            velocity = Vector3.zero;
        }

        public void SetRunning(bool value)
        {
            if (value) currentSpeed = runningSpeed;
            else currentSpeed = walkSpeed;
        }

        public void SetSpeed(ControllerState mode)
        {
            switch (mode)
            {
                case ControllerState.Walk:
                    currentSpeed = walkSpeed;
                    break;
                case ControllerState.Run:
                    currentSpeed = runningSpeed;
                    break;
                default:
                    break;
            }
        }

        public void LockPosition()
        {
            lockedLinear = true;
        }

        public void LockRotation()
        {
            lockedRotation = true;
        }

        public void UnlockPosition()
        {
            lockedLinear = false;
        }

        public void UnlockRotation()
        {
            lockedRotation = false;
        }

        #region PositionBasedMovement
        // Source: https://www.youtube.com/watch?v=4HpC--2iowE
        public void MovePosition(Vector3 dir, float dt)
        {
            if (!lockedLinear)
            {
                if (lockOn.CurrentlyLocked) MovePositionLocked(dir, dt, lockOn.CurrentTargetPos);
                else MovePositionFree(dir, dt);

                //if (lockOn.LockedOn) transform.LookAt(lockOn.CurrentTarget.transform);
                //if (lockOn.LockedOn) lockOn.LookAtTarget(transform);
            }
        }

        void MovePositionFree(Vector3 dir, float dt)
        {
            var angle = 0f;
            var targetAngle = 0f;

            if (dir.sqrMagnitude > 1) dir.Normalize();

            targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            if (isPlayer) targetAngle += cam.eulerAngles.y;

            if (dir != Vector3.zero)
            {
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                    ref turnVelocity, turnTime);
                cc.transform.rotation = Quaternion.Euler(0, angle, 0);
            }

            ds = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            ds.y -= Mathf.Sin(groundCheck.GroundAngle() * Mathf.Deg2Rad);

            if (dir != Vector3.zero)
            {
                cc.Move(currentSpeed * ds.normalized * Time.deltaTime);
                distanceTravelled += currentSpeed * Time.deltaTime;
                controller.Health.Tick(Time.deltaTime);
            }
        }

        void MovePositionLocked(Vector3 dir, float dt, Vector3 centre)
        {
            if (dir.sqrMagnitude > 1) dir.Normalize();

            var dsr = dir.z * transform.forward;
            var dsy = -transform.up * Mathf.Sin(groundCheck.GroundAngle() * Mathf.Deg2Rad);
            var dst = dir.x * transform.right;

            ds = dsr + dsy + dst;

            if (dir != Vector3.zero)
            {
                cc.Move(currentSpeed * ds.normalized * Time.deltaTime);
                distanceTravelled += currentSpeed * Time.deltaTime;
                controller.Health.Tick(Time.deltaTime);
            }

            lockOn.LookAtTarget(transform);
        }

        void FallPosition(float dt)
        {
            //var onSteepSurface = false;

            cc.Move(velocity * Time.deltaTime);

            grounded = groundCheck.IsGrounded;
            //grounded = cc.isGrounded;

            // Rewrite
            if (grounded)
            {
                if (fallDamageTime > 0)
                    GetComponent<Combatant>().ApplyFallDamage(FallDamagePercent);

                if (timeSinceGrounded != 0)
                    timeSinceGrounded = 0;
                if (fallDamageTime != 0)
                    fallDamageTime = 0;
                if (velocity.y != 0)
                    velocity.y = 0;
            }
            else
            {
                timeSinceGrounded += dt;

                // Increase downward y component of velocity
                fallSpeed += gravity.y * dt;
                velocity += gravity * dt;
                if (velocity.sqrMagnitude > sqrTerminalSpeed)
                    velocity = terminalSpeed * velocity.normalized;

                if (velocity.y < fallDamageThreshold) fallDamageTime += dt;
            }
        }
        #endregion

        #region VelocityBasedMovement
        public void MoveVelocity(Vector3 dir)
        {
            if (!lockedLinear && dir != Vector3.zero)
            {
                dir.Normalize();

                var targetAngle = cam.eulerAngles.y + Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                    ref turnVelocity, turnTime);
                cc.transform.rotation = Quaternion.Euler(0, angle, 0);

                var ds = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                ds.y -= Mathf.Sin(groundCheck.GroundAngle() * Mathf.Deg2Rad);

                cc.SimpleMove(currentSpeed * ds.normalized);
            }

            FallVelocity();
        }

        void FallVelocity()
        {
            //var onSteepSurface = false;

            cc.SimpleMove(velocity);

            //grounded = groundCheck.IsGrounded(gameObject);
            grounded = cc.isGrounded;

            if (grounded)
            {
                if (timeSinceGrounded >= fallDamageThreshold)
                    GetComponent<Health>().ChangeResourcePercent(-FallDamagePercent);

                if (timeSinceGrounded != 0)
                    timeSinceGrounded = 0;
                if (velocity.y != 0)
                    velocity.y = 0;
            }
            else
            {
                timeSinceGrounded += Time.deltaTime;

                // Increase downward y component of velocity
                fallSpeed += gravity.y * Time.deltaTime;
                velocity += gravity * Time.deltaTime;
                if (velocity.sqrMagnitude > sqrTerminalSpeed)
                    velocity = terminalSpeed * velocity.normalized;
            }
        }

        // Source: https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
        public void Jump()
        {
            if (cc.isGrounded)
            {
                velocity.y += Mathf.Sqrt(-2f * jumpHeight * gravity.y);
            }
        }
        #endregion

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