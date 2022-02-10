using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerInputController : InputController
    {
        bool inputLocked = false;

        [Header("Basic Actions")]
        public string actionButton = "k";
        public string runButton = "j";
        public string defendButton = "l";
        public string toggleScButton = "i";

        [Header("Abilities")]
        public string tlAbility = "y";
        public string trAbility = "p";
        public string blAbility = "u";
        public string brAbility = "o";

        public override InputMode GetInput()
        {
            if (!inputLocked)
            {
                if (Input.GetKeyDown(actionButton)) return InputMode.Action;
                if (Input.GetKeyDown(runButton)) return InputMode.Run;
                if (Input.GetKeyUp(runButton)) return InputMode.Walk;
                if (Input.GetKeyDown(defendButton)) return InputMode.Defend;
                if (Input.GetKeyDown(toggleScButton)) return InputMode.ToggleShortcut;

                if (Input.GetKeyDown(tlAbility)) return InputMode.TLAbility;
                if (Input.GetKeyDown(trAbility)) return InputMode.TRAbility;
                if (Input.GetKeyDown(blAbility)) return InputMode.BLAbility;
                if (Input.GetKeyDown(brAbility)) return InputMode.BRAbility;
            }

            return InputMode.None;
        }

        public override Vector3 GetOutputDir1()
        {
            dir1.x = Input.GetAxisRaw("Horizontal");
            dir1.z = Input.GetAxisRaw("Vertical");

            return dir1;
        }

        public override Vector3 GetOutputDir2()
        {
            return base.GetOutputDir2();
        }
    }
}