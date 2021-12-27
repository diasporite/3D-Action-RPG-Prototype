using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class InputManager : MonoBehaviour
    {
        [Header("Skills")]
        public string tlSkill = "y";
        public string trSkill = "p";
        public string blSkill = "u";
        public string brSkill = "o";

        [Header("D-Pad Menu")]
        public string up = "up";
        public string left = "left";
        public string right = "right";
        public string down = "down";
    }
}