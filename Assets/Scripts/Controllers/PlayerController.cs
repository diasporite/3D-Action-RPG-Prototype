using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG_Project
{
    public class PlayerController : Controller
    {
        public InputMaster controls;

        protected override void Awake()
        {
            base.Awake();

            controls = new InputMaster();
        }

        protected override void Start()
        {
            base.Start();

            sm.ChangeState(MOVE);
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, 2f * transform.forward);
        }

        protected override void InitSM()
        {
            sm.AddState(MOVE, new PlayerMovementState(this));
            sm.AddState(RUN, new PlayerRunningState(this));
            sm.AddState(RECOVER, new PlayerRecoveryState(this));
            sm.AddState(STAGGER, new PlayerStaggerState(this));
            sm.AddState(ACTION, new PlayerActionState(this));
        }

        public bool Run()
        {
            //if (Input.GetKey("j") /*&& !movement.Stationary && !stamina.Empty*/)
            if (controls.PlayerMove.Run.triggered)
            {
                sm.ChangeState(RUN);
                return true;
            }

            return false;
        }

        public bool Attack()
        {
            if (Input.GetKeyDown("u") || Input.GetKeyDown("o") || 
                Input.GetKeyDown("y") || Input.GetKeyDown("p"))
            //if (inputManager.upSkill.GetInput || inputManager.leftSkill.GetInput || 
            //    inputManager.rightSkill.GetInput || inputManager.downSkill.GetInput)
            {
                AddCommand(0);
                return true;
            }

            return false;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var dir = context.ReadValue<Vector2>();
            var ds = new Vector3(dir.x, 0, dir.y);
            print(ds);
            Move(ds);
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            print("run");
            if (context.started) sm.ChangeState(RUN);
        }

        public void OnAttack()
        {
            AddCommand(0);
        }
    }
}