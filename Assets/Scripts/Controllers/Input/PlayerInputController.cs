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

        [Header("Char")]
        public string char1 = "z";
        public string char2 = "x";
        public string char3 = "c";
        public string char4 = "v";

        string[] inputStrings = new string[] { "k", "j", "l", "i", "y", "p", "u", "o", "z", "x", "c", "v" };
        InputMode[] modes = new InputMode[] { InputMode.Action, InputMode.Run, InputMode.Walk, InputMode.Defend,
            InputMode.ToggleShortcut, InputMode.TLAbility, InputMode.TRAbility, InputMode.BLAbility, InputMode.BRAbility,
            InputMode.Char1, InputMode.Char2, InputMode.Char3, InputMode.Char4 };

        Dictionary<string, InputMode> dict = new Dictionary<string, InputMode>();

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < inputStrings.Length; i++)
                dict.Add(inputStrings[i], modes[i]);
        }

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

                if (Input.GetKeyDown(char1)) return InputMode.Char1;
                if (Input.GetKeyDown(char2)) return InputMode.Char2;
                if (Input.GetKeyDown(char3)) return InputMode.Char3;
                if (Input.GetKeyDown(char4)) return InputMode.Char4;
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