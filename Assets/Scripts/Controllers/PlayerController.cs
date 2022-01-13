using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace RPG_Project
{
    public class PlayerController : Controller
    {
        //public InputMaster controls;

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

        protected override void Update()
        {
            base.Update();

            // Take damage debug
            if (Input.GetKeyDown("space"))
            {

            }
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
            base.InitSM();
        }

        void InitInputDicts()
        {

        }

        #region StateCommands
        public override void MovementCommand()
        {
            base.MovementCommand(); //ResourceTick

            string key = Input.inputString;
            var dir = RawInputDirXz;

            // Look for better solution
            switch (key)
            {
                case "j":
                    //if (dir != Vector3.zero) Run();
                    Run();
                    break;
                case "l":
                    SpecialAction();
                    break;
                case "y":
                    UseAbility(0);
                    break;
                case "p":
                    UseAbility(1);
                    break;
                case "u":
                    UseAbility(2);
                    break;
                case "o":
                    UseAbility(3);
                    break;
                default:
                    Move(dir.normalized);   // No dir != zero for fall checks
                    break;
            }
        }

        public override void RunCommand()
        {
            base.RunCommand();  //ResourceTick

            if (Input.GetKeyUp("j")) sm.ChangeState(MOVE);

            string key = Input.inputString;
            var dir = RawInputDirXz;

            //if (dir == Vector3.zero) sm.ChangeState(MOVE);

            switch (key)
            {
                case "l":
                    SpecialAction();
                    break;
                case "y":
                    UseAbility(0);
                    break;
                case "p":
                    UseAbility(1);
                    break;
                case "u":
                    UseAbility(2);
                    break;
                case "o":
                    UseAbility(3);
                    break;
                default:
                    Move(dir.normalized);
                    break;
            }
        }

        public override void ActionCommand()
        {
            if (!queue.Executing) sm.ChangeState(MOVE);

            string key = Input.inputString;
            var dir = RawInputDirXz;

            switch (key)
            {
                case "l":
                    SpecialAction();
                    break;
                case "y":
                    UseAbility(0);
                    break;
                case "p":
                    UseAbility(1);
                    break;
                case "u":
                    UseAbility(2);
                    break;
                case "o":
                    UseAbility(3);
                    break;
                default:
                    //if (dir != Vector3.zero) Move(dir.normalized);
                    break;
            }
        }

        public override void RecoveryCommand()
        {
            base.RecoveryCommand(); //ResourceTick

            //string key = Input.inputString;
            var dir = RawInputDirXz;

            if (Stamina.Full) sm.ChangeState(MOVE);

            if (dir != Vector3.zero) Move(dir.normalized);
        }

        public override void StaggerCommand()
        {
            base.StaggerCommand();
        }

        public override void DeathCommand()
        {
            base.DeathCommand();
        }
        #endregion

        #region OldInputMethods
        //public void MovePlayer2()
        //{
        //    //var dir = controls.Player.Movement.ReadValue<Vector2>();
        //    var dir = RawInputDir;
        //    //ds.x = dir.x;
        //    //ds.z = dir.y;
        //    //Move(ds);
        //    Move(dir);
        //}

        //public bool Run2()
        //{
        //    if (Input.GetKey("j") /*&& !movement.Stationary && !stamina.Empty*/)
        //    //if (controls.Player.Run.triggered)
        //    {
        //        sm.ChangeState(RUN);
        //        return true;
        //    }

        //    return false;
        //}

        //public bool UseSkill2()
        //{
        //    // top left, top right, bottom left, bottom right
        //    var inputs = new string[] { "y", "p", "u", "o" };

        //    //if (Input.GetKeyDown("y") || Input.GetKeyDown("u") || 
        //    //    Input.GetKeyDown("o") || Input.GetKeyDown("p"))
        //    ////if (inputManager.upSkill.GetInput || inputManager.leftSkill.GetInput || 
        //    ////    inputManager.rightSkill.GetInput || inputManager.downSkill.GetInput)
        //    ////if (controls.Player.UseSkill.triggered)
        //    //{
        //    //    //var dir = controls.Player.UseSkill.ReadValue<Vector2>();
        //    //    // Cast dir to 4 directions
        //    //    AddCommand(0);
        //    //    return true;
        //    //}

        //    for (int i = 0; i < inputs.Length; i++)
        //    {
        //        if (Input.GetKeyDown(inputs[i]))
        //        {
        //            AddAbilityCommand(i);
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        //public bool SpecialAction2()
        //{
        //    var weightclass = combatant.Character.Weightclass;
        //    BattleCommand act;

        //    if (Input.GetKeyDown("l"))
        //    {
        //        switch (weightclass)
        //        {
        //            case WeightClass.Lightweight:
        //                act = new JumpCommand(this);
        //                AddCommand(act);
        //                return true;
        //            case WeightClass.Heavyweight:
        //                act = new GuardCommand(this);
        //                AddCommand(act);
        //                return true;
        //            default:
        //                act = new RollCommand(this);
        //                AddCommand(act);
        //                return true;
        //        }
        //    }

        //    return false;
        //}
        #endregion

        #region NewInputSystem
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
        #endregion
    }
}