using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace RPG_Project
{
    public class PlayerController : Controller
    {
        [SerializeField] PartyManager party;

        //public InputMaster controls;

        Vector3 ds = new Vector3(0, 0, 0);

        public PartyManager Party => party;

        protected override void Awake()
        {
            base.Awake();

            party = GetComponentInParent<PartyManager>();

            //controls = new InputMaster();
        }

        protected override void Start()
        {
            base.Start();

            sm.ChangeState(MOVE);
        }

        private void OnEnable()
        {
            //controls.Enable();
        }

        private void OnDisable()
        {
            //controls.Disable();
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
            //sm.AddState(LEFT_WEAPON, new PlayerWeaponState(this, WeaponHand.Left));
            //sm.AddState(RIGHT_WEAPON, new PlayerWeaponState(this, WeaponHand.Right));
            //sm.AddState(COMBAT, new PlayerCombatState(this));
        }

        public void MovePlayer()
        {
            //var dir = controls.Player.Movement.ReadValue<Vector2>();
            var dir = RawInputDir;
            //ds.x = dir.x;
            //ds.z = dir.y;
            //Move(ds);
            Move(dir);
        }

        public bool Run()
        {
            if (Input.GetKey("j") /*&& !movement.Stationary && !stamina.Empty*/)
            //if (controls.Player.Run.triggered)
            {
                sm.ChangeState(RUN);
                return true;
            }

            return false;
        }

        public bool UseSkill()
        {
            if (Input.GetKeyDown("y") || Input.GetKeyDown("u") || 
                Input.GetKeyDown("o") || Input.GetKeyDown("p"))
            //if (inputManager.upSkill.GetInput || inputManager.leftSkill.GetInput || 
            //    inputManager.rightSkill.GetInput || inputManager.downSkill.GetInput)
            //if (controls.Player.UseSkill.triggered)
            {
                //var dir = controls.Player.UseSkill.ReadValue<Vector2>();
                // Cast dir to 4 directions
                AddCommand(0);
                return true;
            }

            return false;
        }

        //public bool SheatheLeft()
        //{
        //    if (Input.GetKeyDown("u"))
        //    {
        //        switch (currentWeapon)
        //        {
        //            case WeaponHand.Left:
        //                if (currentState != ACTION)
        //                {
        //                    sm.ChangeState(MOVE);
        //                    weapon.SwitchWeapon(WeaponHand.Empty);
        //                    anim.SetBool("LeftWeapon", false);
        //                    anim.SetBool("RightWeapon", false);
        //                }
        //                return true;
        //            case WeaponHand.Right:
        //                sm.ChangeState(LEFT_WEAPON);
        //                weapon.SwitchWeapon(WeaponHand.Left);
        //                anim.SetBool("LeftWeapon", true);
        //                anim.SetBool("RightWeapon", false);
        //                return true;
        //            default:
        //                sm.ChangeState(LEFT_WEAPON);
        //                weapon.SwitchWeapon(WeaponHand.Left);
        //                anim.SetBool("LeftWeapon", true);
        //                anim.SetBool("RightWeapon", false);
        //                return true;
        //        }

        //        //if (currentState != ACTION)
        //        //    sm.ChangeState(COMBAT);
        //        //weapon.SwitchWeapon(WeaponHand.Left);
        //    }

        //    return false;
        //}

        //public bool SheatheRight()
        //{
        //    if (Input.GetKeyDown("o"))
        //    {
        //        switch (currentWeapon)
        //        {
        //            case WeaponHand.Left:
        //                sm.ChangeState(RIGHT_WEAPON);
        //                weapon.SwitchWeapon(WeaponHand.Right);
        //                anim.SetBool("LeftWeapon", false);
        //                anim.SetBool("RightWeapon", true);
        //                return true;
        //            case WeaponHand.Right:
        //                if (currentState != ACTION)
        //                {
        //                    sm.ChangeState(MOVE);
        //                    weapon.SwitchWeapon(WeaponHand.Empty);
        //                    anim.SetBool("LeftWeapon", false);
        //                    anim.SetBool("RightWeapon", false);
        //                }
        //                return true;
        //            default:
        //                sm.ChangeState(RIGHT_WEAPON);
        //                weapon.SwitchWeapon(WeaponHand.Right);
        //                anim.SetBool("LeftWeapon", false);
        //                anim.SetBool("RightWeapon", true);
        //                return true;
        //        }

        //        //if (currentState != ACTION)
        //        //    sm.ChangeState(COMBAT);
        //        //weapon.SwitchWeapon(WeaponHand.Right);
        //    }
        //    return false;
        //}

        //#region NewInputSystem
        //public void OnMovement(InputAction.CallbackContext context)
        //{
        //    var dir = context.ReadValue<Vector2>();
        //    var ds = new Vector3(dir.x, 0, dir.y);
        //    print(ds);
        //    Move(ds);
        //}

        //public void OnRun(InputAction.CallbackContext context)
        //{
        //    print("run");
        //    if (context.started) sm.ChangeState(RUN);
        //}

        //public void OnUseSkill(InputAction.CallbackContext context)
        //{
        //    var dir = context.ReadValue<Vector2>();
        //    AddCommand(0);
        //}
        //#endregion
    }
}