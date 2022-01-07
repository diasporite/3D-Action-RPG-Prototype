using System.Collections;
using UnityEngine;

namespace RPG_Project
{
    [RequireComponent(typeof(CharacterController), typeof(GroundCheck))]
    public class Movement : MonoBehaviour
    {
        bool locked = false;

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

        CharacterController cc;
        CapsuleCollider col;

        CombatManager combat;

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

        private void Start()
        {
            combat = GameManager.instance.Combat;

            GameManager.instance.Party.onCharacterChanged += SetSpeeds;

            SetSpeeds(null);

            velocity = Vector3.zero;
        }

        private void Update()
        {
            //if (Input.GetKeyDown("space")) Jump();
        }

        private void OnDestroy()
        {
            //GameManager.instance.Party.onCharacterChanged -= SetSpeeds;
        }

        public void SetSpeeds(BattleChar character)
        {
            var weight = WeightClass.Middleweight;

            if (character != null)
                weight = character.Weightclass;

            switch (weight)
            {
                case WeightClass.Lightweight:
                    walkSpeed = combat.lightweightWalk;
                    runningSpeed = combat.lightweightRun;
                    break;
                case WeightClass.Middleweight:
                    walkSpeed = combat.middleweightWalk;
                    runningSpeed = combat.middleweightRun;
                    break;
                case WeightClass.Heavyweight:
                    walkSpeed = combat.heavyweightWalk;
                    runningSpeed = combat.heavyweightRun;
                    break;
            }
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
                default:
                    break;
            }
        }

        public void LockMovement()
        {
            locked = true;
        }

        public void UnlockMovement()
        {
            locked = false;
        }

        #region PositionBasedMovement
        // Source: https://www.youtube.com/watch?v=4HpC--2iowE
        public void MovePosition(Vector3 dir, float dt)
        {
            if (!locked && dir != Vector3.zero)
            {
                dir.Normalize();

                var targetAngle = cam.eulerAngles.y + Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, 
                    ref turnVelocity, turnTime);
                cc.transform.rotation = Quaternion.Euler(0, angle, 0);

                var ds = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                ds.y -= Mathf.Sin(groundCheck.GroundAngle() * Mathf.Deg2Rad);

                cc.Move(currentSpeed * ds.normalized * dt);
            }

            FallPosition(dt);
        }

        void FallPosition(float dt)
        {
            //var onSteepSurface = false;

            cc.Move(velocity * Time.deltaTime);

            //grounded = groundCheck.IsGrounded(gameObject);
            grounded = cc.isGrounded;

            if (grounded)
            {
                if (fallDamageTime > 0)
                    GetComponent<Health>().ChangeResourcePercent(-FallDamagePercent);

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
            if (!locked && dir != Vector3.zero)
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