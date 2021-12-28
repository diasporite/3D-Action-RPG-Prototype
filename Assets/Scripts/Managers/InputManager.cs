using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class InputManager : MonoBehaviour
    {
        [Header("Skills")]
        public InputButton upSkill = new InputButton("i", false, false);
        public InputButton leftSkill = new InputButton("j", false, false);
        public InputButton rightSkill = new InputButton("l", false, false);
        public InputButton downSkill = new InputButton("k", false, false);

        [Header("D-Pad Menu")]
        public InputButton up = new InputButton("up", false, false);
        public InputButton left = new InputButton("left", false, false);
        public InputButton right = new InputButton("right", false, false);
        public InputButton down = new InputButton("down", false, false);
    }
}