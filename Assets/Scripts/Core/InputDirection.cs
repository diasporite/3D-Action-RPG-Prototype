using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [Serializable]
    public class InputDirection
    {
        public event Action<Vector3> onDirection;
    }
}