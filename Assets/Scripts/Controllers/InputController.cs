using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum InputMode
    {
        None,

        Action,
        Defend,
        Run,
        Walk,
        ToggleShortcut,

        TLAbility,
        TRAbility,
        BLAbility,
        BRAbility,
    }

    public class InputController : MonoBehaviour
    {
        protected PartyManager party;

        protected Vector3 dir = new Vector3(0, 0);

        private void Awake()
        {
            party = GetComponent<PartyManager>();
        }

        public virtual InputMode GetInput()
        {
            return InputMode.None;
        }

        // Output dir refers to movement dir (xz plane)
        public virtual Vector3 GetOutputDir()
        {
            return Vector3.zero;
        }
    }
}